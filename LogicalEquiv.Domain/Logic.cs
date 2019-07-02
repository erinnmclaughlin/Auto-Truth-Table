using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalEquiv.Domain
{
    public class Logic
    {
        public static bool Compute (string statement, List<Proposition> propositions)
        {
            if (propositions.Count() == 1)
                return propositions[0].Value;

            for (int j = 0; j < propositions.Count() - 1; j++)
            {
                int start = statement.IndexOf(propositions[j].Name) + propositions[j].Name.Length;
                int length = statement.IndexOf(propositions[j + 1].Name) - start;

                // Find operator between two operators
                string o = statement.Substring(start, length);

                // Make next proposition have truth value of the statement
                propositions[j + 1] = TruthValue(propositions[j], propositions[j + 1], o);
            }

            return propositions.Last().Value;
        }

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
