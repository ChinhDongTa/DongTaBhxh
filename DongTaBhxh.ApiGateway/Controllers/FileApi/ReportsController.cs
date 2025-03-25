using DongTa.BaseDapper;
using DongTa.QuarterInYear;
using DongTa.ResponseMessage;
using DongTa.ResponseResult;
using DongTa.TypeExtension;
using DongTaBhxh.ApiGateway.Controllers.FileApi.Reports;
using DongTaBhxh.ApiGateway.Controllers.FileApi.Reports.Models;
using DongTaBhxh.ApiGateway.Helpers;
using DongTaBhxh.DataTranfer.Dtos;
using DongTaBhxh.DefaultValue;
using DongTaBhxh.Domain.Data;
using DongTaBhxh.Parameter.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;

namespace DongTaBhxh.ApiGateway.Controllers.FileApi;

[Route("[controller]")]
[ApiController]
public class ReportsController(DongTaBhxhDbContext context, IGenericDapper dapper) : ControllerBase {

    [HttpGet("test/{userId}")]
    public async Task<IActionResult> Test(string userId)
    {
        //Sử dụng dapper đẻ lấy  dữ liệu DTO
        var deptDto = await dapper.GetOneAsync<DepartmentDto>(SqlQueryString.SelectDepartmentDtoByUserId(userId));

        return Ok(ResultExtension.GetResult(deptDto));
    }

    [HttpPost("BienBanHopQuy")]
    public async Task<IActionResult> BienBanHopQuy([FromBody] BienBanHopQuyPara bienBan)
    {
        if (bienBan is not null)
        {
            var deptDto = await dapper.GetOneAsync<DepartmentDto>(SqlQueryString.SelectDepartmentDtoByUserId(bienBan.UserId));
            if (deptDto is not null)
            {
                using LocalReport report = new();
                DateTime now = DateTime.Now.AddDays(ReadOnlyValue.SubDay);
                byte currentQuarter = DateTimeExtension.GetCurrentQuarter(now.Month);
                var ds = await context.GetQuarterEmployeeRankDtoByDeptId(deptDto.DepartmentId, new QuarterInYear(currentQuarter, now.Year));
                string content = "1. Đánh giá tình hình công tác quý {0} năm {1}, triển khai nhiệm vụ quý {2} năm {3}. Bình xét thi đua, khen thưởng quý {0} năm {1}";
                if (currentQuarter == 4)
                    content = string.Format(content, currentQuarter, now.Year, 1, now.Year + 1);
                else
                    content = string.Format(content, currentQuarter, now.Year, currentQuarter + 1, now.Year);
                ReportParameter[] reportPara = [
                    new ReportParameter ("DeptName", deptDto.Name.ToUpper()),
                    new ReportParameter ("ChuTri", bienBan.ChuTri),
                    new ReportParameter ("ChucVuChuTri", bienBan.ChucVuChuTri),
                    new ReportParameter ("ThuKy", bienBan.ThuKy),
                    new ReportParameter ("ChucVuThuKy", bienBan.ChucVuThuKy),
                    new ReportParameter ("SoThanhVienVangMat", bienBan.SoThanhVienVangMat.ToString()),
                    new ReportParameter ("XepLoaiPhong", bienBan.XepLoaiPhong),
                    new ReportParameter ("DeXuatKhenThuongTapThe", bienBan.DeXuatKhenThuongTapThe),
                    new ReportParameter ("KetLuan", bienBan.KetLuan),
                    new ReportParameter ("Time", bienBan.ThoiGian),
                    new ReportParameter ("NoiDung", content)
                ];
                Load.BienBanHopQuy(report, reportPara, DataSourceConverter.ToXepLoaiCaNhanLines(ds));
                var pdf = report.Render("PDF");
                return File(pdf, "application/pdf", $"BienBan.pdf");
            }
        }
        return Ok(Result<bool>.Failure(Message.Notfound));
    }

    /// <summary>
    /// Báo cáo tổng hợp quý PL4
    /// </summary>
    /// <param name="thamSo"></param>
    /// <returns></returns>
    [HttpPost("TongHopQuyPL4")]
    public async Task<IActionResult> TongHopQuyPL4([FromBody] TongHopQuyPara thamSo)
    {
        //thamSo.FileExtension = 1;
        List<TongHopQuyLine>? dataSource = await DataSourceConverter.GetTongHopQuyDataSource(context, thamSo);
        if (dataSource is not null)
        {
            //xuát báo cáo PDF
            if (thamSo.FileExtension == 0)
            {
                using LocalReport report = new();
                ReportParameter[] reportPara =
                [
                    new ReportParameter ("Director", thamSo.Director),
                    new ReportParameter ("DeptNameCreator", thamSo.DeptNameCreator),
                    new ReportParameter ("Creator", thamSo.Creator),
                    new ReportParameter ("SigningDate", thamSo.SigningDate),
                    new ReportParameter ("DocDescription", thamSo.DocDescription),
                    new ReportParameter ("SumA", dataSource.Count(x=>x.A!=null).ToString()),
                    new ReportParameter ("SumB", dataSource.Count(x=>x.B!=null).ToString()),
                    new ReportParameter ("SumC", dataSource.Count(x=>x.C!=null).ToString()),
                    new ReportParameter ("SumD", dataSource.Count(x=>x.D!=null).ToString()),
                    new ReportParameter ("SumKhongXet", $"{await context.QuarterEmployeeRanks.CountAsync(x=>x.Quarter==thamSo.Quarter &&x.Year==thamSo.Year && x.RewardId==33)} không xét"),
                    new ReportParameter ("Title", $"DANH SÁCH XẾP LOẠI CCVC QUÝ {thamSo.Quarter} NĂM {thamSo.Year}" ),
                ];

                Load.TongHopQuyTinh(report, reportPara, dataSource);
                var pdf = report.Render("PDF");
                return File(pdf, "application/pdf", $"XepLoaiQuy{thamSo.Quarter}Nam{thamSo.Year}.pdf");
            }
            else if (thamSo.FileExtension == 1)//Excel
            {
                return ExcelExtension.ExportExcel(dataSource.AsQueryable(), $"XepLoaiQuy{thamSo.Quarter}Nam{thamSo.Year}");
            }
        }
        return Ok(Result<bool>.Failure(Message.Notfound));
    }
}