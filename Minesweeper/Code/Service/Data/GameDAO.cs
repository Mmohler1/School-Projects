using Milestone.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Milestone.Service.Data
{
    public class GameDAO
    {

        //For connecting to the database
        public string dbScoreConn { get; set; }

        //Save the game or score to database based on if the player has won.
        public bool SaveGameDAO(GameInfo score)
        {
            this.dbScoreConn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbPlayer;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False";

            //Stays false until connection goes through and is closed
            bool isSuccessful = false;
            //Adds Player to database, should be easy to move this to a database layer later
            using (SqlConnection conn = new SqlConnection(dbScoreConn))
            {
                //Calling stored procedure to add player to database
                using (SqlCommand cmd = new SqlCommand("usp_InsertScore", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Using the stored procedure
                    cmd.Parameters.AddWithValue("@Username", score.Username);
                    cmd.Parameters.AddWithValue("@PlayerID", score.PlayerID);
                    cmd.Parameters.AddWithValue("@Board", score.Board);
                    cmd.Parameters.AddWithValue("@Clicks", score.Clicks);
                    cmd.Parameters.AddWithValue("@ScoreTime", score.ScoreTime);
                    cmd.Parameters.AddWithValue("@Win", Convert.ToByte(score.Win));


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

        
        //Load the game from the Database
        public GameInfo LoadGameDAO(int PlayerID)
        {
            //Create instance for save data to be used
            GameInfo SaveData = null;

            this.dbScoreConn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbPlayer;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False";

            //Adds Player to database, should be easy to move this to a database layer later
            using (SqlConnection conn = new SqlConnection(dbScoreConn))
            {
                
                //Calling stored procedure to add player to database
                using (SqlCommand cmd = new SqlCommand("usp_GetScoreByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Using the stored procedure
                    cmd.Parameters.AddWithValue("@PlayerID", PlayerID);
                    cmd.Parameters.AddWithValue("@Win", Convert.ToByte(false));


                    conn.Open();

                    //Executing the query
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        SaveData = new GameInfo((int)reader[0], (string)reader[1], (int)reader[2], (string)reader[3],
                            (int)reader[4], (TimeSpan)reader[5], false, Convert.ToDateTime(reader[7]));
                    }

                   


                }
                conn.Close();


            }
            return SaveData;
        }

        //List of scores
        public List<GameInfo> ListScoresDAO()
        {
            //Create instance for save data to be used
            List<GameInfo> SaveData = new List<GameInfo>();

            this.dbScoreConn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbPlayer;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False";

            //Adds Player to database, should be easy to move this to a database layer later
            using (SqlConnection conn = new SqlConnection(dbScoreConn))
            {

                //Calling stored procedure to add player to database
                using (SqlCommand cmd = new SqlCommand("usp_GetAllScores", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;



                    conn.Open();

                    //Executing the query
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SaveData.Add(new GameInfo((int)reader[0], (string)reader[1], (int)reader[2], (string)reader[3],
                            (int)reader[4], (TimeSpan)reader[5], true, Convert.ToDateTime(reader[7])));
                    }




                }
                conn.Close();


            }
            return SaveData;
        }




        //Deletes save from the database.
        public bool deleteSaveDAO(int PlayerID, bool hasWon)
        {
            this.dbScoreConn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbPlayer;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False";

            //Stays false until connection goes through and is closed
            bool isSuccessful = false;
            //Adds Player to database, should be easy to move this to a database layer later
            using (SqlConnection conn = new SqlConnection(dbScoreConn))
            {
                //Calling stored procedure to add player to database
                using (SqlCommand cmd = new SqlCommand("usp_InsertScore", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Using the stored procedure
                    cmd.Parameters.AddWithValue("@PlayerID", PlayerID);
                    cmd.Parameters.AddWithValue("@Win", Convert.ToByte(hasWon));


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

    }
}
