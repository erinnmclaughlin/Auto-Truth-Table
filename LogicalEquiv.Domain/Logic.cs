using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalEquiv.Domain
{
    public class Logic
    {
        public static Proposition TruthValue (Proposition p, Proposition q, string o)
        {
            bool val = false;

            // Figure out what to do
            switch (o)
            {
                case "&&":
                    val = p.Value && q.Value;
                    break;
                case "||":
                    val = p.Value || q.Value;
                    break;
                case "=>":
                    val = !p.Value || q.Value;
                    break;
                case "<=>":
                    val = p.Value == q.Value;
                    break;
                case "==":
                    val = p.Value == q.Value;
                    break;
                case "XOR":
                    val = p.Value != q.Value &&
                        (p.Value || q.Value);
                    break;
                case "NOR":
                    val = p.Value == false && q.Value == false;
                    break;
            }

            return new Proposition($"{p.Name}{o}{q.Name}", val);

        }
    }
}
