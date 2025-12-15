namespace MyFirstApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageFile { get; set; }
        public DateTime? DateAjoutMovie { get; set; }
        public int Stock { get; set; }
        
        // Foreign Key
        public int GenreId { get; set; }
        
        // Navigation Property
        public Genre? Genre { get; set; }

        public ICollection<Customer>? Customers { get; set; }
    }
}
