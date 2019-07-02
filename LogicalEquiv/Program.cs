using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicalEquiv.Domain;

namespace LogicalEquiv
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program will generate a truth table for given operations on propositions.");
            Console.WriteLine("Note that only lowercase letters will be recognized as propositions.");
            Console.WriteLine("Current supported operations: '&&', '||', '=>', '<=>', '==', 'XOR', 'NOR', !&&\n");

            GetStatement:
            Console.Write("Enter a statement: ");

            string statement = Console.ReadLine();
            Console.WriteLine();

            //TruthTable.Write(statement);
            TruthTable t = new TruthTable(statement);
            t.Write();

            PlayAgain:
            Console.Write("\nWould you like to enter another statement? (Y / N): ");

            switch(Console.ReadLine().ToUpper())
            {
                case "Y":
                    Console.WriteLine();
                    goto GetStatement;
                case "N":
                    break;
                default:
                    Console.WriteLine("Input not recognized. Please try again.");
                    goto PlayAgain;
            }

            Console.WriteLine("\nThanks for playing! Press any key to exit.");
            Console.ReadKey();
            
        }
    }
}
