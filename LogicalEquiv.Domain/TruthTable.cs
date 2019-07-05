using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalEquiv.Domain
{
    public class TruthTable
    {
        public List<string> Statements { get; set; }
        public List<Proposition> Propositions { get; set; }

        public TruthTable(string statement)
        {
            InitializePropositions(statement);
            InitializeStatements(statement);
        }

        public void Write()
        {
            //-- List out statements
            foreach (var s in Statements)
            {
                Console.Write($"{s} \t");
            }
            Console.WriteLine();

            //-- List out truth values
            //-- Loop through all possible scenarios, creating 2^n rows
            for (int i = 0; i < Math.Pow(2, Propositions.Count()); i++)
            {
                //-- Loop through each proposition & determine if the value should change for the given row
                for (int j = 0; j < Propositions.Count(); j++)
                {
                    /*
                        Last proposition switches every row, second to last proposition switches every 2 rows,
                        third to last switches every 4 rows, etc. so the nth proposition will switch if the row number is
                        evenly divisible by 2 to the number of "spaces" away from the last space it is. I.e.:

                        The last prop is 0 spaces away from last prop, so this prop's value will change when the row number is
                        divisible by 2^0, so it'll change everytime.

                        The third to last prop is 2 spaces away from the last prop, so this prop's value will change with the
                        row number is divisible by 2^2, so it'll change every four rows.
                        
                     */

                    if (i % Math.Pow(2, j) == 0)
                        Propositions[Propositions.Count() - 1 - j].Value
                            = !Propositions[Propositions.Count() - 1 - j].Value;

                }

                //-- Loop through each statement
                foreach (var s in Statements)
                {
                    Console.Write($"{Logic.Compute(s, Propositions)}\t");
                }

                Console.WriteLine();

            }
        }

        public void Write(string path)
        {
            path += "\\output.csv";
            var csv = "";

            //-- List out statements
            foreach (var s in Statements)
            {
                csv += $"{s},";
            }
            csv += "\n";

            //-- List out truth values
            //-- Loop through all possible scenarios, creating 2^n rows
            for (int i = 0; i < Math.Pow(2, Propositions.Count()); i++)
            {
                //-- Loop through each proposition & determine if the value should change for the given row
                for (int j = 0; j < Propositions.Count(); j++)
                {
                    /*
                        Last proposition switches every row, second to last proposition switches every 2 rows,
                        third to last switches every 4 rows, etc. so the nth proposition will switch if the row number is
                        evenly divisible by 2 to the number of "spaces" away from the last space it is. I.e.:

                        The last prop is 0 spaces away from last prop, so this prop's value will change when the row number is
                        divisible by 2^0, so it'll change everytime.

                        The third to last prop is 2 spaces away from the last prop, so this prop's value will change with the
                        row number is divisible by 2^2, so it'll change every four rows.
                        
                     */

                    if (i % Math.Pow(2, j) == 0)
                        Propositions[Propositions.Count() - 1 - j].Value
                            = !Propositions[Propositions.Count() - 1 - j].Value;

                }

                //-- Loop through each statement
                foreach (var s in Statements)
                {
                    csv += $"{Logic.Compute(s, Propositions)},";
                }

                csv += "\n";

                File.WriteAllText(path, csv);

            }
        }

        private void InitializePropositions(string statement)
        {
            Propositions = new List<Proposition>();

            //-- Add all propositions to list & set all to false
            //-- Make sure they're not double counted
            foreach (char c in statement)
            {
                if (c >= 'a' && c <= 'z' && Propositions.Where(p => p.Name == c.ToString()).Count() == 0)
                    Propositions.Add(new Proposition(c.ToString(), false));
            }
        }

        private void InitializeStatements(string statement)
        {
            Statements = new List<string>();

            //-- Add single propositions as statements
            foreach (var p in Propositions)
            {
                List<Proposition> prop = new List<Proposition>();
                prop.Add(p);
                Statements.Add(p.Name);
            }

            //-- Add parenthesis surrounding entire statement
            statement = "(" + statement + ")";

            //-- Find all "substatements" in statement
            FindSubstatements(statement);
        }

        //-- Adds each "thing" inside a set of parenthesis as another statement
        private void FindSubstatements(string statement)
        {
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

                //-- Add that to Statements list
                Statements.Add(substring.Replace("\\", "(").Replace("/", ")"));

                //-- Replace parenthesis with slashes to indicate that statement has been read
                statement = statement.Replace($"({substring})", $"\\{substring}/");

            }
        }

        //-- Given a list of propositions, return only the ones that are in a given statement
        private List<Proposition> FilteredPropositions(string statement, List<Proposition> all)
        {
            List<Proposition> fList = new List<Proposition>();

            foreach (var prop in all)
            {
                if (statement.Contains(prop.Name))
                    fList.Add(prop);
            }

            return fList;
        }
    }
}
