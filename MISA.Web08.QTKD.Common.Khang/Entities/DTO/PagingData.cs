namespace MISA.Web08.QTKD.Common.Khang
{
    public class PagingData<T>
    {
        #region Property
        /// <summary>
        /// Danh sách bản ghi
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalCount { get; set; }

        #endregion

        #region Contructor

        public PagingData() { }

        public PagingData(List<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }
        #endregion
    }
}
