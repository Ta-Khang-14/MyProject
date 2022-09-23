import validate from "./validate.js";

// Xử lý validate
// Author: TVKhang(20/09/22)
export default function validateHandle(fieldData = []) {
    fieldData.forEach((e) => {
        let validateResult = e.required
            .map((rq) => validate(e.value, rq))
            .filter((value) => !value.status);

        if (validateResult.length != 0) {
            e.msg = validateResult[0].msg;
            e.status = false;
        }
    });
}
