using CsvHelper;
using LumelService.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumelService
{
    public class CsvHelperLoaderService : ICsvHelperLoaderService
    {
        private readonly AppDbContext _dbContext;

        public CsvHelperLoaderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task LoadAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            await foreach (dynamic row in csv.GetRecordsAsync<dynamic>())
            {
                string customerEmail = row.Customer_Email;

                var customer = await _dbContext.Customers
                    .FirstOrDefaultAsync(c => c.Email == customerEmail);

                if (customer == null)
                {
                    customer = new Customer
                    {
                        Name = row.Customer_Name,
                        Email = row.Customer_Email,
                        Address = row.Customer_Address,
                    };

                    _dbContext.Customers.Add(customer);
                }
                else
                {
                    customer.Name = row.Customer_Name;
                    customer.Address = row.Customer_Address;
                }

                await _dbContext.SaveChangesAsync();

                string productName = row.Product_Name;
                string category = row.Category;

                var product = await _dbContext.Products
                    .FirstOrDefaultAsync(p =>
                        p.Name == productName && p.Category == category);

                if (product == null)
                {
                    product = new Product
                    {
                        Name = productName,
                        Category = category,
                    };

                    _dbContext.Products.Add(product);
                }

                await _dbContext.SaveChangesAsync();

                var order = new Order
                {
                    CustomerId = customer.Id,
                    Region = row.Region,
                    PaymentMethod = row.Payment_Method,
                    SaleDate = DateTime.Parse(row.Date_of_Sale)
                };

                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();

                var item = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    QuantitySold = int.Parse(row.Quantity_Sold),
                    UnitPrice = decimal.Parse(row.Unit_Price),
                    Discount = decimal.Parse(row.Discount),
                    ShippingCost = decimal.Parse(row.Shipping_Cost)
                };

                _dbContext.OrderItems.Add(item);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
