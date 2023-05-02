using Microsoft.EntityFrameworkCore;
using TechItEasyBackend.Persistence.Context;
using TechItEasyBackend.Persistence.Models;
using TechItEasyBackend.Requests;

namespace TechItEasyBackend.Persistence
{
    public class StoreRepository
    {
        private readonly StoreDbContext _context;

        public StoreRepository(StoreDbContext context)
        {
            _context = context;
        }

        internal async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        internal async Task<Customer?> GetCustomer(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => Equals(x.Id, id));
        }

        internal async Task CreateCustomer(CreateCustomerRequest request)
        {
            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = DateOnly.ParseExact(request.DateOfBirth!, Constants.DateFormat),
                Email = request.Email,
            };

            var timeStamp = DateTime.UtcNow;
            customer.CreatedAt = timeStamp;
            customer.UpdatedAt = timeStamp;

            _context.Add(customer);
            await _context.SaveChangesAsync();
        }

        internal async Task UpdateCustomer(int id, CreateCustomerRequest request)
        {
            var existingCustomer = await GetCustomer(id);
            if (existingCustomer == null)
                throw new NotSupportedException("Creating a new customer is not supported through this operation.");

            existingCustomer.FirstName = request.FirstName;
            existingCustomer.LastName = request.LastName;
            existingCustomer.DateOfBirth = DateOnly.ParseExact(request.DateOfBirth!, Constants.DateFormat);
            existingCustomer.Email = request.Email;

            existingCustomer.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        internal async Task DeleteCustomer(int id)
        {
            var existingCustomer = await GetCustomer(id);
            if (existingCustomer == null)
                throw new Exception($"Customer {id} not found");

            _context.Remove(existingCustomer);
            await _context.SaveChangesAsync();
        }

        internal async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        internal async Task<Order?> GetOrder(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => Equals(x.Id, id));
        }

        internal async Task CreateOrder(CreateOrderRequest request)
        {
            var customer = await GetCustomer(request.CustomerId);
            var order = new Order
            {
                Customer = customer,
                PreferredCurrency = request.PreferredCurrency ?? Constants.DefaultCurrency,
            };

            // Add order lines
            foreach (var lineToAdd in request.OrderLines)
            {
                var product = await GetProduct(lineToAdd.ProductId);
                order.OrderLines.Add(new Order.OrderLine
                {
                    Order = order,
                    Product = product,
                    NetPrice = product?.NetPrice ?? 0,
                    Quantity = lineToAdd.Quantity,
                    TaxRate = GetTaxRate(),
                });
            }

            // Calculate totals for order
            order.NetPrice = order.OrderLines.Sum(x => x.LineTotalNetPrice);
            order.GrossPrice = order.OrderLines.Sum(x => x.LineTotalGrossPrice);
            order.NetPriceInPreferredCurrency = GetPriceInCurrency(order.NetPrice, order.PreferredCurrency ?? Constants.DefaultCurrency);
            order.GrossPriceInPreferredCurrency = GetPriceInCurrency(order.GrossPrice, order.PreferredCurrency ?? Constants.DefaultCurrency);

            var timeStamp = DateTime.UtcNow;
            order.CreatedAt = timeStamp;
            order.UpdatedAt = timeStamp;

            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        internal async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        internal async Task<Product?> GetProduct(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => Equals(x.Id, id));
        }

        internal async Task CreateProduct(CreateProductRequest request)
        {
            var product = new Product
            {
                ProductCode = request.ProductCode,
                Name = request.Name,
                Description = request.Description,
                NetPrice = request.NetPrice,
            };

            var timeStamp = DateTime.UtcNow;
            product.CreatedAt = timeStamp;
            product.UpdatedAt = timeStamp;

            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        internal async Task UpdateProduct(int id, CreateProductRequest request)
        {
            var existingProduct = await GetProduct(id);
            if (existingProduct == null)
                throw new NotSupportedException("Creating a new product is not supported through this operation.");

            existingProduct.ProductCode = request.ProductCode;
            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.NetPrice = request.NetPrice;

            existingProduct.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        internal async Task DeleteProduct(int id)
        {
            var existingProduct = await GetProduct(id);
            if (existingProduct == null)
                throw new Exception($"Product {id} not found");

            _context.Remove(existingProduct);
            await _context.SaveChangesAsync();
        }

        private static decimal GetTaxRate()
        {
            // In reality, this would return different values depending on delivery country and product type
            return 0.24m;
        }

        private static decimal GetPriceInCurrency(decimal priceInEur, string preferredCurrency)
        {
            var exchangeRate = GetExchangeRate("EUR", preferredCurrency);
            return priceInEur * exchangeRate;
        }

        private static decimal GetExchangeRate(string fromCurrency, string toCurrency)
        {
            if (fromCurrency == "EUR")
            {
                switch (toCurrency)
                {
                    case "EUR":
                        return 1m;
                    case "SEK":
                        return 11.2m;
                    case "USD":
                        return 1.1m;
                }
            }
            throw new NotSupportedException($"Unsupported currency");
        }
    }
}
