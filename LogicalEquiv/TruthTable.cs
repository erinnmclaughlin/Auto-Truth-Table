using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalEquiv
{
    public class TruthTable
    {
        public TruthTable(int n)
        {
            Props = new List<Proposition>();
            generateProps(n);
        }

        public List<Proposition> Props { get; set; }

        private void generateProps(int n)
        {
            char letter = 'a';

            for (int i = 0; i < n; i++)
            {
                Props.Add(new Proposition(letter.ToString(), true));
                letter++;
            }
        }
    }
}
