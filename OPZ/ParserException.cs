using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPZ
{
    class ParserException : Exception
    {
        public ParserException(String message) : base(message) { }
    }
}
