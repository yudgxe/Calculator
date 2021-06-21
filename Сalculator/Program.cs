using System.Collections.Generic;
using System;

namespace Сalculator
{
    class Program
    {

        /*  Грамматика 
         *  S → (S)VU | aVU
         *  U → +TU | -TU | λ
         *  T → (S)V | aV
         *  V → ∗FV | /FV | λ
         *  F → (S) | a
         */
        static void Main(string[] args)
        {
            Cfg cfg = new Cfg();

            cfg.Rules.Add(new CfgRule("S", new List<string> { "(", "S", ")", "V", "U" }, new List<string> { "#E", "#E", "#E", "#E", "#E" }));
            cfg.Rules.Add(new CfgRule("S", new List<string> { "a", "V", "U" }, new List<string> { "a", "#E", "#E" }));

            cfg.Rules.Add(new CfgRule("U", new List<string> { "+", "T", "U" }, new List<string> { "#E", "#E", "+" }));
            cfg.Rules.Add(new CfgRule("U", new List<string> { "-", "T", "U" }, new List<string> { "#E", "#E", "-" }));
            cfg.Rules.Add(new CfgRule("U", new List<string>(), new List<string>()));

            cfg.Rules.Add(new CfgRule("T", new List<string> { "(", "S", ")", "V" }, new List<string> { "#E", "#E", "#E", "#E" }));
            cfg.Rules.Add(new CfgRule("T", new List<string> { "a", "V" }, new List<string> { "a", "#E" }));

            cfg.Rules.Add(new CfgRule("V", new List<string> { "*", "F", "V" }, new List<string> { "#E", "#E", "*" }));
            cfg.Rules.Add(new CfgRule("V", new List<string> { "/", "F", "V" }, new List<string> { "#E", "#E", "/" }));
            cfg.Rules.Add(new CfgRule("V", new List<string>(), new List<string>()));


            cfg.Rules.Add(new CfgRule("F", new List<string> { "(", "S", ")" }, new List<string> { "#E", "#E", "#E" }));
            cfg.Rules.Add(new CfgRule("F", new List<string> { "a" }, new List<string> { "a" }));


            string input = Console.ReadLine();
            Analyzer LL = new Analyzer(cfg.GetTable(), "S", input);
            Console.WriteLine(LL.Calculate());
        }
    }
}
