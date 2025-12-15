namespace MyFirstApp.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        // Navigation Property
        public ICollection<Movie>? Movies { get; set; }
    }
}
