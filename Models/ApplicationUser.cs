using Microsoft.AspNetCore.Identity;

namespace MyFirstApp.Models
{
    // ApplicationUser hérite de IdentityUser
    // IdentityUser contient déjà : Id, UserName, Email, PasswordHash, etc.
    public class ApplicationUser : IdentityUser
    {
        // Vous pouvez ajouter des propriétés personnalisées ici
        // Par exemple :
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
    }
}
