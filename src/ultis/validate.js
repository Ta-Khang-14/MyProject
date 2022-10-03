import EnumMisa from "./enum.js";

/**
 * Xử lý validate dữ liệu
 * Author: TVKhang(20/09/2022)
 * @param {*} data dữ liệu cần validate
 * @param {*} action kiểu validate
 * @returns Object chứa thông tin lỗi
 */
function validate(data, action) {
    try {
        switch (action) {
            case EnumMisa.Validate.Required:
                return validateRequired(data);
            case EnumMisa.Validate.Email:
                return validateEmail(data);
            case EnumMisa.Validate.EmployeeCode:
                return validateEmployeeCode(data);
            case EnumMisa.Validate.StringUTF8:
                return validateString(data);
            case EnumMisa.Validate.Number:
                return validateNumber(data);
        }
    } catch (err) {
        console.log("Có lỗi: ", err);
    }
}

export default validate;

// Validate dữ liệu có bị trống
function validateRequired(data) {
    if (data.trim() == "") {
        return {
            status: false,
            msg: "Không được để trống",
        };
    }
    return {
        status: true,
    };
}

// Validate dữ liệu có phải là email
function validateEmail(data) {
    data = data.trim();
    if (data != "") {
        if (!/^[\w-.]+@([\w-]+.)+[\w-]{2,4}$/.test(data)) {
            return {
                status: false,
                msg: "Sai định dạng",
            };
        }
    }
    return {
        status: true,
    };
}

// Validate dữ liệu có phải là mã nhân viên
function validateEmployeeCode(data) {
    data = data.trim();
    if (data != "") {
        if (!/^NV-\d{8,}$/.test(data)) {
            return {
                status: false,
                msg: "Sai định dạng",
            };
        }
    }
    return {
        status: true,
    };
}

// Validate dữ liệu có phải là chuỗi ký tự hợp lệ
function validateString(data) {
    data = data.trim();
    if (data != "") {
        if (
            !/^[a-zA-Z0-9-,\s_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]{0,255}$/.test(
                data
            )
        ) {
            return {
                status: false,
                msg: "Sai định dạng",
                data,
            };
        }
    }
    return {
        status: true,
    };
}

// Validate dữ liệu có phải là số
function validateNumber(data) {
    data = data.trim();
    if (data != "") {
        if (!/^[0-9]{0,255}$/.test(data)) {
            return {
                status: false,
                msg: "Sai định dạng",
            };
        }
    }
    return {
        status: true,
    };
}
