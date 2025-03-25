using DongTaBhxh.Enums;

namespace DongTaBhxh.DefaultValue;
public record InfoMessage {
    public static string PostFailed => "Error: Xảy ra lổi cập nhật dữ liệu";
    public static string DownloadFailed => "Error: Download failed !";
    public static string DownloadSuccess => "Tải file thành công !";
    public static string Success => "Success: Đã xử lí thành công";
    public static string Unselect(string name) => $"Error: Chưa chọn {name}";
    public static string ApiCallFailed(string name) => $"Error: Lỗi khi lấy đối tượng '{name}'";
    public static string UserNotDound => "Error: không tìm thấy user";
    public static string ObjectNull(string objName) => $"Error, {objName} is null";
    public static string NotAuthorized => "Warning: tài khoản của bạn không thể vào chức năng này!";
    public static string NotFound => "Warning, không tìm thấy kết quả";
    public static string NotSupport(string objectName) => $"Warning, {objectName} không được hỗ trợ !";
    public static string InvalidDateTime => "Warning: ngày tháng năm không hợp lệ";
    public static string InputEmpty => "Error: Không có dữ liệu nhập vào !";
    public static string InvalidId(string name) => $"Error: Mã {name} không hợp lệ";
    public static string ActionSuccess(CRUD actionName, string objName) => $"Success: {actionName.CRUD2String()} {objName} thành công";
    public static string ActionFailed(CRUD actionName, string objName) => $"Error: {actionName.CRUD2String()} {objName} thất bại";
}

public record ReadOnlyValue {
    /// <summary>
    /// Vì xếp loại quý là sau quý hiện tại nên phải trừ đi một số ngày đẻ lấy quý hiện tại
    /// </summary>
    public const int SubDay = -25;
}