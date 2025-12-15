using MyFirstApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MyFirstApp.ViewModels
{
    public class MovieVM
    {
        public Movie movie { get; set; } = new Movie();
        
        [Display(Name = "Image du film")]
        public IFormFile? photo { get; set; }
    }
}
