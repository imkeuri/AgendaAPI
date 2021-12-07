using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Exceptions
{
    internal class UnvalidArgumentException : Exception
    {
        public UnvalidArgumentException()
        {
        }

        public UnvalidArgumentException(string message) : base(message)
        {
        }

        public UnvalidArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnvalidArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
