namespace MISA.Web08.Demo.QTKD.Common.Khang
{

    /// <summary>
    /// Dùng để xác định 1 property là khóa chính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }


    public class IsNotNullOrEmpty : Attribute
    {
        #region Property
        public string ErrorMessage { get; set; }
        #endregion

        #region Contructor
        public IsNotNullOrEmpty(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        #endregion
    }
}
