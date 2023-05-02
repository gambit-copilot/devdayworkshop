namespace TechItEasyBackend.Requests
{
    public class CreateProductRequest
    {
        public string? ProductCode { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal NetPrice { get; set; }
    }
}
