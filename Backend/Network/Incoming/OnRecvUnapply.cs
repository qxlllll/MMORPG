using Common;
using Backend.Game;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvUnapply(IChannel channel, Message message)
        {
            CUnapply request = message as CUnapply;
            //SPlayerAttribute response = new SPlayerAttribute();
            //SGetInventory response2 = new SGetInventory();
            Player player = (Player)channel.GetContent();

            

        }
    }
}
