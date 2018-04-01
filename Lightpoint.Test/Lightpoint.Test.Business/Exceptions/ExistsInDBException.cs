using System;
using System.Collections.Generic;
using System.Text;

namespace Lightpoint.Test.Business.Exceptions
{
    [Serializable]
    public class ExistsInDBException : Exception
    {
        public ExistsInDBException() { }
        public ExistsInDBException(string message) : base(message) { }
        public ExistsInDBException(string message, Exception inner) : base(message, inner) { }
        protected ExistsInDBException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
