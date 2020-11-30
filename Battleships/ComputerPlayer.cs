using BattleshipEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipEngine
{
    public class ComputerPlayer {
        public Board Board { get; private set; }

        public ComputerPlayer(Board b)
        {
            Board = b;
        }

        //true tells if the ship was sunken
        public Field MakeRandomMove()
        {
            Random rnd = new Random();
            int RandomX = rnd.Next(10);
            int RandomY = rnd.Next(10);
            Field RandomField = Board.Grid[RandomX][RandomY];
            while (RandomField.Visited)
            {
                RandomX = rnd.Next(10);
                RandomY = rnd.Next(10);
                RandomField = Board.Grid[RandomX][RandomY];
            }
            RandomField.Visit();
            return RandomField;     
        }
    }

}
