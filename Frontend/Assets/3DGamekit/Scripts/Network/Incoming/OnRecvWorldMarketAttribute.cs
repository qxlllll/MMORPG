using Common;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvWorldMarketAttribute(IChannel channel, Message message)
        {

            SWorldMarketAttribute msg = message as SWorldMarketAttribute;
            Debug.Log("OnRecvWorldMarketAttribute");
            WorldMarket.world_market_item_name.Clear();
            WorldMarket.world_market_item_price.Clear();
            WorldMarket.world_market_item_seller.Clear();
            WorldMarket.world_market_item_name = msg.world_market_item_name;
            WorldMarket.world_market_item_seller = msg.world_market_item_seller;
            WorldMarket.world_market_item_price = msg.world_market_item_price;

        }
    }
}
