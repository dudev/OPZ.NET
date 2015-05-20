using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OPZ
{
    class Parser
    {
        private readonly string _formula;
        private readonly Dictionary<string, int> _primary = new Dictionary<string, int>();

        public Parser(string formula)
        {
            _formula = formula;

            _primary.Add("sin", 5);
            _primary.Add("cos", 5);
            _primary.Add("^", 4);
            _primary.Add("*", 3);
            _primary.Add("/", 3);
            _primary.Add("+", 2);
            _primary.Add("-", 2);
            _primary.Add("(", 1);
        }
        public Tree Eval()
        {
            Check();
            return Convert();
        }


        private List<string> _operators = new List<string>();
        private List<Tree> _tree = new List<Tree>();

        private Tree Convert()
        {
            var formula = Space();
            foreach (string i in formula)
            {
                if (i == ")")
                {
                    while (_operators[_operators.Count - 1] != "(")
                    {
                        Process();
                    }
                    _operators.RemoveAt(_operators.Count - 1);
                }
                else if (i == "(")
                {
                    _operators.Add(i);
                }
                else if (!(new Regex("^[-]?[0-9.]+$")).IsMatch(i))
                {
                    while (_operators.Count > 0 && _primary[_operators[_operators.Count - 1]] >= _primary[i])
                    {
                        Process();
                    }
                    _operators.Add(i);
                }
                else
                {
                    _tree.Add(new Tree(new Element(i, Element.Types.Number), null, null));
                }
            }
            while (_operators.Count > 0)
            {
                Process();
            }
            return _tree[0];
        }

        private void Process()
        {
            if ((new Regex("[-+*/^]")).IsMatch(_operators[_operators.Count - 1]))
            {
                var fst = _tree[_tree.Count - 1];
                var snd = _tree[_tree.Count - 2];
                _tree.RemoveAt(_tree.Count - 1);
                _tree.RemoveAt(_tree.Count - 1);
                _tree.Add(new Tree(new Element(_operators[_operators.Count - 1], Element.Types.Operator), snd, fst));
            }
            else
            {
                if (_operators[_operators.Count - 1] != "sin" && _operators[_operators.Count - 1] != "cos")
                {
                    throw new ParserException("Недопустимая функция");
                }
                var fst = _tree[_tree.Count - 1];
                _tree.RemoveAt(_tree.Count - 1);
                _tree.Add(new Tree(new Element(_operators[_operators.Count - 1], Element.Types.Operator), fst, null));
            }
            _operators.RemoveAt(_operators.Count - 1);
        }

        private Array Space()
        {
            var ret = "";
            var i = 0;
            while (i < _formula.Length)
            {
                if ((new Regex("[0-9.]")).IsMatch(_formula[i].ToString())
                    || (
                        (i == 0 || _formula[i - 1] == '(')
                        && _formula[i] == '-' && (new Regex("[0-9.]")).IsMatch(_formula[i + 1].ToString())
                        )
                    )
                {
                    ret += _formula[i].ToString();
                }
                else if ((new Regex("[a-z]")).IsMatch(_formula[i].ToString()))
                {
                    if (i > 0 && (new Regex("[^a-z]")).IsMatch(_formula[i - 1].ToString()))
                    {
                        ret += " ";
                    }
                    ret += _formula[i].ToString();
                    if (i < (_formula.Length - 1) && (new Regex("[^a-z]")).IsMatch(_formula[i + 1].ToString()))
                    {
                        ret += " ";
                    }
                }
                else
                {
                    ret += " " + _formula[i].ToString() + " ";
                }
                i++;
            }
            return ret.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }

        private void Check()
        {
            //Есть ли выражение
            if (_formula.Length == 0)
            {
                throw new ParserException("Пустое выражение");
            }
            //Есть ли недопустимые сочетания
            if ((new Regex("^[^-(a-z0-9]{1}$")).IsMatch(_formula[0].ToString()))
            {
                throw new ParserException("Недопустимое выражение 1");
            }
            if ((new Regex("[-+*^/(]$")).IsMatch(_formula))
            {
                throw new ParserException("Недопустимое выражение 2");
            }
            //Баланс скобок
            int balance = 0;
            foreach (var v in _formula)
            {
                if (v == '(')
                {
                    balance += 1;
                }
                if (v == ')')
                {
                    balance -= 1;
                }
            }
            if (balance != 0)
            {
                throw new ParserException("Скобки не сбалансированы");
            }
        }

        public static string ToOpz(Tree tree)
        {
            if (tree == null)
            {
                return "";
            }
            else
            {
                return ToOpz(tree.Left) + ToOpz(tree.Right) + tree.Value.Value + " ";
            }
        }
    }
}
