using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace biblioteka13263.Models
{
    public class Worker
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
        public bool Type { get; set; }
        [Required]
        public string Password { get; set; }
    }
}