using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class CGetWorldMarket : Message
    {
        public CGetWorldMarket() : base(Command.C_GET_WORLD_MARKET) { }
        
    }
}
