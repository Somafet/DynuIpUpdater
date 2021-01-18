using DynuIpUpdater.Abstractions;
using DynuIpUpdater.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DynuIpUpdater.Implementation
{
    public class DynDnsIpAddressProvider : IIpAddressProvider
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly ILogger<DynDnsIpAddressProvider> _logger;
        private readonly AppConfig _appConfig;

        private string _previousIpAddress;
        private string _currentIpAddress;

        public DynDnsIpAddressProvider(ILogger<DynDnsIpAddressProvider> logger, IOptions<AppConfig> appConfig)
        {
            _logger = logger;
            _appConfig = appConfig.Value;
        }

        public async Task<string> FetchCurrentIpAddressAsync()
        {
            _logger.LogDebug("Fetching current IP address");
            var dynResponseData = await httpClient.GetAsync(new Uri(_appConfig.IpAddressLookupProvider));

            var dynContent = await dynResponseData.Content.ReadAsStringAsync();
            Regex ipV4Pattern = new Regex(@"((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");

            Match matchedIpAddress = ipV4Pattern.Match(dynContent);

            _previousIpAddress = _currentIpAddress;
            _currentIpAddress = matchedIpAddress.Value;

            _logger.LogDebug($"Current IP Address: {_currentIpAddress}");

            return _currentIpAddress;
        }

        public string GetCurrentIpAddress() => _currentIpAddress;

        public string GetPreviousIpAddress() => _previousIpAddress;
    }
}
