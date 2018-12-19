using System;
using System.Collections.Generic;
using TMPro;

namespace Common
{
    [Serializable]
    public class SWorldMarketAttribute : Message
    {
        public SWorldMarketAttribute() : base(Command.S_WORLD_MARKET_ATTRIBUTE) { }

        public Dictionary<String, String> world_market_item_name;
        public Dictionary<String, String> world_market_item_seller;
        public Dictionary<String, int> world_market_item_price;
    }
}
