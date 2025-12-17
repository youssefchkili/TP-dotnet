using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstApp.Models
{
    public class PanierParUser
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string? UserID { get; set; }
        
        [Required]
        public Guid ProduitId { get; set; }
        
        public int Quantite { get; set; } = 1;
        
        // Navigation properties
        [ForeignKey("UserID")]
        public ApplicationUser? User { get; set; }
        
        [ForeignKey("ProduitId")]
        public Produit? Produit { get; set; }
    }
}
