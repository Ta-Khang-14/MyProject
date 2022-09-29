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
        public Guid DeleteEmployee(Guid employeeID)
        {
            return _employeeDL.DeleteEmployee(employeeID);
        }

        public PagingData<Employee> EmployeesFilter(string? keyword, string? sort, int? offset, int? limit)
        {
            return _employeeDL.EmployeesFilter(keyword, sort, offset, limit);
        }

        public Guid InsertEmployee(Employee employee)
        {
            return _employeeDL.InsertEmployee(employee);
        }

        public Guid UpdateEmployee(Guid employeeID, Employee employee)
        {
            return _employeeDL.UpdateEmployee(employeeID, employee);
        }
        #endregion


    }
}
