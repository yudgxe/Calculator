using System;
using System.Collections.Generic;

namespace Сalculator
{
   
    class Analyzer
    {
        private Stack<string> stack = new Stack<string>();

        private IDictionary<string, IDictionary<string, CfgRule>> table;

        private string input;

        public RPN rpn;

        void PushStack(List<string> arr, Stack<string> stack)
        {
            for (int i = arr.Count - 1; i >= 0; i--) 
                stack.Push(arr[i]);
            
        }

        void PrintError(Sign sign, IDictionary<string, CfgRule> rules = null)
        {
            Console.Write("Ошибка, ожидался символ: ");

            if (rules != null)
                foreach (var s in rules)
                    Console.Write(s.Key + " ");

            Console.Write("вместо: {0} номер: {1}", sign.Value, sign.Position); 

        }
        public Analyzer(IDictionary<string, IDictionary<string, CfgRule>> table, string startNonTerminal, string input)
        {
            this.table = table;
            this.input = input;

            stack.Push("#END");
            stack.Push(startNonTerminal);
        }


        public string Calculate()
        { 
            Parser parser = new Parser(input);
            Sign sign = parser.GetNextSign();

            rpn = new RPN();

            string top;

            while((top = stack.Peek()) != "#END")
            {
                if (sign.Type == TypeSign.Error)
                {
                    Console.WriteLine("Ошибка, символ: {0} не поддерживается, номер {1}", sign.Value, sign.Position);
                    return null;
                }

                if (table.ContainsKey(top))
                {
                    if (table[top].ContainsKey(sign.Value))
                    {
                        stack.Pop();
                        rpn.PushNonTerminal(table[top][sign.Value].RPNRight);
                        PushStack(table[top][sign.Value].Right, stack);
                    }
                    else
                    {
                        PrintError(sign, table[top]);
                        return null;
                    }
                }
                else
                {
                    if (top == sign.Value)
                    {
                        stack.Pop();
                        rpn.PushTerminal(sign);
                        sign = parser.GetNextSign();
                    }
                    else 
                    {
                        Console.WriteLine("Ошибка, ожидался символ: {0} вместо: {1} номер: {2}", top, sign.Value, sign.Position);
                        return null;
                    }
                        
                }
            }

            if (sign.Type != TypeSign.End)
            {
                Console.WriteLine("Ошибка, ожидался символ: {0} вместо: {1} номер: {2}", top, sign.Value, sign.Position);
                return null;
            }
                
            return rpn.Execute();
        }
    }
}
