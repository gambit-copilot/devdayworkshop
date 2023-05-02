namespace TechItEasyBackend.Requests
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public string? PreferredCurrency { get; set; }

        public class OrderLine
        {
            public int ProductId { get; set; }
            public decimal Quantity { get; set; }
        }
    }
}
