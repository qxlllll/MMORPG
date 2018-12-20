using Common;
using Backend.Game;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvGetSellingInventory(IChannel channel, Message message)
        {
            CGetSellingInventory request = message as CGetSellingInventory;
            Player player = (Player)channel.GetContent();
            SGetSellingInventory response = new SGetSellingInventory();

            Console.WriteLine("Getting Selling Inventory");
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            var cmd = new NpgsqlCommand(string.Format("SELECT item_id,item FROM worldmarket WHERE seller = '{0}' and state=0;", player.user), conn);
            Console.WriteLine("Select selling items finished");
            var reader = cmd.ExecuteReader();
            string item_name="";
            string item_id="";
            player.selling_Inventory.Clear();
            while (reader.Read())
            {
                item_id = Convert.ToString(reader["item_id"]);
                item_name = Convert.ToString(reader["item"]); // 获得指定字段的值
                player.selling_Inventory.Add(item_id, item_name);

            }
            reader.Close();
               
            /*foreach (KeyValuePair<string, string> kvp in player.selling_Inventory)
            {
                Console.WriteLine(kvp.Key + " "+kvp.Value);
            }*/
            response.player_selling_Inventory = player.selling_Inventory;
            channel.Send(response);
        }
           
    }
}
