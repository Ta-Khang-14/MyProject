namespace MISA.Web08.Demo.QTKD.Common.Khang
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BaseValidateAttribute : Attribute
    {
        #region Property
        public string ErrorMessage { get; set; }
        #endregion

        #region Contructor
        public BaseValidateAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        #endregion
    }

    /// <summary>
    /// Xác định xem 1 thuộc tính có là khóa chính hay không
    /// </summary>
    public class PrimaryKey : BaseValidateAttribute
    {
        public PrimaryKey(string errorMessage) : base(errorMessage)
        {
        }
    }

    /// <summary>
    /// Xác định xem 1 thuộc dính có null hay không
    /// </summary>
    public class IsNotNullOrEmpty : BaseValidateAttribute
    {
        public IsNotNullOrEmpty(string errorMessage) : base(errorMessage)
        {
        }
    }

    /// <summary>
    /// Xác định xem 1 thuộc tính có giá trị là email hay không
    /// </summary>
    public class IsEmail : BaseValidateAttribute
    {
        public IsEmail(string errorMessage) : base(errorMessage)
        {
        }
    }

    /// <summary>
    /// Xác định xem 1 thuộc tính có là Mã nhân viên hay không
    /// </summary>
    public class IsEmployeeCode : BaseValidateAttribute
    {
        public IsEmployeeCode(string errorMessage) : base(errorMessage)
        {
        }
    }

    /// <summary>
    /// Xác định xem trường date có vượt qua ngày hiện tại hay không
    /// </summary>
    public class IsValidDate : BaseValidateAttribute
    {
        public IsValidDate(string errorMessage) : base(errorMessage)
        {
        }
    }
}
