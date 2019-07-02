using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalEquiv
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Proposition> Propositions = new List<Proposition>();

            Console.WriteLine("This program will generate a truth table for given operations on propositions.");
            Console.WriteLine("Current supported operations: '&&', '||', '=>'\n");
            Console.Write("Enter a statement: ");

            string statement = Console.ReadLine();

            Console.WriteLine();

            foreach (char c in statement)
            {
                if (c >= 'a' && c <= 'z')
                    Propositions.Add(new Proposition(c.ToString(), false));
            }

            foreach(var prop in Propositions)
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
                    if (i % Math.Pow(2,j) == 0)
                        Propositions[Propositions.Count() - 1 - j].Value 
                            = !Propositions[Propositions.Count() - 1 -j].Value;

                }

                // Write the values of the propositions
                foreach(var prop in Propositions)
                {
                    Console.Write($"{prop.Value}\t");
                }
                
                bool val = false;
                List<Proposition> Temp = new List<Proposition>();

                foreach (var p in Propositions)
                {
                    Temp.Add(p);
                }

                // Determine the value of the statement given the values of the propositions
                for (int j = 0; j < Temp.Count() - 1; j++)
                {
                    string o = statement.Substring(statement.IndexOf(Temp[j].Name) + 1,
                        statement.IndexOf(Temp[j + 1].Name) - statement.IndexOf(Temp[j].Name) - 1);

                    switch(o)
                    {
                        case "&&":
                            val = Temp[j].Value && Temp[j + 1].Value;
                            break;
                        case "||":
                            val = Temp[j].Value || Temp[j + 1].Value;
                            break;
                        case "=>":
                            val = !Temp[j].Value || Temp[j + 1].Value;
                            break;
                    }

                    Temp[j + 1] = new Proposition(Temp[j+1].Name, val);
                }

                Console.Write(val);
                Console.WriteLine();
            }

            Console.ReadKey();
            
        }
    }
}
