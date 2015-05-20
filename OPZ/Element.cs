using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPZ
{
    class Element
    {
        public enum Types
        {
            Number, Operator
        }

        public readonly Types Type;
        public readonly string Value;

        public Element(string v, Types t)
        {
            Value = v;
            Type = t;
        }
    }
}
