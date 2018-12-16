using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class CBuy : Message
    {
        public CBuy() : base(Command.C_BUY) { }
        public int sum_gold_price;
        public int sum_silver_price;
        public int buy_by_gold = 0;
        public int buy_by_silver = 0;
        public Dictionary<String, int> products=new Dictionary<string, int>();
    }
}
