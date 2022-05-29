using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysVentas.Products.Domain.Base
{
    public class DomainValidation
    {
        public Dictionary<string, string> Fallos { get; private set; }
        public bool IsValid { get => Fallos.Count == 0; }
        public DomainValidation()
        {
            this.Fallos = new Dictionary<string, string>();
            this.Fallos.Clear();
        }
        public void AddFailed(string key, string error)
        {
            this.Fallos.Add(key, error);
        }
    }
}
