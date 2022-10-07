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

        /// <summary>
        /// Dữ liệu trả về khi query thành công
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Mã trả về
        /// </summary>
        public int Status { get; set; }
        #endregion

        #region Contructor
        public ResponseHandle() { }

        public ResponseHandle(bool isSuccess, int status, object? data, ErrorResult? errorResult)
        {
            IsSuccess = isSuccess;
            Status = status;
            Data = data;
            ErrorResult = errorResult;
        }

        #endregion
    }
}
