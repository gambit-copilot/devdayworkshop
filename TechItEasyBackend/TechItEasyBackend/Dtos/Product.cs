namespace TechItEasyBackend.Dtos
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

        public static Product FromModel(Persistence.Models.Product model)
        {
            return new Product {
                Id = model.Id,
                ProductCode = model.ProductCode,
                Name = model.Name,
                Description = model.Description,
                NetPrice = model.NetPrice,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
            };
        }
    }
}
