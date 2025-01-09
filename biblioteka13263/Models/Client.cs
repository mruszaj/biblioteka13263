using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace biblioteka13263.Models
{
    public class Client
    {
        
        public string Id { get; set; }
        public string Email { get; set; }
        //[Required]
        //public string Name { get; set; }
        //[Required]
        //public string Surname { get; set; }
        //[Required]
        //public string Phone { get; set; }
        public List<Book> Books { get; set; }



    }
}