namespace TechItEasyBackend.Persistence.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductCode { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal NetPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
