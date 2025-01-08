using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteka13263.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(13)]
        public string ISBN { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        public ICollection<BookGenre> BookGenre { get; set; }

        // Foreign key to Client
        public int? ClientId { get; set; }  // Nullable in case the book is not assigned to any client
        public Client Client { get; set; }  // Navigation property to Client
        public bool IsAvilible {  get; set; }
        public DateTime WhenAvilable { get; set; }
    }
}