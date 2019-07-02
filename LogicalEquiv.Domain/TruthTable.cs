using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalEquiv.Domain
{
    public class TruthTable
    {
        public string Statement { get; set; }
        public List<Proposition> Propositions { get; set; }
        public List<TruthTableRow> Rows { get; set; }

        private string SimplifiedStatement { get; set; }

        public TruthTable(string statement)
        {
            Statement = statement;
            SimplifiedStatement = statement;
            Propositions = new List<Proposition>();
            Rows = new List<TruthTableRow>();
            InitializePropositions();
            Write();
        }

        private void InitializePropositions()
        {
            Propositions = new List<Proposition>();

            //-- First find all individual propositions -- set them all to false
            //-- Make sure that props aren't double counted
            foreach (char c in Statement)
            {
                if (c >= 'a' && c <= 'z' && Propositions.Where(p => p.Name == c.ToString()).Count() == 0)
                    Propositions.Add(new Proposition(c.ToString(), false));
            }
        }

        private List<Proposition> FilterPropositions(string statement, List<Proposition> p)
        {
            List<Proposition> props = new List<Proposition>();

            foreach (var prop in Propositions)
            {
                if (statement.Contains(prop.Name) && !p.Contains(prop))
                    props.Add(prop);
            }

            foreach(var prop in p)
            {
                if (statement.Contains(prop.Name) && prop.Name.Length == 1)
                    props.Add(prop);
            }

            return props;
        }

        public void Write()
        {
            foreach (var prop in Propositions)
            {
                Console.Write($"{prop.Name}\t");
            }

            Console.Write($"{Statement}\n");

            Statement = Statement.Replace(" ", "");

            // Loop through all possible scenarios, creating 2^n rows
            for (int i = 0; i < Math.Pow(2, Propositions.Count()); i++)
            {
                // Loop through each proposition 
                for (int j = 0; j < Propositions.Count(); j++)
                {
                    // Determine if the value should change for the given row
                    if (i % Math.Pow(2, j) == 0)
                        Propositions[Propositions.Count() - 1 - j].Value
                            = !Propositions[Propositions.Count() - 1 - j].Value;

                }

                // Write the values of the propositions
                foreach (var prop in Propositions)
                {
                    Console.Write($"{prop.Value}\t");
                }

                // Determine the value of the statement given the values of the propositions
                string tempStatement = Statement;
                List<Proposition> tempPropositions = Propositions;

                char newProp = 'A';
                while (tempStatement.Contains("("))
                {
                    // Find innermost "(" ")" pair
                    int sIndex = -1, eIndex = -1;
                    for (int j = 0; j < tempStatement.Length; j++)
                    {
                        if (tempStatement[j] == '(')
                            sIndex = j;
                        else if (tempStatement[j] == ')')
                        {
                            eIndex = j;
                            break;
                        }
                    }

                    // Compute what's inside
                    // Something here isn't working bc passing too many props for a given statement, fix later
                    string substring = tempStatement.Substring(sIndex + 1, eIndex - sIndex - 1);
                    tempPropositions = FilterPropositions(substring, tempPropositions);

                    tempPropositions.Add(new Proposition(newProp.ToString(), Logic.Compute(substring, tempPropositions)));
                    tempStatement = tempStatement.Replace("(" + substring + ")", tempPropositions.Last().Name);
                    tempPropositions = FilterPropositions(tempStatement, tempPropositions);
                    newProp++;
                }

                Console.Write(Logic.Compute(tempStatement, tempPropositions));
                Console.WriteLine();
            }
        }
    }
}
