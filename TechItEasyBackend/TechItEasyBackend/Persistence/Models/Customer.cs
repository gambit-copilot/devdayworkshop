namespace TechItEasyBackend.Persistence.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
