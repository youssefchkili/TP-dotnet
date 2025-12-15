namespace MyFirstApp.Models
{
    public class MembershipType
    {
        public int Id { get; set; }
        public float SignupFee { get; set; }
        public int DurationInMonths { get; set; }
        public float DiscountRate { get; set; }

        // Navigation Property
        public ICollection<Customer>? Customers { get; set; }
    }
}
