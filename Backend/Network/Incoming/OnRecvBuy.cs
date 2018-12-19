using Common;
using Backend.Game;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvBuy(IChannel channel, Message message)
        {
            CBuy request = message as CBuy;
            Player player = (Player)channel.GetContent();
            SPlayerAttribute response = new SPlayerAttribute();

            Console.WriteLine("Buying");
            Console.WriteLine(request.sum_gold_price);
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            var cmd = new NpgsqlCommand(string.Format("SELECT gold_coins,silver_coins FROM users WHERE name = '{0}'", player.user), conn);
            Console.WriteLine("Select finished");
            Console.WriteLine(player.user);
            var reader = cmd.ExecuteReader();
            reader.Read();
            Console.WriteLine(reader.GetInt16(0));
            Console.WriteLine(reader.GetInt16(1));
            int remain_gold_coins=reader.GetInt16(0)- request.sum_gold_price;
            int remain_silver_coins = reader.GetInt16(1) - request.sum_silver_price;
            reader.Close();
            Console.WriteLine(remain_gold_coins);
            Console.WriteLine(remain_silver_coins);
            Dictionary<String, int> products = new Dictionary<string, int>();
            products = request.products;
            foreach (KeyValuePair<string, int> kvp in products)
            {
                Console.Write("{0},{1}  ", kvp.Key,kvp.Value);
            }

            if (request.buy_by_gold==1)
            {
                var conn2 = new NpgsqlConnection(connString);
                conn2.Open();
                Console.WriteLine("writing gold_coins");
                var cmd2 = new NpgsqlCommand(string.Format("UPDATE users SET gold_coins='{0}' WHERE name = '{1}';", remain_gold_coins, player.user), conn2);
                var reader2 = cmd2.ExecuteReader();
                reader2.Read();
                Console.WriteLine("write coins finished");
                ClientTipInfo(channel, string.Format("Bought successfully,your remainning gold_coin is {0}", remain_gold_coins));
                player.gold_coins = remain_gold_coins;
                reader2.Close();
            }else if(request.buy_by_silver==1)
            {
                var conn2 = new NpgsqlConnection(connString);
                conn2.Open();
                Console.WriteLine("writing silver_coins");
                var cmd2 = new NpgsqlCommand(string.Format("UPDATE users SET silver_coins='{0}' WHERE name = '{1}';", remain_silver_coins, player.user), conn2);
                var reader2 = cmd2.ExecuteReader();
                reader2.Read();
                Console.WriteLine("write coins finished");
                ClientTipInfo(channel, string.Format("Bought successfully,your remainning silver_coin is {0}", remain_silver_coins));
                player.silver_coins = remain_silver_coins;
                reader2.Close();
            }
            

            
            Console.WriteLine("writing products");
            foreach (KeyValuePair<string, int> kvp in products)
            {
                Console.WriteLine("iterating");
                int current_num=0;
                var conn3 = new NpgsqlConnection(connString);
                conn3.Open();
                var cmd3 = new NpgsqlCommand(string.Format("SELECT item_num FROM users_items WHERE item_name = '{0}'and user_name='{1}';", kvp.Key,player.user), conn3);
                Console.WriteLine("conn3 select");
                var reader3 = cmd3.ExecuteReader();
                int count = 0;
                while (reader3.Read())
                {
                    count++;
                    current_num = reader3.GetInt16(0);
                }
                reader3.Close();
                Console.WriteLine(count);
                if (count!=0)
                {
                    
                    Console.WriteLine("if 1");
                    Console.WriteLine(current_num);
                    var conn4 = new NpgsqlConnection(connString);
                    conn4.Open();
                    var cmd4 = new NpgsqlCommand(string.Format("update users_items set item_num=item_num+{0} where user_name='{1}'and item_name='{2}';",kvp.Value,player.user, kvp.Key), conn4);
                    var reader4 = cmd4.ExecuteReader();
                    reader4.Read();
                    reader4.Close();
                    Console.WriteLine("success if 1");
                }
                else
                {
                    Console.WriteLine("if 2");
                    var conn4 = new NpgsqlConnection(connString);
                    conn4.Open();
                    var cmd4 = new NpgsqlCommand(string.Format("INSERT INTO users_items VALUES ('{0}','{1}',{2});", kvp.Key, player.user,kvp.Value), conn4);
                    var reader4 = cmd4.ExecuteReader();
                    reader4.Read();
                    reader4.Close();
                    Console.WriteLine("success if 2");
                }
                
                
            }
            Console.WriteLine("write products finished");
            response.InteligenceValue = player.InteligenceValue;
            response.SpeedValue = player.SpeedValue;
            response.LevelValue = player.LevelValue;
            response.AttackValue = player.AttackValue;
            response.DefenseValue = player.DefenseValue;
            response.gold_coins = player.gold_coins;
            response.silver_coins = player.silver_coins;
            channel.Send(response);

        }
    }
}
