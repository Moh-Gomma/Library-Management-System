using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiberaryManagementAPI.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; } = false;
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }




    }
}
