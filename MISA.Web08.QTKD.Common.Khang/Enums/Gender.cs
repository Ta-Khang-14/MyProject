using System.ComponentModel;

namespace MISA.Web08.QTKD.Common.Khang
{
    /// <summary>
    /// Giới tính
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Nam
        /// </summary>
        [Description("Nam")]
        Male = 0,

        /// <summary>
        /// Nữ
        /// </summary>
        [Description("Nữ")]
        Female = 1,

        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        Other = 2
    }
}
