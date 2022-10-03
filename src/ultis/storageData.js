/**
 * Lưu dữ liệu vào localstorage
 * Author: TVKhang
 * @param {*} varName Tên biến
 * @param {*} data    Giá trị biến
 */
function storageData(varName, data) {
    try {
        localStorage[varName] = data;
    } catch (err) {
        console.log(err);
    }
}

export default storageData;
