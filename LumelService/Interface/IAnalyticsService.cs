using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumelService.Interface
{
    public interface IAnalyticsService
    {
        Task<int> GetTotalCustomersAsync(DateTime startDate, DateTime endDate);
        Task<int> GetTotalOrdersAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetAverageOrderValueAsync(DateTime startDate, DateTime endDate);
    }
}
