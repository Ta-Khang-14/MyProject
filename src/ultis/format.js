// Định dạng lại ngày tháng năm
// Author: TVKhang(11/09/22)
function formatTime(data) {
    try {
        if (data) {
            data = new Date(data);
            let day = data.getDate();
            let month = data.getMonth() + 1;
            let year = data.getFullYear();
            // let hour = data.getHours();
            // let minute = data.getMinutes();
            return `${day}/${month}/${year}`;
        }
        return "";
    } catch (err) {
        console.log(err);
    }
}

// Loại bỏ 1 số kí tự lạ trong chuỗi nhập vào
// Author: TVKhang(25/09/22)
function simpleFormatString(data) {
    try {
        return data.replaceAll(/'|;|%/g, "");
    } catch (err) {
        console.log(err);
        return "";
    }
}

// Xử lý mã code được trả về từ server
function handleRecordCode(data, prefix) {
    data = data.replaceAll(prefix, "");
    let length = data.length;
    data = +data + 1 + "";
    data =
        length - data.length > 0
            ? prefix + "0".repeat(length - data.length) + data
            : prefix + data;
    return data;
}

export { formatTime, simpleFormatString, handleRecordCode };
