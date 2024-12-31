using System.ComponentModel.DataAnnotations;

namespace LiberaryManagementAPI.Dtos
{
    public class LoanCreateDTO
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int MemberId { get; set; }
    }
}
