namespace TechItEasyBackend.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public List<OrderLineDto> OrderLines { get; set; } = new List<OrderLineDto>();
        public string? PreferredCurrency { get; set; }
        public decimal NetPrice { get; set; }
        public decimal NetPriceInPreferredCurrency { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxAmountInPreferredCurrency { get; set; }
        public decimal GrossPrice { get; set; }
        public decimal GrossPriceInPreferredCurrency { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static OrderDto FromModel(Persistence.Models.Order model)
        {
            return new OrderDto
            {
                Id = model.Id,
                CustomerId = model.Customer?.Id,
                OrderLines = new List<OrderLineDto>(model.OrderLines.Select(x => OrderLineDto.FromModel(x))),
                PreferredCurrency = model.PreferredCurrency,
                NetPrice = model.NetPrice,
                NetPriceInPreferredCurrency = model.NetPriceInPreferredCurrency,
                TaxAmount = model.TaxAmount,
                TaxAmountInPreferredCurrency = model.TaxAmountInPreferredCurrency,
                GrossPrice = model.GrossPrice,
                GrossPriceInPreferredCurrency = model.GrossPriceInPreferredCurrency,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
            };
        }

        public class OrderLineDto
        {
            public int Id { get; set; }
            public int? ProductId { get; set; }
            public decimal NetPrice { get; set; }
            public decimal TaxRate { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal Quantity { get; set; }
            public decimal LineTotalNetPrice { get; set; }
            public decimal LineTotalGrossPrice { get; set; }

            public static OrderLineDto FromModel(Persistence.Models.Order.OrderLine model)
            {
                return new OrderLineDto
                {
                    Id = model.Id,
                    ProductId = model.Product?.Id,
                    NetPrice = model.NetPrice,
                    TaxRate = model.TaxRate,
                    TaxAmount = model.TaxAmount,
                    Quantity = model.Quantity,
                    LineTotalNetPrice = model.LineTotalNetPrice,
                    LineTotalGrossPrice = model.LineTotalGrossPrice,
                };
            }
        }
    }
}
