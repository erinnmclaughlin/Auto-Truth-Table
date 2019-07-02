using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalEquiv
{
    public static class Logic
    {
        //-- not, true when false
        public static bool not(bool prop) => !prop;

        //-- and, true when all props are true
        public static bool and(List<bool> props) => props.TrueForAll(p => p == true);
        public static bool and(bool p, bool q) => p && q;

        //-- nand, true when and is false
        public static bool nand(List<bool> props) => !and(props);
        public static bool nand(bool p, bool q) => !and(p, q);

        //-- or, true when at least one of the props are true
        public static bool or(List<bool> props) => props.Where(p => p == true).Count() > 0;
        public static bool or(bool p, bool q) => p || q;

        //-- xor, true when at least one is true but not all are true
        public static bool xor(List<bool> props) => props.Where(p => p == true).Count() > 0 && !props.TrueForAll(p => p == true);
        public static bool xor(bool p, bool q) => (p || q) && !(p && q);

        //-- nor, true when all are false, aka true when or is false
        public static bool nor(List<bool> props) => !or(props);
        public static bool nor(bool p, bool q) => !or(p, q);

        //-- implies, true when p is false or q is true
        public static bool implies(bool p, bool q) => p == false || q == true;

        //-- biconditional, true when p == q
        public static bool biconditional(bool p, bool q) => p == q;
    }
}
