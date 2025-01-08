using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace biblioteka13263.Models

{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<BookGenre> BookGenre { get; set; }
    }
}
