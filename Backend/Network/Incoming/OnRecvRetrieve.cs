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
            CApply request = message as CApply;
            SPlayerAttribute response = new SPlayerAttribute();
            SGetInventory response2 = new SGetInventory();
            Player player = (Player)channel.GetContent();

            Console.WriteLine("Applying");
            Console.WriteLine(request.to_apply);
            int item_value=0;
            int current_defense_item_value = 0;
            int current_attack_item_value = 0;
            int current_speed_item_value = 0;
            int current_inteligence_item_value = 0;
            String item_type="";
            String if_durable="";
            foreach (KeyValuePair<string, int> kvp in player.all_item_value)
            {
                if (kvp.Key.Equals(request.to_apply))
                {
                    item_value = kvp.Value;
                    Console.WriteLine(item_value);
                }
                if (kvp.Key.Equals(player.defense_item))
                {
                    current_defense_item_value = kvp.Value;
                }
                if (kvp.Key.Equals(player.attack_item))
                {
                    current_attack_item_value = kvp.Value;
                }
                if (kvp.Key.Equals(player.speed_item))
                {
                    current_speed_item_value = kvp.Value;
                }
                if (kvp.Key.Equals(player.inteligence_item))
                {
                    current_inteligence_item_value = kvp.Value;
                }

            }
            foreach (KeyValuePair<string, string> kvp in player.all_item_type)
            {
                if (kvp.Key.Equals(request.to_apply))
                {
                    item_type = kvp.Value;
                    Console.WriteLine(item_type);
                }
            }
            foreach (KeyValuePair<string, string> kvp in player.all_item_durable)
            {
                if (kvp.Key.Equals(request.to_apply))
                {
                    if_durable = kvp.Value;
                    Console.WriteLine(if_durable);
                }
            }
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            var sqlstring = "";
            var sqlstring2 = "";
            if (if_durable.Equals("1"))
            {
                Console.WriteLine("not durable");
                if (item_type.Equals("defense"))
                {
                    Console.WriteLine("if defense");
                    sqlstring = string.Format("update users set {0}={1}+{2} where name='{3}';", item_type, player.DefenseValue, item_value, player.user);
                    Console.WriteLine(sqlstring);
                }
                if (item_type.Equals("attack"))
                {
                    Console.WriteLine("if attack");
                    sqlstring = string.Format("update users set {0}={1}+{2} where name='{3}';", item_type, player.AttackValue, item_value, player.user);
                }
                if (item_type.Equals("inteligence"))
                {
                    Console.WriteLine("if inteligence");
                    sqlstring = string.Format("update users set {0}={1}+{2} where name='{3}';", item_type, player.InteligenceValue, item_value, player.user);
                }
                if (item_type.Equals("speed"))
                {
                    Console.WriteLine("if speed");
                    sqlstring = string.Format("update users set {0}={1}+{2} where name='{3}';", item_type, player.SpeedValue, item_value, player.user);
                }
                var conn3 = new NpgsqlConnection(connString);
                conn3.Open();
                var cmd3 = new NpgsqlCommand(string.Format("update users_items set item_num=item_num-1 where user_name='{0}'and item_name='{1}';", player.user, request.to_apply), conn3);
                var reader3 = cmd3.ExecuteReader();
                reader3.Read();
                reader3.Close();
                Console.WriteLine("update item number finished");
            }
            else if(if_durable.Equals("n"))
            {
                Console.WriteLine("if durable");
                
                if (item_type.Equals("defense"))
                {
                    Console.WriteLine("if defense");
                    player.defense_item = request.to_apply;
                    sqlstring = string.Format("update users set {0}={1}-{2}+{3} where name='{4}';", item_type, player.DefenseValue, current_defense_item_value, item_value, player.user);
                    sqlstring2 = string.Format("update users set defense_item='{0}' WHERE name='{1}'", request.to_apply,player.user);
                }
                if (item_type.Equals("attack"))
                {
                    Console.WriteLine("if attack");
                    player.attack_item = request.to_apply;
                    sqlstring = string.Format("update users set {0}={1}-{2}+{3} where name='{4}';", item_type, player.AttackValue, current_attack_item_value, item_value, player.user);
                    sqlstring2 = string.Format("update users set attack_item='{0}' WHERE name='{1}'", request.to_apply, player.user);
                }
                if (item_type.Equals("inteligence"))
                {
                    Console.WriteLine("if inteligence");
                    player.inteligence_item = request.to_apply;
                    sqlstring = string.Format("update users set {0}={1}-{2}+{3} where name='{4}';", item_type, player.InteligenceValue, current_inteligence_item_value, item_value, player.user);
                    sqlstring2 = string.Format("update users set inteligence_item='{0}' WHERE name='{1}'", request.to_apply, player.user);
                }
                if (item_type.Equals("speed"))
                {
                    Console.WriteLine("if speed");
                    player.speed_item = request.to_apply;
                    sqlstring = string.Format("update users set {0}={1}-{2}+{3} where name='{4}';", item_type, player.SpeedValue, current_speed_item_value, item_value, player.user);
                    sqlstring2 = string.Format("update users set speed_item='{0}' WHERE name='{1}'", request.to_apply, player.user);
                }
            }
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            var cmd = new NpgsqlCommand(sqlstring, conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            reader.Close();

            var conn5 = new NpgsqlConnection(connString);
            conn5.Open();
            var cmd5 = new NpgsqlCommand(sqlstring2, conn5);
            var reader5 = cmd5.ExecuteReader();
            reader5.Read();
            reader5.Close();
            Console.WriteLine("update finished");

            

            var conn2 = new NpgsqlConnection(connString);
            conn2.Open();
            var cmd2 = new NpgsqlCommand(string.Format("SELECT password,Inteligence,Speed,Level,Attack,Defense,gold_coins,silver_coins,defense_item,attack_item,speed_item,inteligence_item FROM users WHERE name = '{0}';", player.user), conn2);
            var reader2 = cmd2.ExecuteReader();
            reader2.Read();
            player.InteligenceValue = Convert.ToString(reader2.GetInt16(1));
            player.SpeedValue = Convert.ToString(reader2.GetInt16(2));
            player.LevelValue = Convert.ToString(reader2.GetInt16(3));
            player.AttackValue = Convert.ToString(reader2.GetInt16(4));
            player.DefenseValue = Convert.ToString(reader2.GetInt16(5));
            player.gold_coins = reader2.GetInt16(6);
            player.silver_coins = reader2.GetInt16(7);
            player.defense_item = reader2.GetString(8);
            player.attack_item = reader2.GetString(9);
            player.speed_item = reader2.GetString(10);
            player.inteligence_item = reader2.GetString(11);

            Console.WriteLine("sending response");
            response.InteligenceValue = player.InteligenceValue;
            response.SpeedValue = player.SpeedValue;
            response.LevelValue = player.LevelValue;
            response.AttackValue = player.AttackValue;
            response.DefenseValue = player.DefenseValue;
            response.gold_coins = player.gold_coins;
            response.silver_coins = player.silver_coins;
            response.speed_item = player.speed_item;
            response.attack_item = player.attack_item;
            response.defense_item = player.defense_item;
            response.inteligence_item = player.inteligence_item;
            reader2.Close();
            Console.WriteLine("update finished");
            channel.Send(response);

            player.Inventory.Clear();
            var conn4 = new NpgsqlConnection(connString);
            conn4.Open();
            var cmd4 = new NpgsqlCommand(string.Format("SELECT item_name,item_num from users_items WHERE user_name = '{0}'", player.user), conn4);
            var reader4 = cmd4.ExecuteReader();
            String item_name;
            while (reader4.Read())
            {

                item_name = Convert.ToString(reader4["item_name"]); // 获得指定字段的值
                player.Inventory.Add(item_name, Convert.ToInt16(reader4["item_num"]));

            }
            reader4.Close();
            response2.player_Inventory = player.Inventory;
            channel.Send(response2);
            ClientTipInfo(channel, "Applied successfully,refresh to see the update");


        }
    }
}
