using Common;
using Backend.Game;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvSell(IChannel channel, Message message)
        {
            CSell request = message as CSell;
            //SPlayerAttribute response = new SPlayerAttribute();
            //SGetInventory response2 = new SGetInventory();
            Player player = (Player)channel.GetContent();

            int flag = 0;

            Console.WriteLine(request.to_sell);
            if(request.to_sell!=player.attack_item && request.to_sell!=player.defense_item && request.to_sell!=player.speed_item && request.to_sell!=player.inteligence_item)
            {
                Console.WriteLine("Adding");
                flag = 1;
            }
           
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            var sqlstring = "";
            var item_id = "";
            var sqlstring2 = "";
            sqlstring2 = string.Format("SELECT item_id FROM worldmarket WHERE seller='{0}'", player.user);
            var conn2 = new NpgsqlConnection(connString);
            conn2.Open();
            var cmd2 = new NpgsqlCommand(sqlstring2, conn2);
            var reader2 = cmd2.ExecuteReader();
            int count = 0;
            var sqlstring3 = "";
            while(reader2.Read())
            {
                count++;
            }
            reader2.Close();
            int user_selling_items = count+1;
            Console.WriteLine(user_selling_items);
            if (flag == 1)
            {
                item_id = string.Format("{0}_{1}",player.user, user_selling_items);
                Console.WriteLine(item_id);
                sqlstring = string.Format("INSERT INTO worldmarket VALUES ('{0}','{1}','{2}',{3});", item_id,request.to_sell, player.user,request.to_sell_price);
                var conn = new NpgsqlConnection(connString);
                conn.Open();
                var cmd = new NpgsqlCommand(sqlstring, conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                reader.Close();

                sqlstring3 = string.Format("UPDATE users_items SET item_num=item_num-1 WHERE user_name = '{0}'and item_name='{1}';",  player.user, request.to_sell);
                var conn3 = new NpgsqlConnection(connString);
                conn3.Open();
                var cmd3 = new NpgsqlCommand(sqlstring3, conn3);
                var reader3 = cmd3.ExecuteReader();
                reader3.Read();
                reader3.Close();

                ClientTipInfo(channel, "Added to worldmarket");
            }
            if (flag==0)
            {
                ClientTipInfo(channel, "Unapply first");
            }
            

        }
    }
}
