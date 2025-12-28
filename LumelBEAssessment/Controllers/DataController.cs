using CsvHelper;
using LumelService;
using LumelService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LumelAnalytics.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class DataController : ControllerBase
    {
        private readonly ICsvHelperLoaderService _csvHelperLoaderServiceService;
        public DataController(ICsvHelperLoaderService csvHelperLoaderServiceService)
        {
            _csvHelperLoaderServiceService = csvHelperLoaderServiceService;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            await _csvHelperLoaderServiceService.LoadAsync("/Data/salesData.csv");
            return Ok(new { message = "Manual refresh completed" });
        }
    }
}
