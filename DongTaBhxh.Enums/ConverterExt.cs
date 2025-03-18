namespace DongTaBhxh.Enums;

public static class ConverterExt {
    public static string CRUD2String(this CRUD crud) => crud switch
    {
        CRUD.Create => "Tạo",
        CRUD.Read => "Đọc",
        CRUD.Update => "Cập nhật",
        CRUD.Delete => "Xóa",
        _ => "Không xác định"
    };

}
