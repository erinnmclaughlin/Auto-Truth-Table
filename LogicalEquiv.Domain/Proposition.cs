using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalEquiv.Domain
{
    public class Proposition
    {
        public Proposition(string n, bool val)
        {
            Name = n;
            Value = val;
        }

        public string Name { get; set; }
        public bool Value { get; set; }

        public void Negate()
        {
            Value = !Value;
        }
    }
}
