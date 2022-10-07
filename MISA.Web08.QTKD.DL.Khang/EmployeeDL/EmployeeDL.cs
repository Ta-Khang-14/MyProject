using Dapper;
using MISA.Web08.QTKD.Common.Khang;
using MySqlConnector;


namespace MISA.Web08.QTKD.DL.Khang
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {

        /// <summary>
        /// Lấy mã nhân viên lơn nhất
        /// </summary>
        /// <returns></returns>
        public ResponseHandle MaxCodeEmployee(string traceID)
        {
            try
            {
                //Khai báo store proceduce
                string storedProceduceName = "Proc_GetMaxCode";

                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {
                    // Chuẩn bị tham số đầu vào
                    var parameters = new DynamicParameters();
                    parameters.Add("v_TableName", "employee");
                    parameters.Add("v_ColumnCode", "EmployeeCodeNumber");

                    string employeeCode = mysqlConnection.QueryFirstOrDefault<string>(storedProceduceName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return new ResponseHandle(true, 200, employeeCode, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
            }
        }

        /// <summary>
        /// Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">Mã nhân viên cần xóa</param>
        /// <returns>Mã nhân viên đã xóa</returns>
        /// Created by: TVKhang(29/09/22)
        public ResponseHandle DeleteEmployee(Guid employeeID, string traceID)
        {
            try
            {

                //Khai báo store proceduce
                string storedProceduceName = "Proc_Employee_DeleteById";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("v_TableName", "employee");
                parameters.Add("v_ID", employeeID);

                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {
                    // chưa mở kết nối thì open
                    if (mysqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        mysqlConnection.Open();
                    }
                    using (var mysqlTransaction = mysqlConnection.BeginTransaction())
                    {
                        try
                        {
                            // Thực hiện khởi chạy procedure
                            var numberOfAffectedRows = mysqlConnection.Execute(storedProceduceName, parameters, mysqlTransaction, commandType: System.Data.CommandType.StoredProcedure);

                            // TH: Thành công
                            if (numberOfAffectedRows > 0)
                            {
                                mysqlTransaction.Commit();
                                return new ResponseHandle(true, 200, employeeID, null);
                            }
                            else
                            {
                                mysqlTransaction.Rollback();
                                return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            mysqlTransaction.Rollback();
                            return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
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
        public ResponseHandle EmployeesFilter(string? keyword, string? sort, int? offset, int? limit, string traceID)
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

                    object temp = new PagingData<Employee>(employees, tottalEmp);

                    return new ResponseHandle(true, 200, new PagingData<Employee>(employees, tottalEmp), null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
            }
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm mới</param>
        /// <returns>ID nhân viên mới</returns>
        /// Created by: TVKhang(29/09/22)
        public ResponseHandle InsertEmployee(Employee employee, string traceID)
        {
            try
            {
                //khai báo store proceduce
                string storedProceduceName = "Proc_employee_Create";

                //chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("v_EmployeeID", employee.EmployeeID);
                parameters.Add("v_EmployeeCode", employee.EmployeeCode);
                parameters.Add("v_EmployeeCodeNumber", employee.EmployeeCodeNumber);
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
                parameters.Add("v_CreatedDate", DateTime.Now);
                parameters.Add("v_ModifiedBy", "TVKHANG");
                parameters.Add("v_ModifiedDate", DateTime.Now);


                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {
                    // chưa mở kết nối thì open
                    if (mysqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        mysqlConnection.Open();
                    }

                    using (var mysqlTransaction = mysqlConnection.BeginTransaction())
                    {

                        try
                        {
                            //Thực hiện gọi vào db để chạy procedure
                            var numberOfAffectedRows = mysqlConnection.Execute(storedProceduceName, parameters, mysqlTransaction, commandType: System.Data.CommandType.StoredProcedure);

                            // TH: Thành công
                            if (numberOfAffectedRows > 0)
                            {
                                mysqlTransaction.Commit();
                                return new ResponseHandle(true, 201, employee.EmployeeID, null);
                            }
                            else  //TH2: Thất bại
                            {
                                mysqlTransaction.Rollback();
                                return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
            }
        }

        /// <summary>
        /// Sửa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">Mã nhân viên cần xóa</param>
        /// <returns>Mã nhân viên đã xóa</returns>
        /// Created by: TVKhang(29/09/22)
        public ResponseHandle UpdateEmployee(Guid employeeID, Employee employee, string traceID)
        {
            try
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
                parameters.Add("v_EmployeeCodeNumber", employee.EmployeeCodeNumber);
                parameters.Add("v_ModifiedBy", "TVKHANG");
                parameters.Add("v_IsActive", employee.IsActive);

                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {
                    // chưa mở kết nối thì open
                    if (mysqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        mysqlConnection.Open();
                    }
                    using (var mysqlTransaction = mysqlConnection.BeginTransaction())
                    {
                        try
                        {
                            //Thực hiện gọi vào db để chạy procedure
                            var numberOfAffectedRows = mysqlConnection.Execute(storedProceduceName, parameters, mysqlTransaction, commandType: System.Data.CommandType.StoredProcedure);

                            // TH: Thành công
                            if (numberOfAffectedRows > 0)
                            {
                                mysqlTransaction.Commit();
                                return new ResponseHandle(true, 200, employeeID, null);
                            }
                            else  //TH2: Thất bại
                            {
                                mysqlTransaction.Rollback();
                                return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
            }
        }

        /// <summary>
        /// Xóa nhiều nhân viên theo ID
        /// </summary>
        /// <param name="listEmployeeIDs">Danh sách ID nhân viên cần xóa</param>
        /// <returns>Số lượng nhân viên đã xóa</returns>
        public ResponseHandle DeleteEmployees(string listEmployeeIDs, string traceID)
        {
            try
            {
                //Khai báo store proceduce
                string storedProceduceName = "Proc_employee_DeleteEmployees";
                var parameters = new DynamicParameters();
                parameters.Add("v_EmployeeIDs", listEmployeeIDs);

                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {

                    // chưa mở kết nối thì open
                    if (mysqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        mysqlConnection.Open();
                    }
                    using (var mysqlTransaction = mysqlConnection.BeginTransaction())
                    {
                        try
                        {
                            var numberOfAffectedRows = mysqlConnection.Execute(storedProceduceName, parameters, mysqlTransaction, commandType: System.Data.CommandType.StoredProcedure);
                            if (numberOfAffectedRows > 0)
                            {
                                mysqlTransaction.Commit();
                                return new ResponseHandle(true, 200, numberOfAffectedRows, null);
                            }
                            else
                            {
                                mysqlTransaction.Rollback();
                                return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            mysqlTransaction.Rollback();
                            return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseHandle(false, 500, null, ErrorResult.Generate500Error(traceID));
            }
        }

    }
}

