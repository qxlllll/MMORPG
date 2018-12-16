using System;
using System.Collections.Generic;
using TMPro;

namespace Common
{
    [Serializable]
    public class SMarketAttribute : Message
    {
        public SMarketAttribute() : base(Command.S_MARKET_ATTRIBUTE) { }
        /*public struct CartItem
        {
            public String item_name;
            public String item_type;
            public Int16 item_value;
            public Int16 gold_price;
            public Int16 silver_price;

        }*/
        public Dictionary<String, String> all_item_type;
        public Dictionary<String, Int16> all_item_value;
        public Dictionary<String, Int16> all_gold_price;
        public Dictionary<String, Int16> all_silver_price;
    }
}
