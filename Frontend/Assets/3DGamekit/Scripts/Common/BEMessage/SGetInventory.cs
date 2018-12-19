using System;
using System.Collections.Generic;
using TMPro;

namespace Common
{
    [Serializable]
    public class SGetInventory : Message
    {
        public SGetInventory() : base(Command.S_GET_INVENTORY) { }

        public Dictionary<String, int> player_Inventory;
    }
}
