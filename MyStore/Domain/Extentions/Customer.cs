using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public partial class Customer
    {
        public string MyProperFullNamety { get => this.Contactname + " " + this.Contacttitle; }
    }
}
