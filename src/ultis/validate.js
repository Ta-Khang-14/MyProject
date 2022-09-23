import EnumMisa from "./enum.js";

// Xử lý validate dữ liệu
// Author: TVKhang(20/09/2022)
function validate(data, action) {
    try {
        switch (action) {
            case EnumMisa.Validate.Required:
                return validateRequired(data);
            case EnumMisa.Validate.Email:
                return validateEmail(data);
            case EnumMisa.Validate.Phone:
                return validatePhone(data);
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

// Validate dữ liệu có phải là số điện thoại
function validatePhone(data) {
    data = data.trim();
    if (data != "") {
        if (
            !/^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/.test(
                data
            )
        ) {
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

// Validate dữ liệu có phải là số điện thoại
function validateString(data) {
    data = data.trim();
    if (data != "") {
        if (
            !/^[a-zA-Z0-9-,\s_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]+$/.test(
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
        if (!/^[0-9]{1,}$/.test(data)) {
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
