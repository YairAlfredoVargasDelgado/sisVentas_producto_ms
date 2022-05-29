using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SysVentas.Products.Domain.Base
{
    public class DomainException: Exception
    {
        public DomainValidation _domainValidation { get; }
        public Dictionary<string, string> Errors { get; }
        public DomainException(DomainValidation validator)
        {
            this._domainValidation = validator;
            this.Errors = this._domainValidation.Fallos;
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
