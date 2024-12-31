using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiberaryManagementAPI.Models
{
    public class Author
    {
        [Key]
        public int AutherId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateofBith {  get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        public ICollection<Book> Books { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

    }
}
