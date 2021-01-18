using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynuIpUpdater.Abstractions
{
    public interface IIpAddressProvider
    {
        Task<string> FetchCurrentIpAddressAsync();

        string GetPreviousIpAddress();
        string GetCurrentIpAddress();
    }
}
