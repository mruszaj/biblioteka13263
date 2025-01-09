using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace biblioteka13263.Models
{
    public class User:IdentityUser
    {
        //[Key]
        //public int Id { get; set; }
        //[Required]
        //public string Email { get; set; }       
        //[Required]
        //public string Password { get; set; }
        [DefaultValue(false)]
        public bool Administrator { get; set; }

    }
}