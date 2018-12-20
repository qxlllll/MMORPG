using System;
using System.Collections.Generic;
using TMPro;

namespace Common
{
    [Serializable]
    public class SGetSellingInventory : Message
    {
        public SGetSellingInventory() : base(Command.S_GET_SELLING_INVENTORY) { }

        public Dictionary<String, String> player_selling_Inventory;
    }
}
