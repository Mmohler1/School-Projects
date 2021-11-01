using Milestone.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Milestone.Service.Data
{
    public class SecurityDAO
    {
        //For connecting to the database
        public string dbPlayerConn { get; set; }

        //Process register for user
        public bool ProcessRegister(PlayerInfo player)
        {
            this.dbPlayerConn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbPlayer;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False";

            //Stays false until connection goes through and is closed
            bool isSuccessful = false;
            //Adds Player to database, should be easy to move this to a database layer later
            using (SqlConnection conn = new SqlConnection(dbPlayerConn))
            {
                //Calling stored procedure to add player to database
                using (SqlCommand cmd = new SqlCommand("usp_InsertPlayer", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Using the stored procedure
                    cmd.Parameters.AddWithValue("@Username", player.Username);
                    cmd.Parameters.AddWithValue("@Password", player.Password);
                    cmd.Parameters.AddWithValue("@Email", player.Email);
                    cmd.Parameters.AddWithValue("@FirstName", player.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", player.LastName);
                    cmd.Parameters.AddWithValue("@Sex", player.Sex);
                    cmd.Parameters.AddWithValue("@Age", player.Age);
                    cmd.Parameters.AddWithValue("@State", player.State);

                    conn.Open();

                    //Executing the query
                    cmd.ExecuteNonQuery();
                    //since Query went through set bool to true. 
                    isSuccessful = true;
                }
                conn.Close();


            }
            return isSuccessful;
        }


        //Process login for user
        public bool ProcessLogin(string username, string password)
        {
            this.dbPlayerConn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbPlayer;" +
                "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
                "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            bool isSuccessful = false;
            //Adds Player to database, should be easy to move this to a database layer later
            using (SqlConnection conn = new SqlConnection(dbPlayerConn))
            {
                //Calling stored procedure
                using (SqlCommand cmd = new SqlCommand("usp_GetPlayerLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    try
                    {

                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        conn.Open();
                        using SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows == true)
                        {
                            isSuccessful = true;
                        }




                    }

                    catch (Exception e)
                    { Console.WriteLine(e); }

                }
                conn.Close();


            }
            return isSuccessful;
        }

        //Returns false if a Username or Email exists in the database
        public bool checkUsernameEmail(PlayerInfo player)
        {
            this.dbPlayerConn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbPlayer;" +
                "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
                "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            bool isSuccessful = true;
            //Adds Player to database, should be easy to move this to a database layer later
            using (SqlConnection conn = new SqlConnection(dbPlayerConn))
            {
                //Calling stored procedure
                using (SqlCommand cmd = new SqlCommand("usp_GetUsernameEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    try
                    {

                        cmd.Parameters.AddWithValue("@Username", player.Username);
                        cmd.Parameters.AddWithValue("@Email", player.Email);
                        conn.Open();
                        using SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows == true)
                        {

                            isSuccessful = false;
                        }




                    }

                    catch (Exception e)
                    { Console.WriteLine(e); }

                }
                conn.Close();


            }
            return isSuccessful;
        }

        //Returns id of player with same username and password
        public int FindPlayerDAO(string username, string password)
        {
            //Create instance for save data to be used
            int PlayerID = 0;

            this.dbPlayerConn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbPlayer;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False";

            //Adds Player to database, should be easy to move this to a database layer later
            using (SqlConnection conn = new SqlConnection(dbPlayerConn))
            {

                //Calling stored procedure to add player to database
                using (SqlCommand cmd = new SqlCommand("usp_GetPlayerByUsernamePassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Using the stored procedure
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);


                    conn.Open();

                    //Executing the query
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PlayerID = (int)reader[0];
                    }




                }
                conn.Close();


            }
            return PlayerID;
        }
    }
}

