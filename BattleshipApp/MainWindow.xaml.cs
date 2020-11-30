using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BattleshipEngine;

namespace BattleshipApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Shoot.Visibility = Visibility.Hidden;
            Invalid_Ship.Visibility = Visibility.Hidden;
            Invalid_Coord.Visibility = Visibility.Hidden;
            Player_Win.Visibility = Visibility.Hidden;
            Computer_Win.Visibility = Visibility.Hidden;
            ApplicationContext.CompetitorGrid.PopulateBoardRandom();
            ApplicationContext.PlayerBoard = new Image[10][];
            Image[] a = { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10 };
            ApplicationContext.PlayerBoard[0] = a;
            Image[] b = { b1, b2, b3, b4, b5, b6, b7, b8, b9, b10 };
            ApplicationContext.PlayerBoard[1] = b;
            Image[] c = { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10 };
            ApplicationContext.PlayerBoard[2] = c;
            Image[] d = { d1, d2, d3, d4, d5, d6, d7, d8, d9, d10 };
            ApplicationContext.PlayerBoard[3] = d;
            Image[] e = { e1, e2, e3, e4, e5, e6, e7, e8, e9, e10 };
            ApplicationContext.PlayerBoard[4] = e;
            Image[] f = { f1, f2, f3, f4, f5, f6, f7, f8, f9, f10 };
            ApplicationContext.PlayerBoard[5] = f;
            Image[] g = { g1, g2, g3, g4, g5, g6, g7, g8, g9, g10 };
            ApplicationContext.PlayerBoard[6] = g;
            Image[] h = { h1, h2, h3, h4, h5, h6, h7, h8, h9, h10 };
            ApplicationContext.PlayerBoard[7] = h;
            Image[] i = { i1, i2, i3, i4, i5, i6, i7, i8, i9, i10 };
            ApplicationContext.PlayerBoard[8] = i;
            Image[] j = { j1, j2, j3, j4, j5, j6, j7, j8, j9, j10 };
            ApplicationContext.PlayerBoard[9] = j;

            ApplicationContext.CompetitorBoard = new Image[10][];
            Image[] a_c = { a1_c, a2_c, a3_c, a4_c, a5_c, a6_c, a7_c, a8_c, a9_c, a1_c };
            ApplicationContext.CompetitorBoard[0] = a_c;
            Image[] b_c = { b1_c, b2_c, b3_c, b4_c, b5_c, b6_c, b7_c, b8_c, b9_c, b10_c };
            ApplicationContext.CompetitorBoard[1] = b_c;
            Image[] c_c = { c1_c, c2_c, c3_c, c4_c, c5_c, c6_c, c7_c, c8_c, c9_c, c10_c };
            ApplicationContext.CompetitorBoard[2] = c_c;
            Image[] d_c = { d1_c, d2_c, d3_c, d4_c, d5_c, d6_c, d7_c, d8_c, d9_c, d10_c };
            ApplicationContext.CompetitorBoard[3] = d_c;
            Image[] e_c = { e1_c, e2_c, e3_c, e4_c, e5_c, e6_c, e7_c, e8_c, e9_c, e10_c };
            ApplicationContext.CompetitorBoard[4] = e_c;
            Image[] f_c = { f1_c, f2_c, f3_c, f4_c, f5_c, f6_c, f7_c, f8_c, f9_c, f10_c };
            ApplicationContext.CompetitorBoard[5] = f_c;
            Image[] g_c = { g1_c, g2_c, g3_c, g4_c, g5_c, g6_c, g7_c, g8_c, g9_c, g10_c };
            ApplicationContext.CompetitorBoard[6] = g_c;
            Image[] h_c = { h1_c, h2_c, h3_c, h4_c, h5_c, h6_c, h7_c, h8_c, h9_c, h10_c };
            ApplicationContext.CompetitorBoard[7] = h_c;
            Image[] i_c = { i1_c, i2_c, i3_c, i4_c, i5_c, i6_c, i7_c, i8_c, i9_c, i10_c };
            ApplicationContext.CompetitorBoard[8] = i_c;
            Image[] j_c = { j1_c, j2_c, j3_c, j4_c, j5_c, j6_c, j7_c, j8_c, j9_c, j10_c };
            ApplicationContext.CompetitorBoard[9] = j_c;
            ApplicationContext.CoordMapping = new Dictionary<string, int>
            {
                { "A", 0 },
                { "B", 1 },
                { "C", 2 },
                { "D", 3 },
                { "E", 4 },
                { "F", 5 },
                { "G", 6 },
                { "H", 7 },
                { "I", 8 },
                { "J", 9 }
            };
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Place_Ship_Click(object sender, RoutedEventArgs e)
        {
            Invalid_Ship.Visibility = Visibility.Hidden;
            int[] ShipCounters = new int[4];
            int FirstCoordinate = -1;
            int SecondCoordinate = -1;
            foreach (Ship ship in ApplicationContext.PlayerGrid.Ships)
            {
                ShipCounters[ship.Size-1] += 1;
            }
            try
            {
                FirstCoordinate = ApplicationContext.CoordMapping[FirstCoord.Text];
                SecondCoordinate = Convert.ToInt32(SecondCoord.Text) - 1;
            } 
            catch
            {
                Invalid_Ship.Visibility = Visibility.Visible;
                return;
            }
            finally
            {

            }
            Field[] fields = new Field[Convert.ToInt32(Ship_Size.Text)];

            if (Ship_Direction.Text == "H" && (SecondCoordinate + Convert.ToInt32(Ship_Size.Text) > 10))
            {
                Invalid_Ship.Visibility = Visibility.Visible;
            }
            else if (Ship_Direction.Text == "V" && (FirstCoordinate + Convert.ToInt32(Ship_Size.Text) > 10))
            {
                Invalid_Ship.Visibility = Visibility.Visible;
            }
            else
            {
                for (int i = 0; i < Convert.ToInt32(Ship_Size.Text); i++)
                {
                    if (Ship_Direction.Text == "H")
                        fields[i] = ApplicationContext.PlayerGrid.Grid[FirstCoordinate][SecondCoordinate + i];
                    else
                        fields[i] = ApplicationContext.PlayerGrid.Grid[FirstCoordinate + i][SecondCoordinate];
                }
                if (!ApplicationContext.PlayerGrid.ValidateShipPlacement(fields))
                {
                    Invalid_Ship.Visibility = Visibility.Visible;
                }
                else if ((Convert.ToInt32(Ship_Size.Text) == 4 & ShipCounters[3] == 1) || (Convert.ToInt32(Ship_Size.Text) == 3 & ShipCounters[2] == 2) || (Convert.ToInt32(Ship_Size.Text) == 2 & ShipCounters[1] == 3) || (Convert.ToInt32(Ship_Size.Text) == 1 & ShipCounters[0] == 4))
                {
                    Invalid_Ship.Visibility = Visibility.Visible;
                }
                else
                {
                    Ship s = new Ship(Convert.ToInt32(Ship_Size.Text));
                    for (int i = 0; i < Convert.ToInt32(Ship_Size.Text); i++)
                    {
                        if (Ship_Direction.Text == "H")
                        {
                            ApplicationContext.PlayerBoard[FirstCoordinate][SecondCoordinate + i].Source = new BitmapImage(new Uri("ship_icon.jpg", UriKind.Relative));
                            ApplicationContext.PlayerGrid.Grid[FirstCoordinate][SecondCoordinate + i].HasShip = true;
                        }
                        else
                        {
                            ApplicationContext.PlayerBoard[FirstCoordinate + i][SecondCoordinate].Source = new BitmapImage(new Uri("ship_icon.jpg", UriKind.Relative));
                            ApplicationContext.PlayerGrid.Grid[FirstCoordinate + i][SecondCoordinate].HasShip = true;
                        }
                    }
                    s.Fields = fields;
                    ApplicationContext.PlayerGrid.PlaceShip(s, fields);
                }
            }
        }

        private void Check_Wins()
        {
            if (ApplicationContext.CompetitorGrid.CheckForWin())
            {
                Player_Win.Visibility = Visibility.Visible;
            } 
            else if (ApplicationContext.PlayerGrid.CheckForWin())
            {
                Computer_Win.Visibility = Visibility.Visible;
            }
        }

        private void Shoot_Click(object sender, RoutedEventArgs e)
        {
            Invalid_Coord.Visibility = Visibility.Hidden;
            if (FirstCoord.Text == "" | SecondCoord.Text == "")
            {
                Invalid_Coord.Visibility = Visibility.Visible;
            }
            else
            {
                int FirstCoordinate = ApplicationContext.CoordMapping[FirstCoord.Text];
                int SecondCoordinate = Convert.ToInt32(SecondCoord.Text) - 1;
                ApplicationContext.CompetitorGrid.Grid[FirstCoordinate][SecondCoordinate].Visit();
                Update_Competitor_View();
                Field VisitedField = ApplicationContext.cp.MakeRandomMove();
                if (VisitedField.HasShip)
                    if (VisitedField.WhichShip.Sunk)
                        foreach (Field f in VisitedField.WhichShip.Fields)
                            ApplicationContext.PlayerGrid.VisitNeighboringFields(f);
                Update_Player_View();
                Check_Wins();
            }

        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Update_Competitor_View()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (ApplicationContext.CompetitorGrid.Grid[i][j].Visited)
                    {
                        ApplicationContext.CompetitorBoard[i][j].Source = new BitmapImage(new Uri("dot.png", UriKind.Relative));
                        if (ApplicationContext.CompetitorGrid.Grid[i][j].HasShip)
                        {
                            if (ApplicationContext.CompetitorGrid.Grid[i][j].WhichShip.Sunk)
                                foreach (Field f in ApplicationContext.CompetitorGrid.Grid[i][j].WhichShip.Fields)
                                    ApplicationContext.CompetitorBoard[i][j].Source = new BitmapImage(new Uri("red_x.jpg", UriKind.Relative));
                            else
                                foreach (Field f in ApplicationContext.CompetitorGrid.Grid[i][j].WhichShip.Fields)
                                    ApplicationContext.CompetitorBoard[i][j].Source = new BitmapImage(new Uri("black_x.jpg", UriKind.Relative));
                        }
                    }
        }

        private void Update_Player_View()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (ApplicationContext.PlayerGrid.Grid[i][j].Visited)
                    {
                        ApplicationContext.PlayerBoard[i][j].Source = new BitmapImage(new Uri("dot.png", UriKind.Relative));
                        if (ApplicationContext.PlayerGrid.Grid[i][j].HasShip)
                        {
                            if (ApplicationContext.PlayerGrid.Grid[i][j].WhichShip.Sunk)
                                foreach (Field f in ApplicationContext.PlayerGrid.Grid[i][j].WhichShip.Fields)
                                    ApplicationContext.PlayerBoard[i][j].Source = new BitmapImage(new Uri("red_x.jpg", UriKind.Relative));
                            else
                                foreach (Field f in ApplicationContext.PlayerGrid.Grid[i][j].WhichShip.Fields)
                                    ApplicationContext.PlayerBoard[i][j].Source = new BitmapImage(new Uri("black_x.jpg", UriKind.Relative));
                        }
                    }
                    else
                    {
                        if (ApplicationContext.PlayerGrid.Grid[i][j].HasShip)
                            ApplicationContext.PlayerBoard[i][j].Source = new BitmapImage(new Uri("ship_icon.jpg", UriKind.Relative));
                    }
        }

        private void Start_Game_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationContext.PlayerGrid.CheckShips())
            {
                Start_Game.Visibility = Visibility.Hidden;
                Shoot.Visibility = Visibility.Visible;
            } 
            else
            {
                ApplicationContext.PlayerGrid.PopulateBoardRandom();
                Update_Player_View();
                Start_Game.Visibility = Visibility.Hidden;
                Shoot.Visibility = Visibility.Visible;
            }
            Ship_Direction.Visibility = Visibility.Hidden;
            Ship_Size.Visibility = Visibility.Hidden;
            Place_Ship.Visibility = Visibility.Hidden;
            Dir_Text.Visibility = Visibility.Hidden;
            Size_Text.Visibility = Visibility.Hidden;
            Field VisitedField = ApplicationContext.cp.MakeRandomMove();
            if (VisitedField.HasShip)
                if (VisitedField.WhichShip.Sunk)
                    foreach (Field f in VisitedField.WhichShip.Fields)
                        ApplicationContext.PlayerGrid.VisitNeighboringFields(f);
            Update_Player_View();
        }
    }
}
