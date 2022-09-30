using MISA.Web08.Demo.QTKD.Common.Khang;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MISA.Web08.QTKD.Common.Khang
{
    public class ValidateData<T>
    {
        #region Static method
        /// <summary>
        /// Validate những thuộc tính có Attribute là IsNotNullOrEmpty
        /// </summary>
        /// <param name="property"></param>
        /// <param name="propertyValue"></param>
        /// <returns>Chuỗi string thể hiện lỗi</returns>
        /// Created by: TVKhang(30/09/22)
        public static string ValidateIsNotNullOrEmpty(PropertyInfo property, object propertyValue)
        {
            var isNotNullOrEmptyAttr = (IsNotNullOrEmpty?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmpty));

            if (isNotNullOrEmptyAttr != null && string.IsNullOrEmpty(propertyValue?.ToString()))
            {
                return isNotNullOrEmptyAttr.ErrorMessage;
            }
            return "";
        }

        /// <summary>
        /// Validate những thuộc tính có Attribute là PrimaryKey
        /// </summary>
        /// <param name="property"></param>
        /// <param name="propertyValue"></param>
        /// <returns>Chuỗi string thể hiện lỗi</returns>
        /// Created by: TVKhang(30/09/22)
        public static string ValidatePrimaryKey(PropertyInfo property, object propertyValue)
        {
            var primaryKeyAttr = (PrimaryKey?)Attribute.GetCustomAttribute(property, typeof(PrimaryKey));

            if (primaryKeyAttr != null && string.IsNullOrEmpty(propertyValue?.ToString()))
            {
                return primaryKeyAttr.ErrorMessage;
            }
            return "";
        }

        /// <summary>
        /// Validate những thuộc tính có Attribute là IsEmail
        /// </summary>
        /// <param name="property"></param>
        /// <param name="propertyValue"></param>
        /// <returns>Chuỗi string thể hiện lỗi</returns>
        /// Created by: TVKhang(30/09/22)
        public static string ValidateIsEmail(PropertyInfo property, object propertyValue)
        {
            if (propertyValue != null)
            {
                var primaryKeyAttr = (IsEmail?)Attribute.GetCustomAttribute(property, typeof(IsEmail));
                string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

                if (primaryKeyAttr != null && Regex.IsMatch(propertyValue?.ToString(), pattern))
                {
                    return primaryKeyAttr.ErrorMessage;
                }
            }
            return "";
        }

        /// <summary>
        /// Validate dữ liệu nhận được từ người dùng
        /// </summary>
        /// <param name="data"></param>
        /// <param name="httpContent"></param>
        /// <returns>Đối tượng thông báo dữ liệu đã đảm bảo đúng hay chưa</returns>
        /// Created by: TVKhang(30/09/22)
        public static ResponseHandle ValidateRequestData(T data, string httpContext)
        {
            // Lấy danh sách thuộc tính của đối tượng đầu vào
            var properties = typeof(T).GetProperties();
            string errMsg = "";
            QTKDErrorCode errCode = QTKDErrorCode.Exception;

            foreach (var property in properties)
            {
                string propertyName = property.Name;
                var propertyValue = property.GetValue(data);

                if (ValidateData<T>.ValidateIsNotNullOrEmpty(property, propertyValue) != "")
                {
                    errMsg = ValidateData<T>.ValidateIsNotNullOrEmpty(property, propertyValue);
                    return new ResponseHandle(false, new ErrorResult(
                        QTKDErrorCode.NotNullOrEmpty, Resource.DevMsg_Exception, Resource.UserMsg_Exception, errMsg, httpContext)
                    );
                }

                if (ValidateData<T>.ValidatePrimaryKey(property, propertyValue) != "")
                {
                    errMsg = ValidateData<T>.ValidatePrimaryKey(property, propertyValue);
                    return new ResponseHandle(false, new ErrorResult(
                        QTKDErrorCode.InvalidID, Resource.DevMsg_Exception, Resource.UserMsg_Exception, errMsg, httpContext)
                    );
                }

                if (ValidateData<T>.ValidateIsEmail(property, propertyValue) != "")
                {
                    errMsg = ValidateData<T>.ValidateIsEmail(property, propertyValue);
                    return new ResponseHandle(false, new ErrorResult(
                        QTKDErrorCode.EmailInvalid, Resource.DevMsg_Exception, Resource.UserMsg_Exception, errMsg, httpContext)
                    );
                }

            }


            return new ResponseHandle(true, null);

        }

        #endregion
    }
}
