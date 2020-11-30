using System;
using System.Collections.Generic;

namespace BattleshipEngine
{
    public class Board
    {
        public Field[][] Grid { get; private set; }
        public HashSet<Ship> Ships { get; private set; }
        public HashSet<Field> InvalidFields { get; private set; }

        public Board()
        {
            InvalidFields = new HashSet<Field>();
            Ships = new HashSet<Ship>();
            Grid = new Field[10][];
            for (int i = 0; i < 10; i++)
            {
                Grid[i] = new Field[10];
                for (int j = 0; j < 10; j++)
                {
                    Grid[i][j] = new Field(i, j);
                }
            }
        }

        public bool ValidateShipPlacement(Field[] fields)
        {
            bool Valid = true;
            foreach (Field f in fields)
                if (InvalidFields.Contains(f))
                    Valid = false;
            return Valid;

        }

        private void AddInvalidFields(Ship NewShip) {
            foreach (Field f in NewShip.Fields)
            {
                InvalidFields.Add(f);
                if (f.X != 0)
                {
                    InvalidFields.Add(Grid[f.X - 1][f.Y]);
                    if (f.Y != 0)
                        InvalidFields.Add(Grid[f.X - 1][f.Y - 1]);
                    if (f.Y != 9)
                        InvalidFields.Add(Grid[f.X - 1][f.Y + 1]);
                }
                if (f.X != 9)
                {
                    InvalidFields.Add(Grid[f.X + 1][f.Y]);
                    if (f.Y != 0)
                        InvalidFields.Add(Grid[f.X + 1][f.Y - 1]);
                    if (f.Y != 9)
                        InvalidFields.Add(Grid[f.X + 1][f.Y + 1]);
                }
                if (f.Y != 0)
                    InvalidFields.Add(Grid[f.X][f.Y - 1]);
                if (f.Y != 9)
                    InvalidFields.Add(Grid[f.X][f.Y + 1]);
            }
        }

        //check if all ships were placed - useful for player's grid
        public bool CheckShips()
        {
            bool Valid = true;
            int[] ShipCounters = new int[4];
            foreach (Ship ship in Ships)
                ShipCounters[ship.Size - 1] += 1;
            if (ShipCounters[3] != 1 || ShipCounters[2] != 2 || ShipCounters[1] != 3 || ShipCounters[0] != 4)
                Valid = false;
            return Valid;
        }

        public void PlaceShip(Ship s, Field[] f) 
        {
            if (!ValidateShipPlacement(f))
            {
                //this should be an exception
                Console.WriteLine("Can't add ship. Invalid.");
                return;
            }
            foreach (Field field in f)
            {
                field.HasShip = true;
                field.WhichShip = s;
            }
            s.PlaceShip(f);
            Ships.Add(s);
            AddInvalidFields(s);
        }

        public void VisitNeighboringFields(Field f)
        {
            if (f.X != 0)
            {
                Grid[f.X - 1][f.Y].Visit();
                if (f.Y != 0)
                    Grid[f.X - 1][f.Y - 1].Visit();
                if (f.Y != 9)
                    Grid[f.X - 1][f.Y + 1].Visit();
            }
            if (f.X != 9)
            {
                Grid[f.X + 1][f.Y].Visit();
                if (f.Y != 0)
                    Grid[f.X + 1][f.Y - 1].Visit();
                if (f.Y != 9)
                    Grid[f.X + 1][f.Y + 1].Visit();
            }
            if (f.Y != 0)
                Grid[f.X][f.Y - 1].Visit();
            if (f.Y != 9)
                Grid[f.X][f.Y + 1].Visit();
        }

        
        public void PopulateBoardRandom()
        {
            int PlacedShips = 0;
            Ship NewShip;
            while (PlacedShips < 10)
            {
                if (PlacedShips == 0)
                    NewShip = new Ship(4);
                else if (PlacedShips < 3)
                    NewShip = new Ship(3);
                else if (PlacedShips < 6)
                    NewShip = new Ship(2);
                else
                    NewShip = new Ship(1);

                Random rnd = new Random();
               
                List<Field> FieldsChosen = new List<Field>();
                bool ready = false;
                do
                {
                    FieldsChosen = new List<Field>();

                    bool Horizontal;
                    // direction: 0 means horizontal
                    int HorizontalOrVertical = rnd.Next(2);
                    if (HorizontalOrVertical == 0)
                        Horizontal = true;
                    else
                        Horizontal = false;

                    int RandomX = rnd.Next(10);
                    int RandomY = rnd.Next(10);
                    Field RandomField = Grid[RandomX][RandomY];
                    while (InvalidFields.Contains(RandomField))
                    {
                        RandomX = rnd.Next(10);
                        RandomY = rnd.Next(10);
                        RandomField = Grid[RandomX][RandomY];
                    }
                    int rows = 0;
                    int cols = 0;
                    if (Horizontal == true)
                    {
                        rows = RandomField.X + NewShip.Size;
                    }
                    else
                    {
                        cols = RandomField.Y + NewShip.Size;
                    }

                    if (rows > 10 || cols > 10)
                    {
                        ready = false;
                        continue;
                    }

                    FieldsChosen.Add(RandomField);
                    for (int i = 1; i < NewShip.Size; i++)
                    {
                        if (Horizontal)
                            RandomField = Grid[RandomX + i][RandomY];
                        else
                            RandomField = Grid[RandomX][RandomY + i];
                        if (!InvalidFields.Contains(RandomField))
                            FieldsChosen.Add(RandomField);
                    }
                    if (FieldsChosen.Count == NewShip.Size)
                        ready = true;
                } while (ready == false);

                PlaceShip(NewShip, FieldsChosen.ToArray());

                System.Diagnostics.Debug.WriteLine("Placing ship...");
                for (int i = 0; i < NewShip.Size; i++)
                {
                    System.Diagnostics.Debug.WriteLine(NewShip.Fields[i].X);
                    System.Diagnostics.Debug.WriteLine(NewShip.Fields[i].Y);
                }
                PlacedShips++;
            }
        }

        public bool Shoot(int x, int y) //return true if shoot was successful
        {
            if (Grid[x][y].Visited == false)
            {
                Grid[x][y].Visit();
                foreach (Ship s in Ships)
                {
                    foreach (Field f in s.Fields)
                    {
                        if (f == Grid[x][y])
                        {
                            s.Hit();
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public bool CheckForWin()
        {
            int counter = 0;
            foreach (Ship s in Ships)
            {
                if (s.Sunk == true)
                    counter++;
            }
            if (counter == 10)
                return true;
            else
                return false;
        }
    }
}

