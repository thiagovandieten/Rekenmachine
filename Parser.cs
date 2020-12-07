using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekenmachine
{
    class Parser
    {
        public const int PLUS = 43;
        public const int MULTIPLY = 42;
        public const int MINUS = 45;
        public const int DIVISION = 47;
        public String testString { get; set; }
        public void calculate(string notation = "8+3*8-6/(7-1)")
        {
            Queue<char> postFix = convertToPostFix(notation);
            //TODO: Parse reverse polish notation on postFix
            while(postFix.Count > 0)
            {
                testString += postFix.Dequeue();
            }
            System.Diagnostics.Debug.WriteLine(testString);
        }

        private Queue<char> convertToPostFix(string notation)
        {
            char[] infix = notation.ToCharArray();
            Queue<char> postfix = new Queue<char>();
            Stack<char> operatorStack = new Stack<char>();
            //Implementeer de shunting-yard algoritme
            //Geleerd van Wikipedia en Brilliant: https://brilliant.org/wiki/shunting-yard-algorithm/
            foreach (char token in infix)
            {
                if (char.IsDigit(token))
                {
                    postfix.Enqueue(token);
                }
                else if (charIsOperator(token))
                {
                    if (operatorStack.Count > 0 && !charIsOfGreaterPrecedence(token, operatorStack.Peek()))
                    {
                        postfix.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(token);

                    System.Diagnostics.Debug.WriteLine($"It's a {token.ToString()}");
                }
            }
            //If operatorstack not empty, pop it's remaining elements
            while(operatorStack.Count > 0)
            {
                postfix.Enqueue(operatorStack.Pop());
            }
            return postfix;
        }

        private bool charIsOperator(char token)
        {
            //Vergelijk char met de ASCII waarde van de operaties *+-/ https://cs.smu.ca/~porter/csc/ref/asciifull.gif
            if (charIsAdditionorSubtraction(token) || charIsMultiplicationorDivision(token))
                return true;
            else
                return false;
        }

        private bool charIsAdditionorSubtraction(char token)
        {
            if (token == PLUS || token == MINUS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool charIsMultiplicationorDivision(char token)
        {
            if (token == MULTIPLY || token == DIVISION)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool charIsOfGreaterPrecedence(char firstOp, char secondOp)
        {
            if(charIsMultiplicationorDivision(firstOp) && charIsAdditionorSubtraction(secondOp))
            {
                return true;
            }
            return false;
        }
    }
}
