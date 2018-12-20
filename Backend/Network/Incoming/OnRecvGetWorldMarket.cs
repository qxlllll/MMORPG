using Common;
using Backend.Game;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvGetWorldMarket(IChannel channel, Message message)
        {
            CGetWorldMarket request = message as CGetWorldMarket;
            Player player = (Player)channel.GetContent();
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            SWorldMarketAttribute response = new SWorldMarketAttribute();
            Console.WriteLine("Getting world market items");
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            var cmd = new NpgsqlCommand("SELECT item_id,item,seller,price,state FROM worldmarket", conn);
            Console.WriteLine("sql finished");
            var reader = cmd.ExecuteReader();
            String world_market_item_id;
            player.world_market_item_name.Clear();
            player.world_market_item_price.Clear();
            player.world_market_item_seller.Clear();
            while (reader.Read())
            {
                if (Convert.ToInt16(reader["state"])==0)
                {
                    world_market_item_id = Convert.ToString(reader["item_id"]);
                    //Console.WriteLine(reader["item_id"]);
                    player.world_market_item_name.Add(world_market_item_id, Convert.ToString(reader["item"]));
                    //Console.WriteLine(reader["item"]);
                    player.world_market_item_seller.Add(world_market_item_id, Convert.ToString(reader["seller"]));
                    //Console.WriteLine(reader["seller"]);
                    player.world_market_item_price.Add(world_market_item_id, Convert.ToInt16(reader["price"]));
                    //Console.WriteLine(reader["price"]);
                }


            }
            reader.Close();
            response.world_market_item_name = player.world_market_item_name;
            response.world_market_item_seller = player.world_market_item_seller;
            response.world_market_item_price = player.world_market_item_price;
            channel.Send(response);
        }
           
    }
}
