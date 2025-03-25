using DongTa.TypeExtension;
using ExportQueryToExcelStream;
using Microsoft.AspNetCore.Mvc;

namespace DongTaBhxh.ApiGateway.Helpers;

public static class ExcelExtension {

    public static FileStreamResult ExportExcel(IQueryable queryable, string? fileName = null)
    {
        StreamExcelConverter streamExcel = new(queryable);
        //var fileNameFull=fileName is null ? $"ErpBhxhGiaLai_{ DateTime.Now.ToYYYYMMDD()}.xlsx":$"{fileName}_{DateTime.Now.ToYYYYMMDD()}.xlsx";
        return new FileStreamResult(streamExcel.ToExcelStream(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = fileName is null ? $"ErpBhxhGiaLai_{DateTime.Now.ToYYYYMMDD()}.xlsx" : $"{fileName}_{DateTime.Now.ToYYYYMMDD()}.xlsx"
        };
    }
}