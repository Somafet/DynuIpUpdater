using System;
using System.Collections.Generic;
using System.Text;

namespace DynuIpUpdater.Exceptions
{

    [Serializable]
    public class IpAddressUpdateException : Exception
    {
        public IpAddressUpdateException() { }
        public IpAddressUpdateException(string message) : base(message) { }
        public IpAddressUpdateException(string message, Exception inner) : base(message, inner) { }
        protected IpAddressUpdateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
