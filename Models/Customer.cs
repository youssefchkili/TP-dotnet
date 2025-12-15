namespace MyFirstApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsSubscribed { get; set; }
        
        public int MembershipTypeId { get; set; }
        public MembershipType? MembershipType { get; set; }

        public ICollection<Movie>? Movies { get; set; }
    }
}
