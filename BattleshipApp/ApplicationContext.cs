using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BattleshipEngine;

namespace BattleshipApp
{
    public class ApplicationContext
    {
        public static Image[][] PlayerBoard { get; set; }
        public static Image[][] CompetitorBoard { get; set; }
        public static Dictionary<string, int> CoordMapping { get; set; }
        public static Board PlayerGrid = new Board();
        public static Board CompetitorGrid = new Board();
        public static ComputerPlayer cp = new ComputerPlayer(PlayerGrid);
    }
}
