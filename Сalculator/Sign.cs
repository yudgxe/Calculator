using System;
using System.Collections.Generic;
using System.Text;

namespace Сalculator
{
    enum TypeSign
    {
        Operator = 1,
        Number = 2,
        Error = 3,
        End = 4
    }
    struct Sign
    {
        public string Digit { get; set; }
        public string Value { get; set; }
        public TypeSign Type { get; set; }
        public int Position { get; set; }
        public Sign(TypeSign type, int position, string value = null, string digit = null)
        {
            Value = value;
            Type = type;
            Position = position;
            Digit = digit;
        }

    }
}
