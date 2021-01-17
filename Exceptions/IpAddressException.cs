using System;
using System.Collections.Generic;
using System.Text;

namespace DynuIpUpdater.Exceptions
{
    [Serializable]
    public class IpAddressException : Exception
    {
        public IpAddressException() { }
        public IpAddressException(string message) : base(message) { }
        public IpAddressException(string message, Exception inner) : base(message, inner) { }
        protected IpAddressException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
