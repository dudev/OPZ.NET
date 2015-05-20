using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPZ
{
    class Tree
    {
        public readonly Element Value;
        public readonly Tree Left = null;
        public readonly Tree Right = null;

        public Tree(Element v, Tree l, Tree r)
        {
            Value = v;
            Left = l;
            Right = r;
        }
    }
}
