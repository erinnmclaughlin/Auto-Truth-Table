using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicalEquiv.Domain
{
    public class Logic
    {
        //-- takes a statement and a list of propositions and returns a value
        public static bool Compute (string statement, List<Proposition> Propositions)
        {
            List<Proposition> propositions = new List<Proposition>(Propositions);
            statement = statement.Replace(" ", "");
            char tempPropName = 'A';
            
            while(statement.Contains("(") || statement.Contains("~"))
            {
                // Look for 'not' operator
                foreach (Match r in Regex.Matches(statement, @"\~[a-zA-Z]"))
                {
                    bool val = !propositions.Where(prop => prop.Name == statement[statement.IndexOf(r.Value) + 1].ToString()).FirstOrDefault().Value;
                    propositions.Add(new Proposition(tempPropName.ToString(), val));
                    var regex = new Regex(Regex.Escape(r.Value));
                    statement = regex.Replace(statement, tempPropName.ToString(), 1);
                    tempPropName++;
                }

                // Look for parenthesis
                while (statement.Contains("("))
                {
                    //-- Find innermost "(" ")" pair
                    int sIndex = -1, eIndex = -1;
                    for (int j = 0; j < statement.Length; j++)
                    {
                        if (statement[j] == '(')
                            sIndex = j;
                        else if (statement[j] == ')')
                        {
                            eIndex = j;
                            break;
                        }
                    }

                    //-- Get what's inside
                    string substring = statement.Substring(sIndex + 1, eIndex - sIndex - 1);

                    //-- Go to beginning of outer for loop if there's a "not" operator
                    if (substring.Contains("~"))
                        break;

                    //-- Compute that value of that & make new proposition to represent that substring
                    propositions.Add(new Proposition(tempPropName.ToString(), Reduce(substring, propositions)));

                    //-- Replace statement in parenethesis with new statement that represents it
                    statement = statement.Replace($"({substring})", tempPropName.ToString());

                    //-- Increment tempPropName
                    tempPropName++;

                }
            }

            return Reduce(statement, propositions);

        }

        //-- Evaluates statements without parenthesis
        private static bool Reduce (string s, List<Proposition> props)
        {
            if (s.Length == 1)
                return props.Where(prop => prop.Name == s).FirstOrDefault().Value;

            Proposition p = props.Where(prop => prop.Name == s[0].ToString()).FirstOrDefault();
            Proposition q = props.Where(prop => prop.Name == s[s.Length - 1].ToString()).FirstOrDefault();
            string o = s.Substring(1, s.Length - 2);
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

            return val;

        }
    }
}
