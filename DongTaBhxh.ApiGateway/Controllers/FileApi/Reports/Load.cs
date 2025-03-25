using DongTaBhxh.ApiGateway.Controllers.FileApi.Reports.Models;
using Microsoft.Reporting.NETCore;
using System.Reflection;

namespace DongTaBhxh.ApiGateway.Controllers.FileApi.Reports;

internal class Load {

    internal static void BienBanHopQuy(LocalReport report, ReportParameter[] parameters, IEnumerable<XepLoaiCaNhanLine>? lines)
    {
        using var rs = GetReportStream("DongTaBhxh.ApiGateway.Controllers.FileApi.Reports.BienBanHopQuy.rdlc");
        report.LoadReportDefinition(rs);
        report.DataSources.Add(new ReportDataSource("Lines", lines));
        report.SetParameters(parameters);
    }

    internal static void TongHopQuyTinh(LocalReport report, ReportParameter[] reportPara, List<TongHopQuyLine> dataSource)
    {
        using var rs = GetReportStream("DongTaBhxh.ApiGateway.Controllers.FileApi.Reports.TongHopQuyPhuLuc4.rdlc");
        report.LoadReportDefinition(rs);
        report.DataSources.Add(new ReportDataSource("Lines", dataSource));
        report.SetParameters(reportPara);
    }

    private static Stream? GetReportStream(string reportName) => Assembly.GetExecutingAssembly().GetManifestResourceStream(reportName);
}