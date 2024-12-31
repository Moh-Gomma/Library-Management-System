namespace LiberaryManagementAPI.Dtos
{
    public class MemberResponseDTO
    {
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public int ActiveLoans { get; set; }
    }
}
