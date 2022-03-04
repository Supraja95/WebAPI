using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table(name:"Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Column(TypeName ="varchar(200)")]
        [Required]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(200)")]
        [Required]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(200)")]
        [Required]
        public string EmailID { get; set; }
    }
}
