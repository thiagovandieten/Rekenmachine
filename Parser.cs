using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Rekenmachine
{
    class Parser
    {
        public static readonly string[] OPERATORS = { "+", "-", "*", "/" };

        public String testString { get; set; }
        public decimal calculate(string notation)
        {
            Queue<string> postFix = convertToPostFix(notation);
            return parsePostFix(postFix);

            //while (postFix.Count > 0)
            //{
            //    testString += postFix.Dequeue();
            //}
            //System.Diagnostics.Debug.WriteLine(testString);
        }

        public string convertPrecentageToDecimal(string notation)
        {
            string[] infix = splitNotation(notation);
            int lastValue = infix.Count() - 1;
            if(Decimal.TryParse(infix[lastValue], out decimal result))
            {
                result /= 100;
                infix[lastValue] = result.ToString();
            }
            else
            {
                return notation;
            }
            return String.Join("", infix);
        }

        private Queue<string> convertToPostFix(string notation)
        {
            string[] infix = splitNotation(notation);

            Queue<string> postfix = new Queue<string>();
            Stack<string> operatorStack = new Stack<string>();
            //Implementeer de shunting-yard algoritme
            //Geleerd van Wikipedia en Brilliant: https://brilliant.org/wiki/shunting-yard-algorithm/
            foreach (String token in infix)
            {
                if (stringIsOperator(token))
                {
                    if (operatorStack.Count > 0 && !tokenIsOfGreaterPrecedence(token, operatorStack.Peek()))
                    {
                        postfix.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(token);

                    System.Diagnostics.Debug.WriteLine($"It's a {token}");
                }
                else if (decimal.TryParse(token, out decimal devnull))
                {
                    postfix.Enqueue(token);
                }
            }
            //If operatorstack not empty, pop it's remaining elements
            while (operatorStack.Count > 0)
            {
                postfix.Enqueue(operatorStack.Pop());
            }
            return postfix;
        }

        private static string[] splitNotation(string notation)
        {
            return Regex.Split(notation, @"([+\-*\/])");
        }

        private decimal parsePostFix(Queue<string> postFix)
        {
            Stack<decimal> stack = new Stack<decimal>();
            decimal number = Decimal.Zero;

            while (postFix.Count > 0)
            {
                string token = postFix.Dequeue();

                if (decimal.TryParse(token, out number))
                {
                    stack.Push(number);
                }
                else
                {
                    switch (token)
                    {
                        case "*":
                            stack.Push(stack.Pop() * stack.Pop());
                            break;

                        case "/":
                            number = stack.Pop();
                            stack.Push(stack.Pop() / number);
                            break;

                        case "+":
                            stack.Push(stack.Pop() + stack.Pop());
                            break;

                        case "-":
                            number = stack.Pop();
                            stack.Push(stack.Pop() - number);
                            break;

                        default:
                            // Won't be caught anywhere. Not planning to unless this calculator app needs to have more functions.
                            throw new ArgumentOutOfRangeException("postFix", "Contains a invalid character");
                    }
                }
            }
            return stack.Pop();
        }

        private bool stringIsOperator(string token)
        {
            //Vergelijk char met de ASCII waarde van de operaties *+-/ https://cs.smu.ca/~porter/csc/ref/asciifull.gif
            if (OPERATORS.Contains(token))
                return true;
            else
                return false;
        }

        private bool tokenIsAdditionorSubtraction(string token)
        {
            string[] plusminus = new string[2];
            Array.Copy(OPERATORS, plusminus, 2);

            if (plusminus.Contains(token))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool tokenIsMultiplicationorDivision(string token)
        {
            string[] multiplydivision = new string[2];
            Array.Copy(OPERATORS, 2, multiplydivision, 0, 2);

            if (multiplydivision.Contains(token))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool tokenIsOfGreaterPrecedence(string firstOp, string secondOp)
        {
            if (tokenIsMultiplicationorDivision(firstOp) && tokenIsAdditionorSubtraction(secondOp))
            {
                return true;
            }
            return false;
        }
    }
}
