using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalEquiv.Domain
{
    public class TruthTable
    {
        public static void Write(string statement)
        {
            List<Proposition> Propositions = Proposition.GetPropositions(statement);

            foreach (var prop in Propositions)
            {
                Console.Write($"{prop.Name}\t");
            }

            Console.Write($"{statement}\n");

            statement = statement.Replace(" ", "");

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

                // Create a copy of Propositions to modify while determining truth value of statement
                List<Proposition> Temp = new List<Proposition>();
                Propositions.ForEach(p => Temp.Add(p));

                // Determine the value of the statement given the values of the propositions
                // Parenthesis not supported yet
                for (int j = 0; j < Temp.Count() - 1; j++)
                {
                    int start = statement.IndexOf(Temp[j].Name) + Temp[j].Name.Length;
                    int length = statement.IndexOf(Temp[j + 1].Name) - start;

                    // Find operator between two operators
                    string o = statement.Substring(start, length);

                    // Make next proposition have truth value of the statement
                    Temp[j + 1] = Logic.TruthValue(Temp[j], Temp[j + 1], o);
                }

                Console.Write(Temp.Last().Value);
                Console.WriteLine();
            }
        }
    }
}
