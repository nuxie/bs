using System;
namespace BattleshipEngine
{

    public class Ship
    {
        public int Hits { get; set; }
        public int Size { get; set; }
        public bool Sunk { get; set; }
        public Field[] Fields { get; set; }

        public Ship(int size)
        {
            if (size < 1 | size > 4)
                throw new System.ArgumentException("Invalid ship size!");
            Size = size;
            Hits = 0;
            Sunk = false;
        }

        public void PlaceShip(Field[] fields)
        {
            Fields = fields;
        }

        public void Hit()
        {
            Hits++;
            if (Hits == Size)
            {
                Sunk = true;
            }
        }

    }
}
