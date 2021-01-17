using DynuIpUpdater.Abstractions;
using DynuIpUpdater.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DynuIpUpdater
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly AppConfig _appConfig;
        private readonly IIpAddressProvider _ipAddressProvider;
        private readonly IIpAddressUpdater _ipAddressUpdater;

        private string _previousIpAddress = string.Empty;

        public Worker(ILogger<Worker> logger, IOptions<AppConfig> appConfig, IIpAddressProvider ipAddressProvider, IIpAddressUpdater ipAddressUpdater)
        {
            _logger = logger;
            _appConfig = appConfig.Value;
            _ipAddressProvider = ipAddressProvider;
            _ipAddressUpdater = ipAddressUpdater;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var ipAddress = await _ipAddressProvider.GetCurrentIpAddressAsync();

                if (_previousIpAddress != ipAddress)
                {
                    await _ipAddressUpdater.UpdateIpAddressAsync(ipAddress);
                    _previousIpAddress = ipAddress;
                }

                await Task.Delay(_appConfig.IpUpdateIntervalMilliSeconds, stoppingToken);
            }
        }
    }
}
