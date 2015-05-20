using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPZ
{
    class CalculatorException : Exception
    {
        public CalculatorException(String message) : base(message) { }
    }
}
