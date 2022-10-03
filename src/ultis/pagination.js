/**
 * Phân trang
 * Author: TVKhang(26/09/22)
 * @param {*} currentPage Trang hiện tại
 * @param {*} totalPage   Tổng số trang
 * @returns
 */
function pagination(currentPage, totalPage) {
    try {
        var current = currentPage,
            last = totalPage,
            delta = 1,
            left = current - delta,
            right = current + delta + 1,
            range = [],
            rangeWithDots = [],
            l;

        // Lấy trang đầu trang cuối, và các trang gần trang hiện tại
        for (let i = 1; i <= last; i++) {
            if (i == 1 || i == last || (i >= left && i < right)) {
                range.push(i);
            }
        }

        for (let i of range) {
            if (l) {
                if (i - l === 2) {
                    rangeWithDots.push(l + 1);
                } else if (i - l !== 1) {
                    rangeWithDots.push("...");
                }
            }
            rangeWithDots.push(i);
            l = i;
        }
        return rangeWithDots;
    } catch (err) {
        console.log(err);
        return [];
    }
}

export default pagination;
