using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Helper;

namespace Calculator
{
    public class Parenthese
    {
        public Parenthese Ancestor { get; }

        public List<string> NumbersOperators { get; set; } = new List<string> { };

        private Parenthese(Parenthese ancestor)
        {
            Ancestor = ancestor;
        }

        public Parenthese OpenParenthese()
        {
            return new Parenthese(this);
        }

        public (Parenthese ancestor, string resultThis) CloseParenthese()
        {
            return (Ancestor, NumbersOperators.ResultOperationsOfThisBlock());
        }

        public Parenthese()
        {
            this.Ancestor = null;
        }
    }
}
