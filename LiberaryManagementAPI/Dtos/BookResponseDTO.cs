namespace LiberaryManagementAPI.Dtos
{
    public class BookResponseDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Quantity { get; set; }
        public int AvailableCopies { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
    }
}
