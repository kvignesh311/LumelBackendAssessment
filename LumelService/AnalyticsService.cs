using LumelService.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumelService
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly AppDbContext _dbContext;

        public AnalyticsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetTotalCustomersAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Orders
                .Where(o => o.SaleDate >= startDate && o.SaleDate <= endDate)
                .Select(o => o.CustomerId)
                .Distinct()
                .CountAsync();
        }

        public async Task<int> GetTotalOrdersAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Orders
                .Where(o => o.SaleDate >= startDate && o.SaleDate <= endDate)
                .CountAsync();
        }

        public async Task<decimal> GetAverageOrderValueAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _dbContext.Orders
                .Where(o => o.SaleDate >= startDate && o.SaleDate <= endDate)
                .Select(o => o.Id)
                .ToListAsync();

            var totalRevenue = await _dbContext.OrderItems
                .Where(oi => orders.Contains(oi.OrderId))
                .SumAsync(oi =>
                    (oi.QuantitySold * oi.UnitPrice * (1 - oi.Discount)) + oi.ShippingCost
                );

            return totalRevenue / orders.Count;
        }
    }
}
