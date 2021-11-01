using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Service.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace Milestone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // api/gameapi/
    public class GameAPIController : Controller
    {
        GameService GameServ = new GameService();

        //Instantiates Game Service class
        public GameAPIController()
        {
            GameServ = new GameService();
        }

        //Creates list of scores ordered like a high score board would
        [HttpGet]
        [ResponseType(typeof(List<GameInfo>))]
        public IEnumerable<GameInfo> Index()
        {
            //Get score
            List<GameInfo> scoreList = GameServ.ListScoresService();

            //use lingq statement for the foreach
            List<GameInfo> scoreAPIList = new List<GameInfo>();
            foreach (GameInfo g in scoreList)
            {
                scoreAPIList.Add(new GameInfo(g.PlayerID, g.Username, g.SaveID, g.Board, g.Clicks, g.ScoreTime, g.Win, g.CompletedOn));
            }

            //Returns list of scores
            return scoreList;
        }
    }
}
