namespace MISA.Web08.QTKD.Common.Khang
{
    public class ErrorResult
    {
        #region Property

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public QTKDErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Thông báo lỗi cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TraceId { get; set; }

        #endregion

        #region Contructor

        public ErrorResult() { }

        public ErrorResult(QTKDErrorCode errorCode, string devMsg, string userMsg, string moreInfo, string traceId)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceId = traceId;
        }

        #endregion

        #region Static Method



        #endregion
    }
}
