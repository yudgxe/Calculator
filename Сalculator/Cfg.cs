using System.Collections.Generic;

namespace Сalculator
{
    class Cfg
    {
        public List<CfgRule> Rules { get; set; } = new List<CfgRule>();

        IEnumerable<string> EnumNonTerminals()
        {
            var seen = new HashSet<string>();

            for (int i = 0; i < Rules.Count; i++) 
            {
                var rule = Rules[i];
                if (seen.Add(rule.Left))
                    yield return rule.Left;
            }
        }

        IEnumerable<string> EnumTerminals()
        {
            var nts = new HashSet<string>();

            for (int i = 0; i < Rules.Count; i++) 
                nts.Add(Rules[i].Left);
            

            var seen = new HashSet<string>();

            for(int i = 0; i < Rules.Count; i++)
            {
                var rule = Rules[i];

                for(int j = 0; j < rule.Right.Count; j++)
                {
                    string s = rule.Right[j];
                    if (!nts.Contains(s) && seen.Add(s))
                        yield return s;
                }
            }
        }

        public IDictionary<string, IDictionary<string, CfgRule>> GetTable()
        {
            var result = new Dictionary<string, IDictionary<string, CfgRule>>();

            foreach (var rl in EnumNonTerminals())
            {
                var d = new Dictionary<string, CfgRule>();
                foreach (var rr in Rules)
                {
                    if (rl == rr.Left)
                    {
                        if (rr.Right.Count == 0)
                        {
                            d.Add("#EOS", rr);

                            foreach (var terminal in EnumTerminals())
                                if(!d.ContainsKey(terminal))
                                    d.Add(terminal, rr);
                        }
                        else 
                            d.Add(rr.Right[0], rr);
                    }
                }

                result.Add(rl, d);
            }
            return result;
        }
    }
}
