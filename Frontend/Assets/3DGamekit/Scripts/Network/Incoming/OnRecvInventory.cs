using Common;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvInventory(IChannel channel, Message message)
        {

            SGetInventory msg = message as SGetInventory;
            Inventory.player_Inventory = msg.player_Inventory;
        }
    }
}
