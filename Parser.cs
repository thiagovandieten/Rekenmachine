using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekenmachine
{
    class Parser
    {
        public void calculate(string notation = "8+3*8-6/(7-1)")
        {
            convertToPostFix(notation);
        }

        private string convertToPostFix(string notation)
        {
            char[] infix = notation.ToCharArray();
            Queue<char> postfix = new Queue<char>();
            Stack<char> operatorStack = new Stack<char>();

            //Implementeer de shunting-yard algoritme
            //Geleerd van Wikipedia en Brilliant: https://brilliant.org/wiki/shunting-yard-algorithm/
            foreach (char token in infix)
            {
                if(char.IsDigit(token))
                {
                    postfix.Enqueue(token);
                }
                else if(charIsOperator(token)) 
                {
                    try
                    {
                        char TopOfStack = operatorStack.Peek();
                        if (charIsMultiplicationorDivision(TopOfStack))
                        {
                            postfix.Enqueue(operatorStack.Pop());
                        }
                    }
                    //operatorStack was leeg.
                    catch (InvalidOperationException e)
                    {
                        
                    } 
                    finally
                    {
                        operatorStack.Push(token);
                    }
                    
                    System.Diagnostics.Debug.WriteLine($"It's a {token.ToString()}");
                }
            }
            //System.Diagnostics.Debug.WriteLine(postfix.ToString());
            return "ye";

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
            if(token == 43 || token == 45)
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
            if(token == 42 || token == 47)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
