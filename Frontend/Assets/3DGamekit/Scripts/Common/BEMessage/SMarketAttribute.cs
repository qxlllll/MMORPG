using System;
using System.Collections.Generic;
using TMPro;

namespace Common
{
    [Serializable]
    public class SMarketAttribute : Message
    {
        public SMarketAttribute() : base(Command.S_MARKET_ATTRIBUTE) { }
        public Dictionary<String, String> all_item_type;
        public Dictionary<String, int> all_item_value;
        public Dictionary<String, int> all_gold_price;
        public Dictionary<String, int> all_silver_price;
        public Dictionary<String, String> all_item_durable;
    }
}
