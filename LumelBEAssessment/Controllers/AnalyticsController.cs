using LumelService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LumelBEAssessment.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalyticsController : ControllerBase
{

    private readonly ILogger<AnalyticsController> _logger;

    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(ILogger<AnalyticsController> logger, IAnalyticsService analyticsService)
    {
        _logger = logger;
        _analyticsService = analyticsService;
    }

    [HttpGet("customers/count")]
    public async Task<IActionResult> GetTotalCustomers(
       DateTime startDate,
       DateTime endDate)
    {
        var count = await _analyticsService
            .GetTotalCustomersAsync(startDate, endDate);

        return Ok(new
        {
            totalCustomers = count
        });
    }

    [HttpGet("orders/count")]
    public async Task<IActionResult> GetTotalOrders(
        DateTime startDate,
        DateTime endDate)
    {
        var count = await _analyticsService
            .GetTotalOrdersAsync(startDate, endDate);

        return Ok(new
        {
            totalOrders = count
        });
    }

    [HttpGet("orders/average-value")]
    public async Task<IActionResult> GetAverageOrderValue(
        DateTime startDate,
        DateTime endDate)
    {
        var value = await _analyticsService
            .GetAverageOrderValueAsync(startDate, endDate);

        return Ok(new
        {
            averageOrderValue = value
        });
    }
}
