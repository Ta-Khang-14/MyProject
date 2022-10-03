using MISA.Web08.QTKD.Common.Khang;

namespace MISA.Web08.QTKD.DL.Khang
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Tìm kiếm, lọc, phân trang nhân viên
        /// </summary>
        /// <param name="keyword">ID hoặc tên nhân viên cần tìm kiếm</param>
        /// <param name="sort">Sắp xếp</param>
        /// <param name="offset">Thứ tự bắt đầu lấy nhân viên</param>
        /// <param name="limit">Số lượng bản ghi muốn lấy</param>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by: TVKhang(29/09/22)
        public PagingData<Employee> EmployeesFilter(string? keyword, string? sort, int? offset, int? limit);

        /// <summary>
        /// Lấy mã nhân viên lơn nhất
        /// </summary>
        /// <returns></returns>
        public string MaxCodeEmployee();

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm mới</param>
        /// <returns>ID nhân viên mới</returns>
        /// Created by: TVKhang(29/09/22)
        public Guid InsertEmployee(Employee employee);

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Mã nhân viên đã được cập nhật</returns>
        /// Created by: TVKhang(29/09/22)
        public Guid UpdateEmployee(Guid employeeID, Employee employee);

        /// <summary>
        /// Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">Mã nhân viên cần xóa</param>
        /// <returns>Mã nhân viên đã xóa</returns>
        /// Created by: TVKhang(29/09/22)
        public Guid DeleteEmployee(Guid employeeID);

        /// <summary>
        /// Xóa nhiều nhân viên theo ID
        /// </summary>
        /// <param name="listEmployeeIDs">Danh sách ID nhân viên cần xóa</param>
        /// <returns>Số lượng nhân viên đã xóa</returns>
        public int DeleteEmployees(List<Guid> listEmployeeIDs);

    }
}
