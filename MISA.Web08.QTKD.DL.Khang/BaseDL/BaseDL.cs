using Dapper;
using MISA.Web08.QTKD.Common.Khang;
using MySqlConnector;

namespace MISA.Web08.QTKD.DL.Khang
{
    public class BaseDL<T> : IBaseDL<T>
    {

        /// <summary>
        /// Lấy tất cả bản ghi của 1 bảng
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi của 1 mảng</returns>
        /// Created by: TVK(29/09/22)
        public PagingData<T> Records()
        {
            try
            {

                // Khai báo tên stored procedure
                string storedProcedureName = "Proc_GetAll";

                // Khởi tạo kết nối tới DB MySQL
                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {
                    // Chuẩn bị tham số cho đầu vào cho câu lệnh trên
                    var parameters = new DynamicParameters();
                    parameters.Add("v_TableName", typeof(T).Name);

                    // Thực hiện lệnh gọi vào DB
                    var result = mysqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                    // Xử lý dữ liệu trả về
                    var records = result.Read<T>().ToList();
                    var totalCount = result.Read<int>().Single();

                    return new PagingData<T>(records, totalCount);
                }
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
                return null;
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết của 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi</param>
        /// <returns>Chi tiết 1 bản ghi</returns>
        /// Created by: TVK(29/09/22)
        public T Record(Guid recordID)
        {
            try
            {
                // Khai báo tên stored procedure
                string storedProcedureName = "Proc_GetByID";

                // Chuẩn bị tham số cho đầu vào cho câu lệnh trên
                var parameters = new DynamicParameters();
                parameters.Add("v_TableName", typeof(T).Name);
                parameters.Add("v_ID", typeof(T).Name + "ID = '" + recordID + "'");

                // Thực hiện lệnh gọi vào DB
                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {
                    var record = mysqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return record;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return default(T);
            }
        }
    }
}
