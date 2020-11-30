using System;
namespace BattleshipEngine
{
    public class Field
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool Visited { get; set; }
        public bool HasShip { get; set; }
        public Ship WhichShip { get; set; }

        public Field() { }

        public Field(int xCoord, int yCoord)
        {
            X = xCoord;
            Y = yCoord;
            Visited = false;
            HasShip = false;
            WhichShip = null;
        }

        public void Visit()
        {
            Visited = true;
            if (WhichShip != null)
                WhichShip.Hit();
        }
    }
}
