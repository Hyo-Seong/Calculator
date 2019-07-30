using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Util
{
    public static class StringExtension
    {
        public static bool IsInt(this string s)
        {
            int x = 0;
            return int.TryParse(s, out x);
        }

        public static bool IsOperator(this string s)
        {
            if (s.Equals("＋") || s.Equals("－") || s.Equals("×") || s.Equals("÷"))
            {
                return true;
            }
            return false;
        }
    }
}
