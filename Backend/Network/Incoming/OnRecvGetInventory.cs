using Common;
using Backend.Game;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvGetInventory(IChannel channel, Message message)
        {
            CGetInventory request = message as CGetInventory;
            Player player = (Player)channel.GetContent();
            SGetInventory response = new SGetInventory();

            Console.WriteLine("Getting Inventory");
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            var cmd = new NpgsqlCommand(string.Format("SELECT item_name,item_num from users_items WHERE user_name = '{0}'", player.user), conn);
            Console.WriteLine("Select finished");
            var reader = cmd.ExecuteReader();
            String item_name;
            while (reader.Read())
            {

                item_name = Convert.ToString(reader["item_name"]); // 获得指定字段的值
                //market.item.item_type  = Convert.ToString(reader["item_type"]);
                player.Inventory.Add(item_name, Convert.ToInt16(reader["item_num"]));

            }
            reader.Close();

            foreach (KeyValuePair<string, Int16> kvp in player.Inventory)
            {
                Console.Write("{0},{1}  ", kvp.Key, kvp.Value);
            }
            response.player_Inventory = player.Inventory;
            channel.Send(response);
        }
           
    }
}
