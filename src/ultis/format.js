// Định dạng lại ngày tháng năm
// Author: TVKhang(11/09/22)

function formatTime(data) {
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
}
export { formatTime };
