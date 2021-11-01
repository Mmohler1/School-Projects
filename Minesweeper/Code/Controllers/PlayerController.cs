using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Milestone.Models;
using Milestone.Service.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Milestone.Controllers
{
    public class PlayerController : Controller
    {
        //Logger for putting info on console
        private readonly ILogger _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            //Sets logger
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Register Page
        public IActionResult Register()
        {
            return View("Register");
        }

        //Register User in Database using Player Info
        [HttpPost]
        public IActionResult Register(PlayerInfo player)
        {
            var securityS = new SecurityService();

            if (securityS.ProcessTheRegister(player) == true)
            {
                //Sends user back to index page
                _logger.LogInformation("Player: Registration Process Went Through");
                return View("Login");
            }
            //If false keep user on register page
            _logger.LogInformation("Player: Registration Process Failed");
            return View("Register");


        }

        //Login Page
        public IActionResult Login()
        {
            return View("Login");
        }

        //Login to the webstie and send the user to the Minesweeper page
        [HttpPost]
        public IActionResult doLogin()
        {
            var securityS = new SecurityService();

            string username = Request.Form["Username"].ToString();
            string password = Request.Form["Password"].ToString();

            
            ViewBag.test = "" + username + " AND " + password;
            if (securityS.ProcessTheLogin(username, password) == true)
            {
                int playerID = securityS.FindPlayerService(username, password);

                
                HttpContext.Session.SetInt32("playerId", playerID); //Uses sessions to save the Player Id
                

                _logger.LogInformation("Player: {Id} has Logged in!", playerID);
                //Sends user to Minesweeper page
                return RedirectToAction("Index", "Minesweeper");
            }

            //If false keep user on Login page
            _logger.LogInformation("Player: Login did not match!");
            return View("Login");


        }



    }  
}
