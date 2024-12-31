using System.ComponentModel.DataAnnotations;

namespace LiberaryManagementAPI.Dtos
{
    public class BookCreateDTO
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public string AuthorName { get; set; }  

        [Required]
        public string CategoryName { get; set; }
    }
}
