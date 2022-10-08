/**
 * Enum dùng chung
 * Author: TVKhang(20/07/22)
 */

const EnumMisa = {
    // Enum giới tính
    Gender: {
        Male: 0, // Nam
        Female: 1, // Nữ
        Other: 2, // Khác
    },
    TypeOfCustomer: {
        Customer: 0, // Là khách hàng
        Vendor: 1, // Là nhà cung cấp
        CustomerAndVendor: 2, // Là khách hàng và nhà cung cấp
    },
    // Enum các hành động của form
    FormMode: {
        New: 0, // Thêm mới
        Edit: 1, // Sửa thông tin
        Watch: 2, // Xem chi tiết
    },

    //Index của key trên bàn phím
    KeyCode: {
        Enter: 13,
        ESC: 27,
        UpArrow: 38,
        DownArrow: 40,
        Tab: 9,
        Ctrl: 17,
        S: 83,
    },

    // Enum thông tin Popup
    PopUp: {
        Warning: 1, // Loại popup cảnh báo
        Question: 2, // Loại popup câu hỏi
        Error: 3, // Loại popup cảnh báo lỗi

        // Hành động của popup
        Action: {
            Delete: 1, // Xóa
            New: 2, // Thêm mới
            Update: 3, // Chỉnh sửa
            Notify: 4, // Thông báo
        },
    },

    // Enum thông tin validate
    Validate: {
        Required: 1, // Trường bắt buộc nhập
        Email: 2, // Trường là email
        EmployeeCode: 3, // Trường là mã nhân viên
        Date: 4, // Validate ngày tháng
        Number: 5, // Trường là số
    },
};

export default EnumMisa;
