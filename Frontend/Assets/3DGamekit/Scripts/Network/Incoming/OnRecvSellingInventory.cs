using Common;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvSellingInventory(IChannel channel, Message message)
        {

            SGetSellingInventory msg = message as SGetSellingInventory;
            Inventory.player_selling_Inventory = msg.player_selling_Inventory;
            /*foreach (KeyValuePair<string, string> kvp in Inventory.player_selling_Inventory)
            {
                Debug.Log(kvp.Key + " " + kvp.Value);
            }*/
        }
    }
}
