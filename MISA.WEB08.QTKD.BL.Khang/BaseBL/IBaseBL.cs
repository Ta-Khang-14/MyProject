using MISA.Web08.QTKD.Common.Khang;

namespace MISA.WEB08.QTKD.BL.Khang
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi của 1 bảng
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi của 1 mảng</returns>
        /// Created by: TVK(29/09/22)
        public ResponseHandle Records(string traceID);

        /// <summary>
        /// Lấy chi tiết 1 bản ghi theo ID
        /// </summary>
        /// <returns>Lấy chi tiết 1 bản ghi</returns>
        /// Created by: TVK(29/09/22)
        public ResponseHandle Record(Guid recordID, string traceID);
    }
}
