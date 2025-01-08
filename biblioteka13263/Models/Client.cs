using System.ComponentModel.DataAnnotations;

namespace biblioteka13263.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Phone { get; set; }
        public List<Book> Books { get; set; }
        [Required]
        public string Password { get; set; }

    }
}