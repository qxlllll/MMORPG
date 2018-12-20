using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class CGetSellingInventory : Message
    {
        public CGetSellingInventory() : base(Command.C_GET_SELLING_INVENTORY) { }
        
    }
}
