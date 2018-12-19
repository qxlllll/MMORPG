using Common;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvMarketAttribute(IChannel channel, Message message)
        {

            SMarketAttribute msg = message as SMarketAttribute;
            Debug.Log("OnRecvMarketAttribute");
            FMarket.all_item_type = msg.all_item_type;
            FMarket.all_item_value = msg.all_item_value;
            FMarket.all_gold_price = msg.all_gold_price;
            FMarket.all_silver_price = msg.all_silver_price;
            FMarket.all_item_durable = msg.all_item_durable;

        }
    }
}
