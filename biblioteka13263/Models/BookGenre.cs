using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteka13263.Models
{
    public class BookGenre
    {

        public int BookId { get; set; }  
        public Book Book { get; set; }  

        public int GenreId { get; set; }  
        public Genre Genre { get; set; }  
    }
}
