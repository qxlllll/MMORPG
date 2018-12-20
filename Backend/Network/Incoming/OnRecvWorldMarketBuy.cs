using Common;
using Backend.Game;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvWorldMarketBuy(IChannel channel, Message message)
        {
            CWorldMarketBuy request = message as CWorldMarketBuy;
            Player player = (Player)channel.GetContent();
            //SPlayerAttribute response = new SPlayerAttribute();
            
            Dictionary<String, int> buy_info=new Dictionary<string, int>();
            buy_info = request.buy_info;

            Console.WriteLine("Buying from world market");
            string item_id = "";
            string seller = "";
            string item_name = "";
            int item_price = 0;
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            string sqlstring = "";
            foreach (var kv in buy_info)
            {
                item_id = kv.Key;
                item_price = kv.Value;
                seller = player.world_market_item_seller[item_id];
                item_name = player.world_market_item_name[item_id];
                
                var conn = new NpgsqlConnection(connString);
                conn.Open();
                var cmd = new NpgsqlCommand(string.Format("UPDATE users SET silver_coins=silver_coins-{0} WHERE name = '{1}';",item_price , player.user), conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                reader.Close();
                Console.WriteLine("reduce buyer money finished");

                var conn2 = new NpgsqlConnection(connString);
                conn2.Open();
                var cmd2 = new NpgsqlCommand(string.Format("UPDATE users SET silver_coins=silver_coins+{0} WHERE name = '{1}';", item_price , seller), conn2);
                var reader2 = cmd2.ExecuteReader();
                reader2.Read();
                reader2.Close();
                Console.WriteLine("add seller money finished");

                sqlstring = string.Format("INSERT INTO users_items VALUES ('{0}','{1}',1);", item_name, player.user);
                int sValue = 0;
                Console.WriteLine(player.Inventory.TryGetValue(item_name, out sValue));
                if (player.Inventory.TryGetValue(item_name, out sValue))
                {
                    Console.WriteLine("contains");
                    sqlstring = string.Format("update users_items set item_num=item_num+1 where user_name='{0}'and item_name='{1}';", player.user, item_name);
                    Console.WriteLine(sqlstring);
                }

                var conn3 = new NpgsqlConnection(connString);
                conn3.Open();
                var cmd3 = new NpgsqlCommand(sqlstring, conn3);
                var reader3 = cmd3.ExecuteReader();
                reader3.Read();
                reader3.Close();
                Console.WriteLine("add to buyer inventory finished");

                var conn4 = new NpgsqlConnection(connString);
                conn4.Open();
                Console.WriteLine("deleting "+item_id);
                Console.WriteLine(string.Format("DELETE FROM worldmarket WHERE item_id='{0}'", item_id));
                var cmd4 = new NpgsqlCommand(string.Format("DELETE FROM worldmarket WHERE item_id='{0}'", item_id), conn4);
                var reader4 = cmd4.ExecuteReader();
                reader4.Read();
                reader4.Close();
                Console.WriteLine("delete from market finished");
            }
            
            
            
        }
    }
}
