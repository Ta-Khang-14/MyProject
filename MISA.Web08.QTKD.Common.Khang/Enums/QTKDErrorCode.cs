namespace MISA.Web08.QTKD.Common.Khang
{
    public enum QTKDErrorCode
    {
        /// <summary>
        /// Lỗi không xác định
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Trùng mã
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

        /// <summary>
        /// Không được để trống
        /// </summary>
        NotNullOrEmpty = 5,

        /// <summary>
        /// Không phải là email
        /// </summary>
        EmailInvalid = 5,

        /// <summary>
        /// Dữ liệu đầu vào không hợp lệ
        /// </summary>
        DataInputInvalid = 6,

        /// <summary>
        /// Trùng ID
        /// </summary>
        DuplicateID = 7
    }
}
