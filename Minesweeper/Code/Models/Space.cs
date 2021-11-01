using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Class from previous CST Class
namespace Milestone.Models
{
    public class Space
    {
        public int row { get; set; }
        public int col { get; set; }

        // this means that there is a mine at this location
        public bool CurrentlyOccupied { get; set; }
        public int BombNear { get; set; }
        public bool visited { get; set; }
        public bool flag { get; set; } //added 3/13

        public Space(int x, int y)
        {
            row = x;
            col = y;
            CurrentlyOccupied = false;
            BombNear = 0;
            visited = false;
            flag = false;  //added 3/13
        }
    }
}
