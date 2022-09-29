using Dapper;
using MISA.Web08.QTKD.Common.Khang;
using MySqlConnector;

namespace MISA.Web08.QTKD.DL.Khang
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {

        /// <summary>
        /// Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">Mã nhân viên cần xóa</param>
        /// <returns>Mã nhân viên đã xóa</returns>
        /// Created by: TVKhang(29/09/22)
        public Guid DeleteEmployee(Guid employeeID)
        {
            try
            {

                //Khai báo store proceduce
                string storedProceduceName = "Proc_Employee_DeleteById";

                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {
                    // Chuẩn bị tham số đầu vào
                    var parameters = new DynamicParameters();
                    parameters.Add("v_TableName", "employee");
                    parameters.Add("v_ID", employeeID);

                    // Thực hiện khởi chạy procedure
                    var numberOfAffectedRows = mysqlConnection.Execute(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                    // TH: Thành công
                    if (numberOfAffectedRows > 0)
                    {
                        return employeeID;
                    }
                    else
                    {
                        return employeeID;

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
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
            try
            {
                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {
                    // Khai báo tên stored procedure
                    string storedProcedureName = "Proc_employee_Filter";

                    // Chuẩn bị tham số cho đầu vào cho câu lệnh trên
                    var parameters = new DynamicParameters();
                    limit = limit != null ? limit : 10;
                    parameters.Add("v_Limit", limit);

                    offset = offset != null ? offset : 0;
                    parameters.Add("v_Offset", offset);

                    string whereCondition = "";
                    if (keyword != null)
                    {
                        whereCondition = $"EmployeeCode LIKE '%{keyword}%' OR EmployeeName LIKE '%{keyword}%'";
                    }
                    parameters.Add("v_Where", whereCondition);

                    string sortCondition = "";
                    if (sort != null)
                    {
                        string subString = sort.Substring(1);
                        char condition = sort[0];
                        if (condition == '-')
                        {
                            sortCondition = $"{subString} DESC";
                        }
                        else
                        {
                            sortCondition = $"{subString} ASC";
                        }

                    }
                    parameters.Add("v_Sort", sortCondition);

                    // Thực hiện lệnh gọi vào DB
                    var result = mysqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                    // Xử lý kết quả trả về từ DB
                    var employees = result.Read<Employee>().ToList();
                    var tottalEmp = result.Read<int>().Single();

                    return new PagingData<Employee>(employees, tottalEmp);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm mới</param>
        /// <returns>ID nhân viên mới</returns>
        /// Created by: TVKhang(29/09/22)
        public Guid InsertEmployee(Employee employee)
        {

            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                //khai báo store proceduce
                string storedProceduceName = "Proc_employee_Create";

                //chuẩn bị tham số đầu vào
                var employeeID = Guid.NewGuid();
                var parameters = new DynamicParameters();
                parameters.Add("v_EmployeeID", employeeID);
                parameters.Add("v_EmployeeCode", employee.EmployeeCode);
                parameters.Add("v_EmployeeName  ", employee.EmployeeName);
                parameters.Add("v_TypeOfCustomer", employee.TypeOfCustomer);
                parameters.Add("v_DateOfBirth", employee.DateOfBirth);
                parameters.Add("v_Gender", employee.Gender);
                parameters.Add("v_IdentityNumber", employee.IdentityNumber);
                parameters.Add("v_DepartmentID", employee.DepartmentID);
                parameters.Add("v_DepartmentName", employee.DepartmentName);
                parameters.Add("v_IdentityPlace", employee.IdentityPlace);
                parameters.Add("v_IdentityDate", employee.IdentityDate);
                parameters.Add("v_EmployeeAddress", employee.EmployeeAddress);
                parameters.Add("v_PhoneNumber", employee.PhoneNumber);
                parameters.Add("v_LandlineNumber", employee.LandlineNumber);
                parameters.Add("v_Email", employee.Email);
                parameters.Add("v_BankName", employee.BankName);
                parameters.Add("v_BankNumber", employee.BankNumber);
                parameters.Add("v_BankBranch", employee.BankBranch);
                parameters.Add("v_PositionName", employee.PositionName);
                parameters.Add("v_CreatedBy", "TVKHANG");
                parameters.Add("v_ModifiedBy", "TVKHANG");

                //Thực hiện gọi vào db để chạy procedure
                var numberOfAffectedRows = mysqlConnection.Execute(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // TH: Thành công
                if (numberOfAffectedRows > 0)
                {
                    return employeeID;
                }
                else  //TH2: Thất bại
                {
                    return employeeID;
                }
            }
        }

        /// <summary>
        /// Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">Mã nhân viên cần xóa</param>
        /// <returns>Mã nhân viên đã xóa</returns>
        /// Created by: TVKhang(29/09/22)
        public Guid UpdateEmployee(Guid employeeID, Employee employee)
        {
            try
            {
                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {
                    //khai báo store proceduce
                    string storedProceduceName = "Proc_employee_Update";

                    //chuẩn bị tham số đầu vào
                    var parameters = new DynamicParameters();
                    parameters.Add("v_EmployeeID", employeeID);
                    parameters.Add("v_EmployeeCode", employee.EmployeeCode);
                    parameters.Add("v_EmployeeName  ", employee.EmployeeName);
                    parameters.Add("v_TypeOfCustomer", employee.TypeOfCustomer);
                    parameters.Add("v_DateOfBirth", employee.DateOfBirth);
                    parameters.Add("v_Gender", employee.Gender);
                    parameters.Add("v_IdentityNumber", employee.IdentityNumber);
                    parameters.Add("v_DepartmentID", employee.DepartmentID);
                    parameters.Add("v_DepartmentName", employee.DepartmentName);
                    parameters.Add("v_IdentityPlace", employee.IdentityPlace);
                    parameters.Add("v_IdentityDate", employee.IdentityDate);
                    parameters.Add("v_EmployeeAddress", employee.EmployeeAddress);
                    parameters.Add("v_PhoneNumber", employee.PhoneNumber);
                    parameters.Add("v_LandlineNumber", employee.LandlineNumber);
                    parameters.Add("v_Email", employee.Email);
                    parameters.Add("v_BankName", employee.BankName);
                    parameters.Add("v_BankNumber", employee.BankNumber);
                    parameters.Add("v_BankBranch", employee.BankBranch);
                    parameters.Add("v_PositionName", employee.PositionName);
                    parameters.Add("v_ModifiedBy", "TVKHANG");

                    //Thực hiện gọi vào db để chạy procedure
                    var numberOfAffectedRows = mysqlConnection.Execute(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                    // TH: Thành công
                    if (numberOfAffectedRows > 0)
                    {
                        return employeeID;
                    }
                    else  //TH2: Thất bại
                    {
                        return employeeID;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
