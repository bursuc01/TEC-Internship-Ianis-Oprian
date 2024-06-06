using ApiApp.DataAccessLayer.Model;

namespace ApiApp.DataAccessLayer.ObjectModel
{
    public class DepartmentCreation
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public virtual ICollection<int> PositionIds { get; set; }
    }
}
