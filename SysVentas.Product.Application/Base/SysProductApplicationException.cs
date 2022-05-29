using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SysVentas.Products.Application.Base
{
    public class SysProductApplicationException: Exception
    {
        public SysProductApplicationException() { }
        public SysProductApplicationException(string message) : base(message) { }
        public SysProductApplicationException(string message, Exception inner) : base(message, inner) { }
        protected SysProductApplicationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
