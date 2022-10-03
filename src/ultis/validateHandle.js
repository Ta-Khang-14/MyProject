import validate from "./validate.js";

/**
 * Xử lý validate
 * Author: TVKhang(20/09/22)
 * @param {*} fieldData danh sách các object chứa thông tin cần validate
 */
export default function validateHandle(fieldData = []) {
    fieldData.forEach((e) => {
        e.status = true;
        let validateResult = e.required
            .map((rq) => validate(e.value, rq))
            .filter((value) => !value.status);

        if (validateResult.length != 0) {
            e.msg = validateResult[0].msg;
            e.status = false;
        }
    });
}
