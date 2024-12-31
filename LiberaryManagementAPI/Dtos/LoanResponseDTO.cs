namespace LiberaryManagementAPI.Dtos
{
    public class LoanResponseDTO
    {
        public int LoanId { get; set; }
        public string BookTitle { get; set; }
        public string MemberName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
