using System;
using System.Collections.Generic;
using System.Text;

namespace DynuIpUpdater.Configuration
{
    public class AppConfig
    {
        public string IpAddressLookupProvider { get; set; }
        public int IpUpdateIntervalMilliSeconds { get; set; }
        public DynuConfig Dynu { get; set; }
    }
}
