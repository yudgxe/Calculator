using System.Collections.Generic;
using System.Text;

namespace Сalculator
{
    class CfgRule
    {
        public string Left { get; set; }

        public List<string> Right { get; set; }

        public List<string> RPNRight { get; set; }

        public CfgRule(string left, IEnumerable<string> right, IEnumerable<string> RPNRight)
        {
            Left = left;

            Right = new List<string>(right);
            this.RPNRight = new List<string>(RPNRight);

        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Left);
            sb.Append(" ->");

            for (var i = 0; i < Right.Count; i++)
            {
                sb.Append(" ");
                sb.Append(Right[i]);
            }
            return sb.ToString();
        }
    }
}
