using DynuIpUpdater.Abstractions;
using DynuIpUpdater.Configuration;
using DynuIpUpdater.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DynuIpUpdater.Implementation
{
    public class DynuIpAddressUpdater : IIpAddressUpdater
    {
        private readonly AppConfig _appConfig;
        private readonly ILogger<DynuIpAddressUpdater> _logger;
        private static readonly HttpClient _httpClient = new HttpClient();

        public DynuIpAddressUpdater(ILogger<DynuIpAddressUpdater> logger, IOptions<AppConfig> appConfig)
        {
            _logger = logger;
            _appConfig = appConfig.Value;
        }

        public async Task UpdateIpAddressAsync(string ipAddress)
        {
            _logger.LogInformation($"Updating IP address to: {ipAddress}");
            var md5Password = CreateMD5(_appConfig.Dynu.Password);
            var response = await _httpClient.GetAsync($"{_appConfig.Dynu.UpdateUrl}?myip={ipAddress}&username={_appConfig.Dynu.Username}&password={md5Password}");

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new IpAddressUpdateException($"Update request didn't return OK. Response: {content}");
            }

            _logger.LogInformation("Update successful");
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
