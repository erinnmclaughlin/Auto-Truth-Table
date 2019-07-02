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

        public static List<Proposition> GetPropositions(string statement)
        {
            List<Proposition> Propositions = new List<Proposition>();
            foreach (char c in statement)
            {
                if (c >= 'a' && c <= 'z')
                    Propositions.Add(new Proposition(c.ToString(), false));
            }

            return Propositions;
        }
    }
}
