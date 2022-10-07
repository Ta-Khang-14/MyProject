using MISA.Web08.QTKD.Common.Khang;

namespace MISA.Web08.QTKD.DL.Khang
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi của 1 bảng
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi của 1 mảng</returns>
        /// Created by: TVK(29/09/22)
        public ResponseHandle Records(string traceID);

        /// <summary>
        /// Lấy thông tin 1 bản ghi
        /// </summary>
        /// <returns>Lấy thông tin 1 bản ghi</returns>
        /// Created by: TVK(29/09/22)
        public ResponseHandle Record(Guid recordID, string traceID);
    }
}
