using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalEquiv.Domain
{
    public class TruthTableRow
    {
        private int cols { get; set; }

        public TruthTableRow(int c)
        {
            cols = c;
        }
    }
}
