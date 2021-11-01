using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;

//Class from previous CST Class
namespace Milestone.Models
{
    public class FieldService
    {
        int Size = 10;
        
        // choose some places on the field to plant mines.
        public void MakeMines(List<Space> buttons)
        {
            // place some mines on the field at random locations.
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {              
                buttons.ElementAt(rand.Next(100)).CurrentlyOccupied = true;
            } 
        }


        // helper function to determine whether or not a space is actually in bounds.  valid or not.
        public bool IsSafe(int a, int b)
        {
            if (a < 0 || a >= Size )
            {
                return false;
            }
            else if (b < 0 || b >= Size)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //This function assigns values to each button based on how many bombs are near
        public List<Space> mineCheck(List<Space> buttons)
        {
            Space[,] theGrid = new Space[10, 10];
            int k = 0;
            
            //Assigns the list of buttons to the grid
            for (int X = 0; X < 10; X++)
            {
                for (int Y = 0; Y < 10; Y++)
                {
                    theGrid[X, Y] = buttons[k];
                    k++;
                }
            }

            //checking around each space
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Space currentSpace = new Space(i, j);
                    if (IsSafe(currentSpace.row + 1, currentSpace.col))
                    {
                        if (theGrid[currentSpace.row + 1, currentSpace.col].CurrentlyOccupied == true)
                        {
                            theGrid[currentSpace.row, currentSpace.col].BombNear++;
                        }
                    }
                    if (IsSafe(currentSpace.row - 1, currentSpace.col))
                    {
                        if (theGrid[currentSpace.row - 1, currentSpace.col].CurrentlyOccupied == true)
                        {
                            theGrid[currentSpace.row, currentSpace.col].BombNear++;
                        }
                    }
                    if (IsSafe(currentSpace.row + 1, currentSpace.col + 1))
                    {
                        if (theGrid[currentSpace.row + 1, currentSpace.col + 1].CurrentlyOccupied == true)
                        {
                            theGrid[currentSpace.row, currentSpace.col].BombNear++;
                        }
                    }
                    if (IsSafe(currentSpace.row - 1, currentSpace.col + 1))
                    {
                        if (theGrid[currentSpace.row - 1, currentSpace.col + 1].CurrentlyOccupied == true)
                        {
                            theGrid[currentSpace.row, currentSpace.col].BombNear++;
                        }
                    }
                    if (IsSafe(currentSpace.row, currentSpace.col + 1))
                    {
                        if (theGrid[currentSpace.row, currentSpace.col + 1].CurrentlyOccupied == true)
                        {
                            theGrid[currentSpace.row, currentSpace.col].BombNear++;
                        }
                    }
                    if (IsSafe(currentSpace.row, currentSpace.col - 1))
                    {
                        if (theGrid[currentSpace.row, currentSpace.col - 1].CurrentlyOccupied == true)
                        {
                            theGrid[currentSpace.row, currentSpace.col].BombNear++;
                        }
                    }
                    if (IsSafe(currentSpace.row + 1, currentSpace.col - 1))
                    {
                        if (theGrid[currentSpace.row + 1, currentSpace.col - 1].CurrentlyOccupied == true)
                        {
                            theGrid[currentSpace.row, currentSpace.col].BombNear++;
                        }
                    }
                    if (IsSafe(currentSpace.row - 1, currentSpace.col - 1))
                    {
                        if (theGrid[currentSpace.row - 1, currentSpace.col - 1].CurrentlyOccupied == true)
                        {
                            theGrid[currentSpace.row, currentSpace.col].BombNear++;
                        }
                    }
                }
            }

            //reassigning the grid array to the button list
            buttons = new List<Space>();
            k = 0;
            for (int X = 0; X < 10; X++)
            {
                for (int Y = 0; Y < 10; Y++)
                {
                    buttons.Add(theGrid[X,Y]);
                    k++;
                }
            }
            return buttons;
        }
        
        //Checks for zeroes near other zeroes
        public void floodFill(int r, int c, List<Space> buttons)
        {
            //Assigns the list of buttons to the grid
            Space[,] theGrid = new Space[10, 10];
            int k = 0;
            for (int X = 0; X < 10; X++)
            {
                for (int Y = 0; Y < 10; Y++)
                {
                    theGrid[X, Y] = buttons[k];
                    k++;
                }
            }


            if (IsSafe(r, c) && theGrid[r, c].BombNear.Equals(0) && theGrid[r, c].visited == false)
            {
                
                theGrid[r, c].visited = true;

                //reassigning the grid array to the button list
                buttons = new List<Space>();
                k = 0;
                for (int X = 0; X < 10; X++)
                {
                    for (int Y = 0; Y < 10; Y++)
                    {
                        buttons.Add(theGrid[X,Y]);
                        k++;
                    }
                }

                // apply the cell to the south (r + 1)
                floodFill(r + 1, c, buttons);
                // apply the cell to the north (r - 1)
                floodFill(r - 1, c, buttons);
                // apply the cell to the south (c + 1)
                floodFill(r, c + 1, buttons);
                // apply the cell to the south (c - 1)
                floodFill(r, c - 1, buttons);
                // apply the cell to the south (r + 1)
                floodFill(r + 1, c + 1, buttons);
                // apply the cell to the north (r - 1)
                floodFill(r - 1, c - 1, buttons);
                // apply the cell to the south (c + 1)
                floodFill(r - 1, c + 1, buttons);
                // apply the cell to the south (c - 1)
                floodFill(r + 1, c - 1, buttons);
            }
        }


        //Returns a string from an array of sarcastic comments
        public string sarcasticComment(int number)
        {
            //initalize array of strings to randomly print to the user with every button
            string[] sassy0 = { "I told you we wouldn’t go boom.", "I swear I just saw a white pointer in the sky.", "Think there’s a fast food place around here?", "I haven’t seen this many 0’s since senior year.", "Do you have games on your phone?", "Wanna play tag?", "Honestly I could go for a nap.", "Seems clear." };

            string[] sassy1 = { "The fact we’re not dead is a good sign.", "Only a loser would put a mine here.", "Watch your step!", "Can anyone disarm a mine?", "You go ahead, I’ll be fine here.", "Why do we have to cover the whole field again?", "These flags are heavy.", "Do you imagine we’re in a desert or a forest?", "Please God, help me through this!", "Don’t fail me now instincts.", "That job at McDonald’s doesn’t sound so bad about now.", "Did you hear that?", "I knew those telepathic prairie dogs gave us the wrong map!", "Are those vultures?", "Which Way?!", "Are soldiers who plant mines called minors?", "I’m filling out a crossword. What’s a four letter word for explosive?" };

            string[] sassy2 = { "How are we able to jump so far?", "Isn’t there an app for this?", "Which way does the Magic 8-Ball say to go next?", "Do you smell gunpowder?", "Didn’t you lose an arm here last time?", "This is nothing like COD.", "When I get back to the mall I’m going to tell that guy off." };

            string[] sassy3 = { "That means our points go up right?", "What sound does a firing mechanism make?", "I don’t want to die!", "Has your life flash before your eyes and realize it kinda sucked?", "Who wants to be a pencil pusher?! What was I thinking!", "Is someone in a dark robe staring at us?", "On second thought, you hold the sweeper.", "=(", "This seems like a place someone would put a trap.", "Please be a mine shaft! Please be a mine shaft!" };

            string[] sassyLose = { "That must hurt!", "You Lost...some weight", "Your arm must have gone 10 feet in the air!", "Almost Missed it!", "We’ll fit what we can of you in a box for your family.", "So bomb sniffing cats don’t work as well?", "Anyone have some rubbing alcohol?", "I’m sure they can sew that back on your body… if we could only find it…", "Medic!!!"};

            //Check how many bombs are near to decide on text used.
            if(number == 0)
            {
                Random rand = new Random();
                return sassy0[rand.Next(sassy0.Length)];
            }
            else if (number == 1)
            {
                Random rand = new Random();
                return sassy1[rand.Next(sassy1.Length)];
            }
            else if (number == 2)
            {
                Random rand = new Random();
                return sassy2[rand.Next(sassy2.Length)];
            }
            else if (number == 9) //If the user hit a bomb
            {
                Random rand = new Random();
                return sassyLose[rand.Next(sassyLose.Length)];
            }
            else //Anything else over 3
            {
                Random rand = new Random();
                return sassy3[rand.Next(sassy3.Length)];
            }


        }

    }
}
