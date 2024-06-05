using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiApp.DataAccessLayer.Model
{
    public class PersonDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BirthDay { get; set; }

        [Required]
        public string PersonCity { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }

    }
}
