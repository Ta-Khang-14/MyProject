using Microsoft.AspNetCore.Mvc;
using MISA.Web08.QTKD.Common.Khang;
using MISA.WEB08.QTKD.BL.Khang;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

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
        [Produces("application/json")]
        public IActionResult MaxCodeEmployee()
        {
            ResponseHandle res = _employeeBL.MaxCodeEmployee(HttpContext.TraceIdentifier);

            // Kiểm tra xem request đã thành công hay chưa
            if (res.IsSuccess)
            {
                string data = "NV-" + res.Data.ToString();
                return StatusCode(StatusCodes.Status200OK, data);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, res.ErrorResult);
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
            ResponseHandle res = _employeeBL.EmployeesFilter(keyword, sort, offset, limit, HttpContext.TraceIdentifier);

            // Kiểm tra xem request đã thành công hay chưa
            if (res.IsSuccess)
            {
                PagingData<Employee> data = (PagingData<Employee>)res.Data;
                return StatusCode(StatusCodes.Status200OK, data);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, res.ErrorResult);
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
                // Lấy danh sách nhân viên
                ResponseHandle rs = _employeeBL.Records(HttpContext.TraceIdentifier);

                // Lấy danh sách thành công
                if (rs.IsSuccess)
                {
                    PagingData<Employee> data = (PagingData<Employee>)rs.Data;
                    List<Employee> employees = data.Data;

                    var stream = new MemoryStream();

                    using (var xlPackage = new ExcelPackage(stream))
                    {
                        var worksheet = xlPackage.Workbook.Worksheets.Add("Danh_sach_nhan_vien");

                        worksheet.Row(2).Height = 20;
                        worksheet.Row(3).Height = 20;

                        worksheet.Cells["A1"].Value = "Danh sách nhân viên";

                        // Hợp cột A1 -> J1 của dòng 1 trong sheet Danh_sach_nhan_vien
                        using (var r = worksheet.Cells["A1:J1"])
                        {
                            r.Merge = true;

                            // Định dạng kiểu chữ
                            r.Style.Font.Size = 16;
                            r.Style.Font.Bold = true;

                            // Căn chính giữa
                            r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        }


                        using (var r = worksheet.Cells["A2:J2"])
                        {
                            r.Merge = true;
                        }
                        using (var r = worksheet.Cells["A3:J3"])
                        {
                            // Định dạng kiểu chữ
                            r.Style.Font.Size = 12;
                            r.Style.Font.Bold = true;
                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            r.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                            // Căn chính giữa
                            r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                            // Định dạng border
                            r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        }
                        worksheet.Cells["A3"].Value = "STT";
                        worksheet.Cells["B3"].Value = "Mã nhân viên";
                        worksheet.Cells["C3"].Value = "Tên nhân viên";
                        worksheet.Cells["D3"].Value = "Giới tính";
                        worksheet.Cells["E3"].Value = "Ngày sinh";
                        worksheet.Cells["F3"].Value = "Vị trí";
                        worksheet.Cells["G3"].Value = "Tên phòng ban";
                        worksheet.Cells["H3"].Value = "Số tài khoản";
                        worksheet.Cells["I3"].Value = "Tên ngân hàng";
                        worksheet.Cells["J3"].Value = "Chi nhánh ngân hàng";

                        worksheet.Column(1).Width = 6;
                        worksheet.Column(2).Width = 20;
                        worksheet.Column(3).Width = 25;
                        worksheet.Column(4).Width = 12;
                        worksheet.Column(5).Width = 20;
                        worksheet.Column(6).Width = 20;
                        worksheet.Column(7).Width = 20;
                        worksheet.Column(8).Width = 20;
                        worksheet.Column(9).Width = 20;
                        worksheet.Column(10).Width = 25;

                        int row = 4;
                        int STT = 1;
                        int start = 4;
                        int end = 4;
                        foreach (var emp in employees)
                        {
                            string gender = "";
                            switch (emp.Gender)
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

                            worksheet.Cells[row, 1].Value = STT++;
                            worksheet.Cells[row, 2].Value = emp.EmployeeCode;
                            worksheet.Cells[row, 3].Value = emp.EmployeeName;
                            worksheet.Cells[row, 4].Value = gender;
                            worksheet.Cells[row, 5].Value = emp.DateOfBirth?.ToString("dd/MM/yyyy");
                            worksheet.Cells[row, 6].Value = emp.PositionName;
                            worksheet.Cells[row, 7].Value = emp.DepartmentName;
                            worksheet.Cells[row, 8].Value = emp.BankNumber;
                            worksheet.Cells[row, 9].Value = emp.BankName;
                            worksheet.Cells[row, 10].Value = emp.BankBranch;

                            // Tạo border 1 trường dữ liệu
                            var recordRow = worksheet.Cells["A" + start++ + ":J" + end++];

                            recordRow.Style.Font.Size = 12;
                            recordRow.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            recordRow.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            recordRow.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            recordRow.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            row++;
                        }

                        xlPackage.Workbook.Properties.Title = "DANH SÁCH NHÂN VIÊN";
                        xlPackage.Workbook.Properties.Author = "Tạ Việt Khang";

                        //worksheet.Cells.AutoFitColumns();
                        worksheet.Cells.Style.Font.Name = "Arial";

                        xlPackage.Save();

                    }
                    stream.Position = 0;

                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhan_vien.xlsx");
                }

                else
                {
                    // Lấy danh sách thất bại
                    return StatusCode(StatusCodes.Status500InternalServerError, rs.ErrorResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            employee.Email = employee.Email != null ? employee.Email.ToLower() : employee.Email;
            ResponseHandle rs = ValidateData<Employee>.ValidateRequestData(employee, HttpContext.TraceIdentifier);

            // Kiểm tra xem dữ liệu đầu vào đã validate hay chưa
            if (rs.IsSuccess)
            {
                if (true)
                {
                    ResponseHandle res = _employeeBL.InsertEmployee(employee, HttpContext.TraceIdentifier);

                    // Kiểm tra xem request đã được thực hiện hay chưa
                    if (res.IsSuccess)
                    {
                        var data = res.Data;
                        return StatusCode(StatusCodes.Status201Created, data);
                    }

                    // Request thất bại
                    if (res.Status == 400)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, res.ErrorResult);
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, res.ErrorResult);
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
            employee.Email = employee.Email != null ? employee.Email.ToLower() : employee.Email;
            ResponseHandle rs = ValidateData<Employee>.ValidateRequestData(employee, HttpContext.TraceIdentifier);

            //Kiểm tra xem dữ liệu đầu vào đã đưuọc validate hay chưa
            if (rs.IsSuccess)
            {
                ResponseHandle res = _employeeBL.UpdateEmployee(employeeID, employee, HttpContext.TraceIdentifier);

                // Kiểm tra xem request đã thực hiện thành công hay chưa
                if (res.IsSuccess)
                {
                    Guid emp = (Guid)res.Data;
                    return StatusCode(StatusCodes.Status201Created, emp);
                }

                // Request thất bại
                if (res.Status == 400)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, rs.ErrorResult);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, rs.ErrorResult);
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
            ResponseHandle rs = _employeeBL.DeleteEmployee(employeeID, HttpContext.TraceIdentifier);

            // Kiểm tra xem request đã thực hiện thành công hay chưa
            if (rs.IsSuccess)
            {
                return StatusCode(StatusCodes.Status201Created, employeeID);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, rs.ErrorResult);
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
            ResponseHandle rs = _employeeBL.DeleteEmployees(listEmployeeIDs, HttpContext.TraceIdentifier);

            // Kiểm tra xem request đã thực hiện thành công hay chưa
            if (rs.IsSuccess)
            {
                // Số bản ghi đã xóa
                int count = (int)rs.Data;
                return StatusCode(StatusCodes.Status200OK, count);
            }

            // Request thất bại
            if (rs.Status == 400)
            {
                return StatusCode(StatusCodes.Status400BadRequest, rs.ErrorResult);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, rs.ErrorResult);

        }
    }
}
