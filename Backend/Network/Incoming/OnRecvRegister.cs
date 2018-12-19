using Common;
using Npgsql;
using Backend.Game;
using System;
using System.IO;

namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvRegister(IChannel channel, Message message)
        {
            /*
             *  write to database
             */

            CRegister request = message as CRegister;
            string name = request.user;
            string passwd = request.password;

            // tip
            Console.WriteLine("Hello PostgreSQL");

            // postgresql configure 
            //219.228.148.232
            //123456
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=postgres;";

            using (var conn = new NpgsqlConnection(connString))
            {

                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO USERS VALUES (@p,@q,0,0,0,0,0,0)";
                    cmd.Parameters.AddWithValue("p", name);
                    cmd.Parameters.AddWithValue("q", passwd);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        ClientTipInfo(channel, "Congratulations! Register successfully!");
                    }
                    catch
                    {
                        ClientTipInfo(channel, "Sorry, the name is used by others!");

                    }
                }
            }




            // for test 
            /*
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand("SELECT * FROM USERS", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine(reader.GetString(0));
            }

    */
        }

    }
}