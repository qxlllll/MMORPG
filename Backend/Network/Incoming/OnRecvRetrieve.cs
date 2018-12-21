using Common;
using Backend.Game;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvRetrieve(IChannel channel, Message message)
        {
            CRetrieve request = message as CRetrieve;
            
            SGetInventory response2 = new SGetInventory();
            Player player = (Player)channel.GetContent();
            string item_id = request.to_retrieve;
            string item_name = player.world_market_item_name[item_id];
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            var sqlstring = "";

            sqlstring = string.Format("INSERT INTO users_items VALUES ('{0}','{1}',1);", item_name, player.user);
            int count = -1;
            var conn0 = new NpgsqlConnection(connString);
            conn0.Open();
            var cmd0 = new NpgsqlCommand(string.Format("SELECT COUNT(*) FROM users_items WHERE user_name='{0}' AND item_name='{1}';", player.user, item_name), conn0);
            var reader0 = cmd0.ExecuteReader();
            reader0.Read();
            count = reader0.GetInt16(0);
            reader0.Close();
            Console.WriteLine(count);
            if (count != 0)
            {
                Console.WriteLine("contains");
                sqlstring = string.Format("update users_items set item_num=item_num+1 where user_name='{0}'and item_name='{1}';", player.user, item_name);
                //Console.WriteLine(sqlstring);
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
            var cmd4 = new NpgsqlCommand(string.Format("UPDATE worldmarket SET state=-1 WHERE item_id='{0}'", item_id), conn4);
            var reader4 = cmd4.ExecuteReader();
            reader4.Read();
            reader4.Close();
            Console.WriteLine("change market state finished");





            player.Inventory.Clear();
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            var cmd = new NpgsqlCommand(string.Format("SELECT item_name,item_num from users_items WHERE user_name = '{0}'", player.user), conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                item_name = Convert.ToString(reader["item_name"]); // 获得指定字段的值
                player.Inventory.Add(item_name, Convert.ToInt16(reader["item_num"]));

            }
            reader.Close();
            response2.player_Inventory = player.Inventory;
            channel.Send(response2);
            ClientTipInfo(channel, "Retrieved");


        }
    }
}
