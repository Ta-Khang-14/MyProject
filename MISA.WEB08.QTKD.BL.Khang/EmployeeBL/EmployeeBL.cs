using MISA.Web08.QTKD.Common.Khang;
using MISA.Web08.QTKD.DL.Khang;

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
        /// Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">Mã nhân viên cần xóa</param>
        /// <returns>Mã nhân viên đã xóa</returns>
        /// Created by: TVKhang(29/09/22)
        public Guid DeleteEmployee(Guid employeeID)
        {
            return _employeeDL.DeleteEmployee(employeeID);
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
        public PagingData<Employee> EmployeesFilter(string? keyword, string? sort, int? offset, int? limit)
        {
            return _employeeDL.EmployeesFilter(keyword, sort, offset, limit);
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm mới</param>
        /// <returns>ID nhân viên mới</returns>
        /// Created by: TVKhang(29/09/22)
        public Guid InsertEmployee(Employee employee)
        {
            return _employeeDL.InsertEmployee(employee);
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Mã nhân viên đã được cập nhật</returns>
        /// Created by: TVKhang(29/09/22)
        public Guid UpdateEmployee(Guid employeeID, Employee employee)
        {
            return _employeeDL.UpdateEmployee(employeeID, employee);
        }
        #endregion


    }
}
