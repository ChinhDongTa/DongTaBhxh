using DongTa.QuarterInYear;
using DongTa.TypeExtension;
using DongTaBhxh.ApiGateway.Controllers.FileApi.Reports.Models;
using DongTaBhxh.ApiGateway.Helpers;
using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.DataTranfer.Mapping;
using DongTaBhxh.Domain.Data;
using DongTaBhxh.Parameter.Reports;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.ApiGateway.Controllers.FileApi.Reports;

internal class DataSourceConverter {

    public static async Task<List<TongHopQuyLine>?> GetTongHopQuyDataSource(DongTaBhxhDbContext context, TongHopQuyPara thamSo)
    {
        List<QuarterDepartmentRankDto>? xepLoaiDonVi = await context.QuarterDepartmentRanks
            .Where(x => x.Quarter == thamSo.Quarter && x.Year == thamSo.Year).Include(x => x.Dept)
            .Select(x => x.ToDto()).ToListAsync();

        if (xepLoaiDonVi is not null)
        {
            var list = new List<TongHopQuyLine>();
            int sttDept = 1;
            foreach (var item in xepLoaiDonVi)
            {
                //Thêm dòng đơn vi
                TongHopQuyLine deptLine = new()
                {
                    //SortOrder = sttDept,
                    Roman = sttDept++.ToRomanNumeral(),
                    DeptName = item.DeptName,
                    //FullName = item.DeptName,
                    SelfClassificationDept = item.RewardName
                };
                if (item.DeptId != 1)//Trừ ban giám đốc ra
                {
                    //Điểm tự chấm của đơn vị
                    if (item.SelfScore.HasValue)
                    {
                        deptLine.SelfScoreDept = $"{item.SelfScore}/{item.BaseCore} = {((double)item.SelfScore.Value / item.BaseCore).ToPercentString(1)}";
                    }
                    //Điểm xét duyệt của đơn vị
                    if (item.ResultScore.HasValue)
                    {
                        deptLine.ResultScoreDept = $"{((double)item.ResultScore.Value / item.BaseCore).ToPercentString(1)}";
                    }
                }
                deptLine.Group = item.RewardName switch
                {
                    "A" => "1,0",
                    "B" => "0,97",
                    "C" => "0,94",
                    "D" => "0,9",
                    _ => string.Empty
                };
                var xepLoaiCaNhans = await context.GetQuarterEmployeeRankDtoByDeptId(item.DeptId, new QuarterInYear((byte)thamSo.Quarter, thamSo.Year));
                deptLine.SummaryDept = GetSummaryDept(xepLoaiCaNhans, item.DeptId);
                //Thêm Đơn vị vào danh sách
                list.Add(deptLine);

                //Thêm dòng cá nhân
                int sttCaNhan = 1;
                foreach (var caNhan in xepLoaiCaNhans)
                {
                    TongHopQuyLine caNhanLine = new()
                    {
                        Roman = deptLine.Roman,
                        STT = sttCaNhan++,
                        FullName = caNhan.EmployeeName,
                        DeptName = deptLine.DeptName,
                        Note = caNhan.Note,
                        SelfScore = caNhan.SelfScore != null ? caNhan.SelfScore.Value.ToString() : string.Empty,
                        ResultScore = caNhan.ResultScore != null ? caNhan.ResultScore.Value.ToString() : string.Empty,
                    };
                    switch (caNhan.RewardName)
                    {
                        case "A":
                            caNhanLine.A = 'A';
                            caNhanLine.Personal = "1,3";
                            break;

                        case "B":
                            caNhanLine.B = 'B';
                            caNhanLine.Personal = "1,2";
                            break;

                        case "C":
                            caNhanLine.C = 'C';
                            caNhanLine.Personal = "0,7";
                            break;

                        case "D":
                            caNhanLine.D = 'D';
                            caNhanLine.Personal = "0";
                            break;

                        default:
                            break;
                    }
                    list.Add(caNhanLine);
                }
            }
            return list;
        }
        return null;
    }

    public static IEnumerable<XepLoaiCaNhanLine>? ToXepLoaiCaNhanLines(IEnumerable<QuarterEmployeeRankDto>? list)
    {
        if (list is not null)
        {
            byte index = 1;
            var result = new List<XepLoaiCaNhanLine>();
            foreach (var item in list)
            {
                XepLoaiCaNhanLine line = new()
                {
                    STT = index++,
                    FullName = item.EmployeeName!,
                    Note = item.Note
                };
                switch (item.RewardId)
                {
                    case 22: line.A = "x"; break;
                    case 23: line.B = "x"; break;
                    case 24: line.C = "x"; break;
                    case 25: line.D = "x"; break;
                    default:
                        break;
                }
                result.Add(line);
            }
            return result;
        }
        return null;
    }

    internal static IEnumerable<TongHopQuyCcVc>? ToTongHopKetQuaCongTacQuy(IEnumerable<QuarterEmployeeRankDto>? list)
    {
        if (list is not null)
        {
            var result = new List<TongHopQuyCcVc>();
            byte stt = 1;
            foreach (var item in list)
            {
                result.Add(new TongHopQuyCcVc()
                {
                    TotalWork = item.TotalWork ?? 0,
                    Note = item.Note,
                    NumWorked = item.NumWorked ?? 0,
                    Reward = item.RewardName!,
                    StaffName = item.EmployeeName!,
                    STT = stt++,
                    SelfReward = GetSelfReward(item.SelfScore)
                });
            }
            return result;
        }
        return null;
    }

    private static string? GetSummaryDept(IEnumerable<QuarterEmployeeRankDto> xepLoaiCaNhan, int deptId)
    {
        if (deptId != 1)//trừ ban giám đốc
        {
            string result = string.Empty;
            int A = xepLoaiCaNhan.Count(x => x.RewardId == 22);
            int B = xepLoaiCaNhan.Count(x => x.RewardId == 23);
            int C = xepLoaiCaNhan.Count(x => x.RewardId == 24);
            int D = xepLoaiCaNhan.Count(x => x.RewardId == 25);
            if (A > 0)
                result = $"{A}A";
            if (B > 0)
                result = $"{result}, {B}B";
            if (C > 0)
                result = $"{result}, {C}C";
            if (D > 0)
                result = $"{result}, {D}D";
            return result;
        }
        return null;
    }

    private static string GetSelfReward(int? selfScore) => selfScore.HasValue
            ? selfScore switch
            {
                >= 90 => "A",
                < 90 and >= 80 => "B",
                < 80 and > 50 => "C",
                _ => "D",
            }
            : string.Empty;
}