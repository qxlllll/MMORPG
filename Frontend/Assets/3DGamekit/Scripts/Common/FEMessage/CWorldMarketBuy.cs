using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class CWorldMarketBuy : Message
    {
        public CWorldMarketBuy() : base(Command.C_WORLD_MARKET_BUY) { }
        public Dictionary<String, int> buy_info=new Dictionary<string, int>();
    }
}
