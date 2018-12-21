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
            SPlayerAttribute response = new SPlayerAttribute();
            //SGetInventory response2 = new SGetInventory();
            Player player = (Player)channel.GetContent();

            string item_name = request.to_unapply;
            int item_value = player.all_item_value[item_name];
            string item_type = player.all_item_type[item_name];

            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            var sqlstring = "";
            var sqlstring2 = "";
            if (item_type.Equals("defense"))
            {
                Console.WriteLine("if defense");
                player.defense_item = "0";
                sqlstring = string.Format("update users set {0}={1}-{2} where name='{3}';", item_type, player.DefenseValue,item_value, player.user);
                sqlstring2 = string.Format("update users set defense_item='0' WHERE name='{0}'",  player.user);
            }
            if (item_type.Equals("attack"))
            {
                Console.WriteLine("if attack");
                player.attack_item = "0";
                sqlstring = string.Format("update users set {0}={1}-{2} where name='{3}';", item_type, player.AttackValue, item_value, player.user);
                sqlstring2 = string.Format("update users set defense_item='0' WHERE name='{0}'", player.user);
            }
            if (item_type.Equals("inteligence"))
            {
                Console.WriteLine("if inteligence");
                player.inteligence_item = "0";
                sqlstring = string.Format("update users set {0}={1}-{2} where name='{3}';", item_type, player.InteligenceValue, item_value, player.user);
                sqlstring2 = string.Format("update users set defense_item='0' WHERE name='{0}'", player.user);
            }
            if (item_type.Equals("speed"))
            {
                Console.WriteLine("if speed");
                player.speed_item = "0";
                sqlstring = string.Format("update users set {0}={1}-{2} where name='{3}';", item_type, player.SpeedValue, item_value, player.user);
                sqlstring2 = string.Format("update users set defense_item='0' WHERE name='{0}'", player.user);
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
            Console.WriteLine(" item and attribute update finished");

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


            ClientTipInfo(channel, "Unapplied successfully");
        }
    }
}
