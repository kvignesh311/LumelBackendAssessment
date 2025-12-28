using CsvHelper;
using LumelService.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Entities;
using System;
using System.Formats.Asn1;
using System.Globalization;

namespace LumelService
{
    public class DataLoaderService : BackgroundService
    {
        private readonly ICsvHelperLoaderService _csvHelperLoaderService;

        public DataLoaderService(ICsvHelperLoaderService csvHelperLoaderService)
        {
            _csvHelperLoaderService = csvHelperLoaderService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);

                try
                {
                    await _csvHelperLoaderService.LoadAsync("/Data/salesData.csv");
                }
                catch (Exception ex)
                {
                    //ToDO:Exception Logging
                }
            }
        }

      
    }
}
