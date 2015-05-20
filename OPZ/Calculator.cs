using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPZ
{
    class Calculator
    {
        public static double Eval(Tree tree)
        {
            return (new Calculator()).Calc(tree);
        }

        private double Calc(Tree tree)
        {
            switch (tree.Value.Type)
            {
                case Element.Types.Number:
                    return Convert.ToDouble(tree.Value.Value);
                case Element.Types.Operator:
                    switch (tree.Value.Value)
                    {
                        case "-":
                            return Calc(tree.Left) - Calc(tree.Right);
                        case "+":
                            return Calc(tree.Left) + Calc(tree.Right);
                        case "/":
                            return Calc(tree.Left) / Calc(tree.Right);
                        case "*":
                            return Calc(tree.Left) * Calc(tree.Right);
                        case "^":
                            return Math.Pow(Calc(tree.Left), Calc(tree.Right));
                        case "sin":
                            return Math.Sin(Calc(tree.Left));
                        case "cos":
                            return Math.Cos(Calc(tree.Left));
                        default:
                            throw new CalculatorException("Неизвестный оператор");
                    }
            }
            throw new CalculatorException("Неизвестная ошибка");
        }
    }
}
