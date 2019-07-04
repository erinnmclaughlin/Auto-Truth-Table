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
            // Look for 'not' operator
            char tempChar = '1';

            while(statement.Contains("~"))
            {
                for(int i = 0; i < statement.Length - 1; i++)
                {
                    if (statement[i] == '~' && statement[i + 1] >= '0' && statement[i + 1] <= 'z')
                    {
                        bool val = propositions.Where(p => p.Name == statement[i + 1].ToString()).FirstOrDefault().Value;
                        propositions.Add(new Proposition(tempChar.ToString(), !val));
                        statement = statement.Replace($"~{statement[i+1]}", tempChar.ToString());
                        tempChar++;
                        i = -1;
                    }
                }
            }

            // Put proposition list in order that it appears in statement
            List<Proposition> temp = new List<Proposition>();
            foreach (char c in statement)
            {
                if (propositions.Where(p => p.Name == c.ToString()).Count() > 0)
                    temp.Add(propositions.Where(p => p.Name == c.ToString()).FirstOrDefault());
            }

            for (int j = 0; j < temp.Count() - 1; j++)
            {
                int start = statement.IndexOf(temp[j].Name) + temp[j].Name.Length;
                int length = statement.IndexOf(temp[j + 1].Name) - start;

                // Find operator between two propositions
                string o = statement.Substring(start, length);

                // Make next proposition have truth value of the statement
                temp[j + 1] = Reduce(temp[j], temp[j + 1], o);
            }

            return temp.Last().Value;
        }

        private static Proposition Reduce (Proposition p, Proposition q, string o)
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
                case "!&&":
                    val = p.Value == false || q.Value == false;
                    break;
            }

            return new Proposition($"{p.Name}{o}{q.Name}", val);

        }
    }
}
