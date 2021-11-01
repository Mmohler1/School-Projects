using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milestone.Models
{
    public class GameInfo
    {
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public int SaveID { get; set; }
        public string Board { get; set; }
        public int Clicks { get; set; }
        public TimeSpan ScoreTime { get; set; }
        public bool Win { get; set; }    
        public DateTime CompletedOn { get; set; }

        //Constructor for saving the games state
        public GameInfo(int playerID, string username,string board, int clicks, TimeSpan scoreTime, bool win)
        {
            PlayerID = playerID;
            Username = username;
            Board = board;
            Clicks = clicks;
            ScoreTime = scoreTime;
            Win = win;
        }

        //Constructor for Loading the score
        public GameInfo(int playerID, string username, int saveID, string board, int clicks, TimeSpan scoreTime, bool win, DateTime completedOn)
        {
            PlayerID = playerID;
            Username = username;
            SaveID = saveID;
            Board = board;
            Clicks = clicks;
            ScoreTime = scoreTime;
            Win = win;
            CompletedOn = completedOn;
        }

        public GameInfo()
        {
        }
    }
}
