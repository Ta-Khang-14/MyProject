using MISA.Web08.QTKD.Common.Khang;
using MISA.Web08.QTKD.DL.Khang;

namespace MISA.WEB08.QTKD.BL.Khang
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        #region Field
        private IDepartmentDL _departmentDL;
        #endregion


        #region Contructor
        public DepartmentBL(IDepartmentDL departmentDL) : base(departmentDL)
        {
            _departmentDL = departmentDL;
        }
        #endregion
    }
}
