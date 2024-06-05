using System.ComponentModel.DataAnnotations;

namespace ApiApp.DataAccessLayer.Model
{
    public class Department
    {
        public Department()
        {
            Positions = new HashSet<Position>();
        }
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        public virtual ICollection<Position> Positions { get; set; }

    }
}
