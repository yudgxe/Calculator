using System;
using System.Collections.Generic;
using System.Text;

namespace Сalculator
{
    class RPN
    {
        public Stack<string> RPNstack { get; set; }

        private Stack<string> Rpn;

        public RPN()
        {
            RPNstack = new Stack<string>();
            Rpn = new Stack<string>();

            RPNstack.Push("#E");
            RPNstack.Push("#E");
        }


        public void PushNonTerminal(List<string> toPush)
        {
            string top = RPNstack.Pop();

            if ("+-/*".IndexOf(top) != -1)
                Rpn.Push(top);

            for (int i = toPush.Count - 1; i >= 0; i--)
                RPNstack.Push(toPush[i]);
            
        }

        public void PushTerminal(Sign sign)
        {
            string top = RPNstack.Peek();

            if(sign.Type == TypeSign.Number && top == "a")
                Rpn.Push(sign.Digit);

            if("+-/*".IndexOf(top) != -1)
                Rpn.Push(top);

            RPNstack.Pop();
        }


        public string Execute()
        {
            List<string> listRpn = new List<string>(Rpn.ToArray());
            Stack<string> addStack = new Stack<string>();

            for(int i = listRpn.Count - 1; i >= 0; i--)
            {
                if ("+-/*".IndexOf(listRpn[i]) != -1)
                {
                    float
                        numberSecond = float.Parse(addStack.Pop()),
                        numberFirst = float.Parse(addStack.Pop());

                    switch (listRpn[i]) {
                        case "+":
                            addStack.Push((numberFirst + numberSecond).ToString());
                            break;

                        case "-":
                            addStack.Push((numberFirst - numberSecond).ToString());
                            break;

                        case "/":
                            addStack.Push(numberSecond == 0 ? return "Division by 0" : (numberFirst / numberSecond).ToString());
                            break;

                        case "*":
                            addStack.Push((numberFirst * numberSecond).ToString());
                            break;
                    }
                    continue;
                }

                addStack.Push(listRpn[i]);
            }

            return addStack.Pop(); 
        }

    }
}
