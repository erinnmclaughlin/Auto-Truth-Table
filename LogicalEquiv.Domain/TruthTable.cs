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

        public TruthTable(string statement)
        {
            Statement = statement;
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

        //-- Given a list of propositions, return only the ones that are in a given statement
        private List<Proposition> FilteredPropositions(string statement, List<Proposition> all)
        {
            List<Proposition> fList = new List<Proposition>();
            
            foreach(var prop in all)
            {
                if (statement.Contains(prop.Name))
                    fList.Add(prop);
            }

            return fList;
        }

        //-- Write truth table to console
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
                    
                    /*
                        Last proposition switches every row, second to last proposition switches every 2 rows,
                        third to last switches every 4 rows, etc. so the nth proposition will switch if the row number is
                        evenly divisible by 2 to the number of "spaces" away from the last space it is
                     */

                    if (i % Math.Pow(2, j) == 0)
                        Propositions[Propositions.Count() - 1 - j].Value
                            = !Propositions[Propositions.Count() - 1 - j].Value;

                }

                // Write the values of the propositions
                Propositions.ForEach(p => Console.Write($"{p.Value}\t"));


                string tempStatement = Statement;
                List<Proposition> tempPropositions = new List<Proposition>();
                List<Proposition> allPropositions = new List<Proposition>();
                
                foreach (var p in Propositions)
                {
                    tempPropositions.Add(p);
                    allPropositions.Add(p);
                }

                // Parse through parenthesis, if any
                char newProp = 'A'; // starting index to name new propositions
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

                    // Get what's inside
                    string substring = tempStatement.Substring(sIndex + 1, eIndex - sIndex - 1);

                    // Get list of propositions that exist in the substring
                    tempPropositions = FilteredPropositions(substring, allPropositions);

                    // Create a new proposition with the value of what was inside the parenthesis; add to list of all propositions
                    allPropositions.Add(new Proposition(newProp.ToString(), Logic.Compute(substring, tempPropositions)));

                    // Replace the statement in parenthesis with the new proposition that represents it
                    tempStatement = tempStatement.Replace("(" + substring + ")", allPropositions.Last().Name);

                    newProp++;
                }

                Console.Write(Logic.Compute(tempStatement, FilteredPropositions(tempStatement, allPropositions)));
                Console.WriteLine();
            }
        }
    }
}
