using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class CGetInventory : Message
    {
        public CGetInventory() : base(Command.C_GET_INVENTORY) { }
        
    }
}
