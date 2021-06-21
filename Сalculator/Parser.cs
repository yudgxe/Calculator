using System.Linq;

namespace Сalculator
{
    class Parser
    {
        private const string operatorToParse = "+*()-/";

        private int index = default;

        private string source;

        public Parser(string input) 
        {
            source = input.Replace(".", ",");
        }

        string ParseNumber(string startSign)
        {
            string relust = startSign;

            for (; index < source.Length; index++) 
            {
                if ("1234567890".IndexOf(source[index]) != -1 || source[index] == ',')
                {
                    relust += source[index];
                } 
                else
                {
                    break;
                }
            }

            return relust;
        }

        public Sign GetNextSign()
        {
            if (index == source.Length) return new Sign(TypeSign.End, index, "#EOS");

            string sign;

            while ((sign = source[index++].ToString()) == " ");
                
            if (operatorToParse.IndexOf(sign) != -1)
            {
                return new Sign(TypeSign.Operator, index, sign);
            }

            if ("1234567890".IndexOf(sign) != -1)
            {
                return new Sign(TypeSign.Number,index, "a", ParseNumber(sign));
            }

            return new Sign(TypeSign.Error, index, sign);
        }

    }
}
