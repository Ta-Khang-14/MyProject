namespace MISA.Web08.QTKD.Common.Khang
{
    public class ResponseHandle
    {
        #region Field
        /// <summary>
        /// Thể hiện dữ liệu có thành công hay không
        /// </summary>
        public Boolean IsSuccess { get; set; }

        /// <summary>
        /// Đối tượng thể hiện các lỗi
        /// </summary>
        public ErrorResult? ErrorResult { get; set; }
        #endregion

        #region Contructor
        public ResponseHandle() { }

        public ResponseHandle(bool isSuccess, ErrorResult? errorResult)
        {
            IsSuccess = isSuccess;
            ErrorResult = errorResult;
        }

        #endregion
    }
}
