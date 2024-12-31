using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiberaryManagementAPI.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        [StringLength(13)]
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;
        public ICollection<Loan> loans { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

    }
}
