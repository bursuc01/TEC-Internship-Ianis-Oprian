using ApiApp.DataAccessLayer.Model;

namespace ApiApp.DataAccessLayer.ObjectModel
{
    public class DepartmentCreation
    {
        public DepartmentCreation() 
        {
            PositionIds = new List<int>();
        }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public virtual ICollection<int> PositionIds { get; set; }
    }
}
