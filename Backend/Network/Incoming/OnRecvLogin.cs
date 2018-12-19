using Common;
using Backend.Game;
using Npgsql;
using System;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvLogin(IChannel channel, Message message)
        {
            CLogin request = message as CLogin;
            SPlayerEnter response = new SPlayerEnter();
            string scene = "Level1";
            response.user = request.user;
            //Console.WriteLine(request.user);
            response.token = request.password;
            //Console.WriteLine(request.password);
            response.scene = scene;
            response.exist_or_not=false;

            Player player = new Player(channel);
            player.scene = scene;
            player.user = request.user;

            Console.WriteLine("Connecting PostgreSQL");
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres";
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            Console.WriteLine("Connected PostgreSQL");
            var cmd = new NpgsqlCommand(string.Format("SELECT password,Inteligence,Speed,Level,Attack,Defense,gold_coins,silver_coins,defense_item,attack_item,speed_item,inteligence_item FROM users WHERE name = '{0}'", request.user), conn);
            var reader=cmd.ExecuteReader();
            int count = 0;
            string password = "";
            while (reader.Read())
            {
                count++;
                Console.WriteLine(reader.GetString(0));
                password = reader.GetString(0);

                Console.WriteLine(reader.GetInt16(1));
                player.InteligenceValue = Convert.ToString(reader.GetInt16(1));

                Console.WriteLine(reader.GetInt16(2));
                player.SpeedValue = Convert.ToString(reader.GetInt16(2));

                Console.WriteLine(reader.GetInt16(3));
                player.LevelValue = Convert.ToString(reader.GetInt16(3));

                Console.WriteLine(reader.GetInt16(4));
                player.AttackValue = Convert.ToString(reader.GetInt16(4));

                Console.WriteLine(reader.GetInt16(5));
                player.DefenseValue = Convert.ToString(reader.GetInt16(5));

                Console.WriteLine(reader.GetInt16(6));
                player.gold_coins = reader.GetInt16(6);

                Console.WriteLine(reader.GetInt16(7));
                player.silver_coins = reader.GetInt16(7);

                Console.WriteLine(reader.GetString(8));
                player.defense_item = reader.GetString(8);

                Console.WriteLine(reader.GetString(9));
                player.attack_item = reader.GetString(9);

                Console.WriteLine(reader.GetString(10));
                player.speed_item = reader.GetString(10);

                Console.WriteLine(reader.GetString(11));
                player.inteligence_item = reader.GetString(11);

            }
            Console.WriteLine(count);
            if (count!=0)
            {
                Console.WriteLine("count!=0");
                if (password.Equals(request.password))
                {
                    Console.WriteLine("Comparing yes");
                    ClientTipInfo(channel, "Entering");
                    response.exist_or_not = true;
                    channel.Send(response);
                }
                else
                {
                    Console.WriteLine("Comparing no");
                    ClientTipInfo(channel, "Wrong password");
                    channel.Send(response);
                }
            }
            else
            {
                ClientTipInfo(channel, "Wrong username");
                channel.Send(response);
            }
           
            DEntity dentity = World.Instance.EntityData["Ellen"];
            player.FromDEntity(dentity);
            player.forClone = false;

            reader.Close();
        }
    }
}
