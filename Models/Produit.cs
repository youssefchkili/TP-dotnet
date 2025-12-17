using System.ComponentModel.DataAnnotations;

namespace MyFirstApp.Models
{
    public class Produit
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string? Nom { get; set; }
        
        public string? Description { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0")]
        public decimal Prix { get; set; }
        
        public int Stock { get; set; }
        
        public string? ImageUrl { get; set; }
    }
}
