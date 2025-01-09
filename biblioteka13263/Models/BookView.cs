using System.ComponentModel.DataAnnotations;

namespace biblioteka13263.Models
{
    public class BookView
    {
        public int Id { get; set; }
        public string ISBN { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }
        public string Genres { get; set; }

        public string ClientEmail { get; set; }  
        public bool IsAvilible { get; set; }
        public DateTime WhenAvilable { get; set; }
    }
}
