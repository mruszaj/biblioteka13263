using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteka13263.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ISBN { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }
        public ICollection<Genre> Genres  { get; set; }
        public bool IsAvilible {  get; set; }
        public DateTime WhenAvilable { get; set; }
    }
}