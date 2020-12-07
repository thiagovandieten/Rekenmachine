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
                else if(isOperator(token)) 
                {
                    System.Diagnostics.Debug.WriteLine($"It's a {token.ToString()}");
                }
            }
            //System.Diagnostics.Debug.WriteLine(postfix.ToString());
            return "ye";

        }

        private bool isOperator(char token)
        {
            String tokenString = token.ToString();
            if (tokenString.Equals("+") || tokenString.Equals("-") || tokenString.Equals("*") || tokenString.Equals("/"))
                return true;
            else
                return false;
        }
    }
}
