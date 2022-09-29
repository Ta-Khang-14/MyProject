namespace MISA.Web08.QTKD.Common.Khang
{
    public class Department
    {
        public Guid DepartmentID { get; set; }

        public string DeparmentCode { get; set; }

        public string DepartmentName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
