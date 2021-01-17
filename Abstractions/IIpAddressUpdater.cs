using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynuIpUpdater.Abstractions
{
    public interface IIpAddressUpdater
    {
        Task UpdateIpAddressAsync(string ipAddress);
    }
}
