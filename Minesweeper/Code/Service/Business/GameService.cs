using Milestone.Models;
using Milestone.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milestone.Service.Business
{
    public class GameService
    {
        //Returns bool based on if game was saved
        public bool SaveGameService(GameInfo score)
        {

            var GameAO = new GameDAO();


                return GameAO.SaveGameDAO(score);
        }

        //Returns GameInfo to be used to load the previous game.
        public GameInfo LoadGameService(int PlayerID)
        {

            var GameAO = new GameDAO();


            return GameAO.LoadGameDAO(PlayerID);
        }

        //List of scores 
        public List<GameInfo> ListScoresService()
        {
            var GameAO = new GameDAO();


            return GameAO.ListScoresDAO();
        }

        //Returns bool based on if save was deleted
        public bool deleteSaveService(int PlayerID, bool hasWon)
        {

            var GameAO = new GameDAO();


            return GameAO.deleteSaveDAO(PlayerID, hasWon);
        }
    }
}
