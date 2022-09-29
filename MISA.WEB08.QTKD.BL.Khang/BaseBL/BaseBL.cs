using MISA.Web08.QTKD.Common.Khang;
using MISA.Web08.QTKD.DL.Khang;

namespace MISA.WEB08.QTKD.BL.Khang
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field
        private IBaseDL<T> _baseDL;
        #endregion

        #region Contructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả bản ghi của 1 bảng
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi của 1 mảng</returns>
        /// Created by: TVK(29/09/22)
        public PagingData<T> Records()
        {
            return _baseDL.Records();
        }

        /// <summary>
        /// Lấy thông tin 1 bản ghi
        /// </summary>
        /// <returns>Thông tin chi tiết 1 bản ghi</returns>
        /// Created by: TVK(29/09/22)
        public T Record(Guid recordID)
        {
            return _baseDL.Record(recordID);
        }
        #endregion
    }
}

