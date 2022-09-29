namespace MISA.Web08.QTKD.Common.Khang
{
    public enum QTKDErrorCode
    {
        /// <summary>
        /// Lỗi không xác định
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Trùng mã nhân viên
        /// </summary>
        DupplicateCode = 2,

        /// <summary>
        /// ID không tìm thấy
        /// </summary>
        NotFoundID = 3,

        /// <summary>
        /// ID sai định dạng
        /// </summary>
        InvalidID = 4,
    }
}
