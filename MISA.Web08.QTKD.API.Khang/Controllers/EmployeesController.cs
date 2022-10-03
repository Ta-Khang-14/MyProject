using Microsoft.AspNetCore.Mvc;
using MISA.Web08.QTKD.Common.Khang;
using MISA.WEB08.QTKD.BL.Khang;
using System.Text;

namespace MISA.Web08.QTKD.API.Khang.Controllers
{
    [Route("api/v1/[controller]")]
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
        /// Lấy mã nhân viên lơn nhất
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("max-code")]
        public IActionResult MaxCodeEmployee()
        {
            var data = _employeeBL.MaxCodeEmployee();
            if (data != "")
            {
                return StatusCode(StatusCodes.Status200OK, "NV-" + data);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResult.Generate500Error(HttpContext.TraceIdentifier));
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
        [HttpGet]
        [Route("filter")]
        public IActionResult Employees([FromQuery] string? keyword, [FromQuery] string? sort, [FromQuery] int? offset, [FromQuery] int? limit)
        {
            var data = _employeeBL.EmployeesFilter(keyword, sort, offset, limit);
            if (data != null)
            {
                return StatusCode(StatusCodes.Status200OK, data);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResult.Generate500Error(HttpContext.TraceIdentifier));
        }

        /// <summary>
        /// Xuất dữ liệu nhân viên ra file excel
        /// </summary>
        /// <returns>File excel chứa dữ liệu nhân viên</returns>
        [HttpGet]
        [Route("exportEmployees")]
        public IActionResult EmployeeExportFile()
        {
            try
            {
                List<Employee> ListEmployee = new List<Employee>();
                ListEmployee = _employeeBL.Records().Data;

                StringBuilder str = new StringBuilder();
                str.Append("<table border=`" + "1px" + "`b>");
                str.Append("<tr>");
                str.Append("<td><b><font face=Arial Narrow size=3>STT</font></b></td>");
                str.Append("<td><b><font face=Arial Narrow size=3>Mã nhân viên</font></b></td>");
                str.Append("<td><b><font face=Arial Narrow size=3>Họ tên nhân viên</font></b></td>");
                str.Append("<td><b><font face=Arial Narrow size=3>Giới tính</font></b></td>");
                str.Append("<td><b><font face=Arial Narrow size=3>Ngày sinh</font></b></td>");
                str.Append("<td><b><font face=Arial Narrow size=3>Chức danh</font></b></td>");
                str.Append("<td><b><font face=Arial Narrow size=3>Tên đơn vị</font></b></td>");
                str.Append("<td><b><font face=Arial Narrow size=3>Số tài khoản</font></b></td>");
                str.Append("<td><b><font face=Arial Narrow size=3>Tên ngân hàng</font></b></td>");
                str.Append("</tr>");

                int index = 1;
                foreach (Employee val in ListEmployee)
                {
                    string gender = "";
                    switch (val.Gender)
                    {
                        case Gender.Male:
                            gender = "Nam";
                            break;
                        case Gender.Female:
                            gender = "Nữ";
                            break;
                        case Gender.Other:
                            gender = "Khác";
                            break;
                    }

                    str.Append("<tr>");
                    str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + index.ToString() + "</font></td>");
                    str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.EmployeeCode.ToString() + "</font></td>");
                    str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.EmployeeName.ToString() + "</font></td>");
                    str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + gender + "</font></td>");
                    str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.DateOfBirth?.ToString() + "</font></td>");
                    str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.PositionName?.ToString() + "</font></td>");
                    str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.DepartmentName?.ToString() + "</font></td>");
                    str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.BankNumber?.ToString() + "</font></td>");
                    str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.BankName?.ToString() + "</font></td>");
                    str.Append("</tr>");
                    index++;
                }


                str.Append("</table>");
                HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=Danh_Sach_Nhan_Vien.xls");
                this.Response.ContentType = "application/vnd.ms-excel";
                byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());

                return File(temp, "application/vnd.ms-excel");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorResult.Generate500Error(HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm mới</param>
        /// <returns>ID nhân viên mới</returns>
        /// Created by: TVKhang(29/09/22)
        [HttpPost]
        [Route("")]
        public IActionResult Employee([FromBody] Employee employee)
        {
            ResponseHandle rs = ValidateData<Employee>.ValidateRequestData(employee, HttpContext.TraceIdentifier);

            if (rs.IsSuccess)
            {
                // Kiểm tra trùng mã nhân viên
                if (true)
                {
                    var data = _employeeBL.InsertEmployee(employee);
                    if (data != Guid.Empty)
                    {
                        return StatusCode(StatusCodes.Status201Created, data);
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, ErrorResult.Generate500Error(HttpContext.TraceIdentifier));
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, rs.ErrorResult);
            }
        }


        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Mã nhân viên đã được cập nhật</returns>
        /// Created by: TVKhang(29/09/22)
        [HttpPut]
        [Route("{employeeID}")]
        public IActionResult Employee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            ResponseHandle rs = ValidateData<Employee>.ValidateRequestData(employee, HttpContext.TraceIdentifier);

            //Nếu validate thành công
            if (rs.IsSuccess)
            {
                var data = _employeeBL.UpdateEmployee(employeeID, employee);
                if (data != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status201Created, data);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorResult.Generate500Error(HttpContext.TraceIdentifier));
            }
            else
            {
                // Validate không thành công
                return StatusCode(StatusCodes.Status400BadRequest, rs.ErrorResult);
            }
        }

        /// <summary>
        /// Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">Mã nhân viên cần xóa</param>
        /// <returns>Mã nhân viên đã xóa</returns>
        /// Created by: TVKhang(29/09/22)
        [HttpDelete]
        [Route("{employeeID}")]
        public IActionResult Employee([FromRoute] Guid employeeID)
        {
            var data = _employeeBL.DeleteEmployee(employeeID);
            if (data != Guid.Empty)
            {
                return StatusCode(StatusCodes.Status201Created, data);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResult.Generate500Error(HttpContext.TraceIdentifier));
        }
        #endregion

        /// <summary>
        /// Xóa nhiều nhân viên theo ID
        /// </summary>
        /// <param name="listEmployeeIDs">Danh sách ID nhân viên cần xóa</param>
        /// <returns>Số lượng nhân viên đã xóa</returns>
        [HttpPost]
        [Route("delete-batch")]
        public IActionResult Employees([FromBody] List<Guid> listEmployeeIDs)
        {
            try
            {
                int result = _employeeBL.DeleteEmployees(listEmployeeIDs);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorResult.Generate500Error(HttpContext.TraceIdentifier));
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorResult.Generate500Error(HttpContext.TraceIdentifier));
            }
        }
    }
}
