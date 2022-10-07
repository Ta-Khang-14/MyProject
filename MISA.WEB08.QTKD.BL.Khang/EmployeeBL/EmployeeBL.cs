using MISA.Web08.QTKD.Common.Khang;
using MISA.Web08.QTKD.DL.Khang;
using System.Text;

namespace MISA.WEB08.QTKD.BL.Khang
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Contructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }


        #endregion

        #region Method

        /// <summary>
        /// Lấy mã nhân viên lơn nhất
        /// </summary>
        /// <returns></returns>
        public ResponseHandle MaxCodeEmployee(string traceID)
        {
            return _employeeDL.MaxCodeEmployee(traceID);
        }

        /// <summary>
        /// Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">Mã nhân viên cần xóa</param>
        /// <returns>Mã nhân viên đã xóa</returns>
        /// Created by: TVKhang(29/09/22)
        public ResponseHandle DeleteEmployee(Guid employeeID, string traceID)
        {
            return _employeeDL.DeleteEmployee(employeeID, traceID);
        }

        /// <summary>
        /// Tìm kiếm, lọc, phân trang nhân viên
        /// </summary>
        /// <param name="keyword">ID hoặc tên nhân viên cần tìm kiếm</param>
        /// <param name="sort">Sắp xếp</param>
        /// <param name="offset">Thứ tự bắt đầu lấy nhân viên</param>
        /// <param name="limit">Số lượng bản ghi muốn lấy</param>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by: TVKhang(29/09/22)
        public ResponseHandle EmployeesFilter(string? keyword, string? sort, int? offset, int? limit, string traceID)
        {
            return _employeeDL.EmployeesFilter(keyword, sort, offset, limit, traceID);
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm mới</param>
        /// <returns>ID nhân viên mới</returns>
        /// Created by: TVKhang(29/09/22)
        public ResponseHandle InsertEmployee(Employee employee, string traceID)
        {
            // Tạo 1 ID cho nhân viên mới
            employee.EmployeeID = Guid.NewGuid();

            // Kiểm tra đã tồn tại ID nhân viên hay chưa
            ResponseHandle rs;
            rs = _employeeDL.Record(employee.EmployeeID, traceID);

            // Kiểm tra xem mã nhân viên dã tồn tại hay chưa, nếu tồn tại tạo lại mã nhân viên
            while (rs.IsSuccess)
            {
                employee.EmployeeID = Guid.NewGuid();
                rs = _employeeDL.Record(employee.EmployeeID, traceID);
            }


            // Kiểm tra mã nhân viên đã tồn tại hay chưa
            rs = _employeeDL.EmployeesFilter(employee.EmployeeCode, null, 0, 10, traceID);

            if (rs.IsSuccess)
            {
                PagingData<Employee> data = (PagingData<Employee>)rs.Data;

                // Nếu mã nhân viên đã tồn tại
                if (data.TotalCount > 0)
                {
                    return new ResponseHandle(
                            false, 400, null,
                            new ErrorResult(
                                QTKDErrorCode.DupplicateCode,
                                Resource.DevMsg_InsertEmployeeFailed,
                                Resource.UserMsg_InsertEmployeeFailed,
                                "Mã nhân viên đã tồn tại",
                                traceID
                                )
                        );
                }
            }

            string codeNumber = employee.EmployeeCode.Replace("NV-", "");
            employee.EmployeeCodeNumber = codeNumber;

            return _employeeDL.InsertEmployee(employee, traceID);
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Mã nhân viên đã được cập nhật</returns>
        /// Created by: TVKhang(29/09/22)
        public ResponseHandle UpdateEmployee(Guid employeeID, Employee employee, string traceID)
        {
            // Kiểm tra mã nhân viên đã tồn tại hay chưa
            ResponseHandle rs;

            string employeeCodeQuery = $"EmployeeCode='{employee.EmployeeCode}'";
            rs = _employeeDL.EmployeesFilter(employeeCodeQuery, null, 0, 10, traceID);

            if (rs.IsSuccess)
            {
                PagingData<Employee> data = (PagingData<Employee>)rs.Data;

                // Nếu mã nhân viên đã tồn tại
                if (data.TotalCount > 1)
                {
                    return new ResponseHandle(
                            false, 400, null,
                            new ErrorResult(
                                QTKDErrorCode.DupplicateCode,
                                Resource.DevMsg_InsertEmployeeFailed,
                                Resource.UserMsg_InsertEmployeeFailed,
                                "Mã nhân viên đã tồn tại",
                                traceID
                                )
                        );
                }
            }
            string codeNumber = employee.EmployeeCode.Replace("NV-", "");
            employee.EmployeeCodeNumber = codeNumber;
            employee.IsActive = employee.IsActive != null ? employee.IsActive : true;

            return _employeeDL.UpdateEmployee(employeeID, employee, traceID);
        }

        /// <summary>
        /// Xóa nhiều nhân viên theo ID
        /// </summary>
        /// <param name="listEmployeeIDs">Danh sách ID nhân viên cần xóa</param>
        /// <returns>Số lượng nhân viên đã xóa</returns>
        public ResponseHandle DeleteEmployees(List<Guid> listEmployeeIDs, string traceID)
        {
            if (listEmployeeIDs != null)
            {
                // Chuyển list employee id thành chuỗi string
                StringBuilder listString = new StringBuilder();

                foreach (Guid employeeID in listEmployeeIDs)
                {
                    listString.Append(employeeID.ToString() + ",");
                }
                listString.Length--;

                return _employeeDL.DeleteEmployees(listString.ToString(), traceID);
            }
            return new ResponseHandle(false, 400, null, new ErrorResult(
                QTKDErrorCode.DataInputInvalid,
                Resource.DevMsg_DeleteEmployeeFailed,
                Resource.UserMsg__DeleteEmployeeFailed,
                Resource.MoreInfor_DeleteEmployeeFailed,
                traceID));
        }
        #endregion


    }
}
