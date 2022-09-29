using Microsoft.AspNetCore.Mvc;
using MISA.Web08.QTKD.Common.Khang;
using MISA.WEB08.QTKD.BL.Khang;

namespace MISA.Web08.QTKD.API.Khang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {

        #region Field
        private IEmployeeBL _employeeBL;
        #endregion

        #region Contructor
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Tìm kiếm, lọc, phân trang nhân viên
        /// </summary>
        /// <param name="keyword">ID hoặc tên nhân viên cần tìm kiếm</param>
        /// <param name="sort">Sắp xếp</param>
        /// <param name="offset">Thứ tự bắt đầu lấy nhân viên</param>
        /// <param name="limit">Số lượng bản ghi muốn lấy</param>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by: TVKhang(29/09/22)
        [HttpGet]
        [Route("filter")]
        public IActionResult EmployeesFilter(string? keyword, string? sort, int? offset, int? limit)
        {
            var data = _employeeBL.EmployeesFilter(keyword, sort, offset, limit);
            return StatusCode(StatusCodes.Status200OK, data);
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm mới</param>
        /// <returns>ID nhân viên mới</returns>
        /// Created by: TVKhang(29/09/22)
        [HttpPost]
        [Route("")]
        public IActionResult CreateEmployee(Employee employee)
        {
            var data = _employeeBL.InsertEmployee(employee);
            return StatusCode(StatusCodes.Status201Created, data);
        }


        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Mã nhân viên đã được cập nhật</returns>
        /// Created by: TVKhang(29/09/22)
        [HttpPut]
        [Route("")]
        public IActionResult UpdateEmployee(Guid employeeID, Employee employee)
        {
            var data = _employeeBL.UpdateEmployee(employeeID, employee);
            return StatusCode(StatusCodes.Status201Created, data);
        }

        /// <summary>
        /// Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">Mã nhân viên cần xóa</param>
        /// <returns>Mã nhân viên đã xóa</returns>
        /// Created by: TVKhang(29/09/22)
        [HttpDelete]
        [Route("")]
        public IActionResult DeleteEmployee(Guid employeeID)
        {
            var data = _employeeBL.DeleteEmployee(employeeID);
            return StatusCode(StatusCodes.Status201Created, data);
        }
        #endregion
    }
}
