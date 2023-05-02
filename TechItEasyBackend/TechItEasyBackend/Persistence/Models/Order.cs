namespace TechItEasyBackend.Persistence.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer? Customer { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public string? PreferredCurrency { get; set; }
        public decimal NetPrice { get; set; }
        public decimal NetPriceInPreferredCurrency { get; set; }
        public decimal GrossPrice { get; set; }
        public decimal GrossPriceInPreferredCurrency { get; set; }
        public decimal TaxAmount => GrossPrice - NetPrice;
        public decimal TaxAmountInPreferredCurrency => GrossPriceInPreferredCurrency - NetPriceInPreferredCurrency;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public class OrderLine
        {
            public int Id { get; set; }
            public Product? Product { get; set; }
            public decimal NetPrice { get; set; }
            public decimal TaxRate { get; set; }
            public decimal TaxAmount => Math.Round(NetPrice * TaxRate, 2);
            public decimal Quantity { get; set; }
            public decimal LineTotalNetPrice => Math.Round(NetPrice * Quantity, 2);
            public decimal LineTotalGrossPrice => Math.Round((NetPrice + TaxAmount) * Quantity, 2);

            public Order? Order { get; set; }
        }
    }
}
