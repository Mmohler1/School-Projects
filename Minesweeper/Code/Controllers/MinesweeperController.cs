using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Service.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Milestone.Controllers
{



    public class MinesweeperController : Controller
    {
        FieldService board = new FieldService();

        bool win = false;
        
        static int clicks = 0;
        static Stopwatch Timer = new Stopwatch();
        static TimeSpan extra; //Extra time for after loading.
        static int playerId; //For saving and loading the game and scores.

        static List<Space> buttons = new List<Space>(); //The Board

        
        private readonly ILogger _logger; //Logger for putting info on console

        public MinesweeperController(ILogger<MinesweeperController> logger)
        {
            this._logger = logger; //Sets logger
        }

        public IActionResult Index()
        {
            //Create Instance of Game Service
            var GameServ = new GameService();

            playerId = (int)HttpContext.Session.GetInt32("playerId"); //Uses Session Variables to get PlayerId
            _logger.LogInformation("Minesweeper: Player id set to {Id}", playerId);

            NewBoard();
            Timer.Start(); //Starts timer


                //If there is nothing in the button list then make the field and everything in it
                if (buttons.Count < 2)
                {
                    for (int X = 0; X < 10; X++)
                    {
                        for (int Y = 0; Y < 10; Y++)
                        {
                            buttons.Add(new Space(X, Y));
                        }
                    }

                    board.MakeMines(buttons);
                    board.mineCheck(buttons);

                _logger.LogInformation("Minesweeper: The Board, Mines, and MineCheck have gone through");
                }

                return View("Index", buttons);

        }

        //To be used with ajax and javascript to refreash the page without reloading it. 
        public IActionResult _ViewMinesweeperPartial(string squareNumber)
        {
            
            int bttnInt = int.Parse(squareNumber);
            _logger.LogInformation("Minesweeper: Button Number {bttn} Has Been Selected", bttnInt);

            //If a mine is there show all the spaces and print lose text.
            if (buttons.ElementAt(bttnInt).CurrentlyOccupied == true)
            {
                _logger.LogInformation("Minesweeper: The Button had a bomb");
                Timer.Stop(); //Stops Timer
                
                buttons.ElementAt(bttnInt).visited = true;
                for (int i = 0; i < 100; i++)
                {
                    buttons.ElementAt(i).visited = true;
                }

                //Updates viewbags to tell the player what's going on. 
                @ViewBag.Clicks = "Clicks: " + clicks;
                @ViewBag.theResult = board.sarcasticComment(9);
                @ViewBag.Win = "You Lose!";
                return PartialView(buttons);
            }
            //If a bomb is not clicked, set the button to visited and run through the functions within the FieldService
            else
            {
                _logger.LogInformation("Minesweeper: Button did not have a bomb.");
                //Updates viewbag to tell the player what's going on. 
                clicks++;
                @ViewBag.Clicks = "Clicks: " + clicks;
                @ViewBag.theResult = board.sarcasticComment(buttons.ElementAt(bttnInt).BombNear);

                board.floodFill(buttons.ElementAt(bttnInt).row, buttons.ElementAt(bttnInt).col, buttons);
                buttons.ElementAt(bttnInt).visited = true;

                

                //Iterate through the whole list to determine if a win condition has been met
                for (int i = 0; i < 100; i++)
                {
                    win = false;
                    //If all spaces have been either visted or occupied by a bomb, you have won
                    if (buttons.ElementAt(i).visited == true || buttons.ElementAt(i).CurrentlyOccupied == true)
                    {   
                        win = true;
                    }
                    //exception handling so that the last button being visited doesn't produce a win con
                    else
                    {
                        break;
                    }
                }
                _logger.LogInformation("Minesweeper: Win has been set to {cond}", win);
            }

            if (win == true)
            {
                Timer.Stop(); //Stops Timer

                //Updates viewbag to tell the player what's going on. 
                @ViewBag.theResult = "";
                @ViewBag.Clicks = "Clicks: " + clicks;
                @ViewBag.Win = "You Win!";


                SaveButton(true);
                return PartialView(buttons);
            }
            else
            {
                
                return PartialView(buttons);
            }
        }



        //Adds or removes square's image to flag with javascript
        public IActionResult RightClickFlag(string squareNumber)
        {
            //Keeps clicks updated
            @ViewBag.Clicks = "Clicks: " + clicks;

            int bttnInt = int.Parse(squareNumber);
            
            //Checks if the space has already been selected. If not don't put flag, if so then continue.
            if (buttons.ElementAt(bttnInt).visited == false)
            {
                //If a flag isn't there place when, if it is then don't place one.
                if (buttons.ElementAt(bttnInt).flag == false)
                {
                    _logger.LogInformation("Minesweeper: Flag has been placed at button {bttn}", bttnInt);
                    buttons.ElementAt(bttnInt).flag = true;
                    return PartialView(buttons);
                }
                else
                {
                    _logger.LogInformation("Minesweeper: Flag has been removed from button {bttn}", bttnInt);
                    buttons.ElementAt(bttnInt).flag = false;
                    return PartialView(buttons);
                }

            }
            else
            {
                _logger.LogInformation("Minesweeper: Flag could not toggled at button {bttn}", bttnInt);
                return PartialView(buttons);
            }

        }
        

        


        //Saves game to Database
        public IActionResult SaveButton(bool hasWon)
        {

            if (hasWon)
            { 
                _logger.LogInformation("Minesweeper: Game is being stored as a score");
            }
            else
            {
                _logger.LogInformation("Minesweeper: Game is being stored as a save file");
            }

            //Create Save info 
            GameInfo SaveData = new GameInfo();
            SaveData.PlayerID = playerId;
            SaveData.Board = SaveBoard(buttons);
            SaveData.Clicks = clicks;
            SaveData.ScoreTime = Timer.Elapsed + extra;
            SaveData.Win = hasWon;

            //Create Instance of Game Service
            var GameServ = new GameService();
            GameServ.SaveGameService(SaveData);


            //Return to Home page after game is saved.
            return RedirectToAction("Index", "Home");

        }



        //Loads game from Database and set the proper variables
        public IActionResult LoadButton()
        {
            _logger.LogInformation("Minesweeper: Saved game is being loaded.");

            //Create Instance of Game Service
            var GameServ = new GameService();
            
            //Set the Old Board, Clicks and Time
            GameInfo OldInfo = GameServ.LoadGameService(playerId);
            LoadBoard(OldInfo.Board); //Makes old board the current board.
            clicks = OldInfo.Clicks;
            extra = OldInfo.ScoreTime;


            board.mineCheck(buttons);
            Timer.Start(); //Starts timer

            //Deletes Save file now that it has been loaded.
            GameServ.deleteSaveService(playerId, false);
            _logger.LogInformation("Minesweeper: Game Was loaded from save.");
            return View("Index", buttons);

        }



        //Saves board to an encoded string.
        public string SaveBoard(List<Space> squares)
        {
            _logger.LogInformation("Minesweeper: Board is being saved to String.");
            string codedLine = "";

            //For loop that saves the string
            for (int x = 0; x < 100; x++)
            {


                if (squares.ElementAt(x).CurrentlyOccupied == true)
                {
                    if (squares.ElementAt(x).flag == true)
                        codedLine += "4";   //Space is bomb with flag
                    else
                        codedLine += "3";   //Space is a bomb
                }
                else if (squares.ElementAt(x).visited == false)
                {
                    if(squares.ElementAt(x).flag == true)
                    codedLine += "2";   //space isn't visted with flag
                    else
                    codedLine += "1";   //space isn't visted
                }
                else
                {
                    codedLine += "0"; //Space has been visited
                }
            }
            _logger.LogInformation("Minesweeper: Board has been saved as string {coded}", codedLine);
            return codedLine;

        }


        //Load board from encoded string.
        public void LoadBoard(string encodedBoard)
        {

            _logger.LogInformation("Minesweeper: Board is being loaded from string.");

            //array of characters to read the string
            char[] splitCode = encodedBoard.ToCharArray();

            //Make new board
            buttons = new List<Space>();


            //id for list and char array
            int total = 0;

            //For loop that initalizes and loads string into board
            for (int X = 0; X < 10; X++)
            {
                for (int Y = 0; Y < 10; Y++)
                {
                    //Make new space
                    buttons.Add(new Space(X, Y));

                    //Turn that space into what the decoded string says
                    if (splitCode[total] == '0') //Vistited
                    {
                        buttons.ElementAt(total).visited = true;
                    }
                    else if (splitCode[total] == '2') //UnVistited + Flag
                    {
                        buttons.ElementAt(total).flag = true;
                    }
                    else if (splitCode[total] == '3') //Bomb
                    {
                        buttons.ElementAt(total).CurrentlyOccupied = true;
                    }
                    else if (splitCode[total] == '4') //Bomb + flag
                    {
                        buttons.ElementAt(total).flag = true;
                        buttons.ElementAt(total).CurrentlyOccupied = true;
                    }
                    else
                    { //It'll be set to the equivalent to 1, UnVistited
                    }

                    total += 1;
                }
            }



        }

        //Creates new board
        public void NewBoard()
        {
            buttons = new List<Space>();
            win = false;

            clicks = 0;
            Stopwatch Timer = new Stopwatch();
            extra = new TimeSpan(0);
            

            

        }


    }

}
