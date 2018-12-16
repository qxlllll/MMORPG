using Common;
using Backend.Game;
using Npgsql;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvPlayerEnter(IChannel channel, Message message)
        {
            CPlayerEnter request = message as CPlayerEnter;
            SPlayerAttribute response = new SPlayerAttribute();
            Player player = (Player)channel.GetContent();
           
            Scene scene = World.Instance.GetScene(player.scene);
            // add the player to the scene
            player.Spawn();
            scene.AddEntity(player);
            response.InteligenceValue = player.InteligenceValue;
            response.SpeedValue = player.SpeedValue;
            response.LevelValue = player.LevelValue;
            response.AttackValue = player.AttackValue;
            response.DefenseValue = player.DefenseValue;
            response.gold_coins = player.gold_coins;
            channel.Send(response);

            
            SMarketAttribute market_response = new SMarketAttribute();
            Console.WriteLine("Getting market items");
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            var cmd = new NpgsqlCommand("SELECT item_name,item_type,item_value,gold_price,silver_price FROM cartitems ", conn);
            Console.WriteLine("sql finished");
            var reader = cmd.ExecuteReader();
            //reader.Read();
            //int i = 0;
            String item_name;

            while (reader.Read())
            {
                /*for (i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write("{0}  ", reader[i]); //获得字段名
                }*/
                item_name  = Convert.ToString(reader["item_name"]); // 获得指定字段的值
                //market.item.item_type  = Convert.ToString(reader["item_type"]);
                player.all_item_type.Add(item_name, Convert.ToString(reader["item_type"]));
                player.all_item_value.Add(item_name, Convert.ToInt16(reader["item_value"]));
                player.all_gold_price.Add(item_name, Convert.ToInt16(reader["gold_price"]));
                player.all_silver_price.Add(item_name, Convert.ToInt16(reader["silver_price"]));
            }
            reader.Close();
            //market_response.items = market.items;
            market_response.all_item_type = player.all_item_type;
            market_response.all_item_value = player.all_item_value;
            market_response.all_gold_price = player.all_gold_price;
            market_response.all_silver_price = player.all_silver_price;
            /*foreach (KeyValuePair<string, short> kvp in market_response.all_gold_price)
            {
                Console.Write("{0},{1}  ", kvp.Key,kvp.Value);
            }*/
            
            channel.Send(market_response);

        }
    }
}
