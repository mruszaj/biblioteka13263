using biblioteka13263.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace biblioteka13263.Models
{
    public class BookCreate
    {
        [Required]
        [MaxLength(13)]
        public string ISBN { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required(ErrorMessage = "Select at least one genre")]
        public List<int> SelectedGenreIds { get; set; }

        public List<SelectListItem> Genres { get; set; } 

    }
}

