using System;

namespace Common
{
    [Serializable]
    public class CSell : Message
    {
        public CSell() : base(Command.C_SELL) { }
        public string to_sell;
        public int to_sell_price;
    }
}
