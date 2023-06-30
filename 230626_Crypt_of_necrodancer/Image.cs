using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _230626_Crypt_of_necrodancer
{
    public class Image : GameInfo 
    {
        Draw draw = new Draw();

        public void TextBox()
        {
            Console.WriteLine("┌────────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                                                                    │");
            Console.WriteLine("│                                                                    │");
            Console.WriteLine("│                                                                    │");
            Console.WriteLine("│                                                                    │");
            Console.WriteLine("└────────────────────────────────────────────────────────────────────┘");
        }
        public void Box(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Yellow;
            draw.MoveCursor(x, y);
            Console.WriteLine("▣▦▦▦▦▦▦▦▣");
            draw.MoveCursor(x, y + 1);
            Console.WriteLine("▦");
            draw.MoveCursor(x+16, y + 1);
            Console.WriteLine("▦");
            draw.MoveCursor(x, y + 2);
            Console.WriteLine("▦");
            draw.MoveCursor(x + 16, y + 2);
            Console.WriteLine("▦");
            draw.MoveCursor(x, y + 3);
            Console.WriteLine("▦");
            draw.MoveCursor(x + 16, y + 3);
            Console.WriteLine("▦");
            draw.MoveCursor(x + 8, y + 4);
            Console.WriteLine("◈");
            draw.MoveCursor(x, y + 4);
            Console.WriteLine("▦");
            draw.MoveCursor(x + 16, y + 4);
            Console.WriteLine("▦");
            draw.MoveCursor(x, y + 5);
            Console.WriteLine("▦");
            draw.MoveCursor(x + 16, y + 5);
            Console.WriteLine("▦");
            draw.MoveCursor(x, y + 6);
            Console.WriteLine("▦");
            draw.MoveCursor(x + 16, y + 6);
            Console.WriteLine("▦");
            draw.MoveCursor(x, y + 7);
            Console.WriteLine("▣▦▦▦▦▦▦▦▣");
            Console.ResetColor();

        }

        public void TitleLogo(int x, int y)
        {

            Console.ForegroundColor = ConsoleColor.DarkRed;
            draw.MoveCursor(x, y);
            Console.WriteLine("       ______  __                  _____       ");
            draw.MoveCursor(x, y + 1);
            Console.WriteLine("       ___  / / /__________ _________  /_      ");
            draw.MoveCursor(x, y + 2);
            Console.WriteLine("       __  /_/ /_  _ \\  __ `/_  ___/  __/      ");
            draw.MoveCursor(x, y + 3);
            Console.WriteLine("       _  __  / /  __/ /_/ /_  /   / /_        ");
            draw.MoveCursor(x, y + 4);
            Console.WriteLine("       /_/ /_/  \\___/\\__,_/ /_/    \\__/        ");
            draw.MoveCursor(x, y + 5);
            Console.WriteLine("________                   ______              ");
            draw.MoveCursor(x, y + 6);
            Console.WriteLine("___  __ )_________________ ___  /______________");
            draw.MoveCursor(x, y + 7);
            Console.WriteLine("__  __  |_  ___/  _ \\  __ `/_  //_/  _ \\_  ___/");
            draw.MoveCursor(x, y + 8);
            Console.WriteLine("_  /_/ /_  /   /  __/ /_/ /_  ,<  /  __/  /    ");
            draw.MoveCursor(x, y + 9);
            Console.WriteLine("/_____/ /_/    \\___/\\__,_/ /_/|_| \\___//_/     ");

            Console.ResetColor();

        }

        public void Synopsis(int x, int y)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            draw.MoveCursor(x, y);
            Console.WriteLine("           _____   _____");
            draw.MoveCursor(x, y + 1);
            Console.WriteLine("          /     \\ /     \\");
            draw.MoveCursor(x, y + 2);
            Console.WriteLine("     ,   |       '       |");
            draw.MoveCursor(x, y + 3);
            Console.WriteLine("     I __L________       L__");
            draw.MoveCursor(x, y + 4);
            Console.WriteLine("O====IE__________/     ./___>");
            draw.MoveCursor(x, y + 5);
            Console.WriteLine("     I      \\.       ./");
            draw.MoveCursor(x, y + 6);
            Console.WriteLine("     `        \\.   ./");
            draw.MoveCursor(x, y + 7);
            Console.WriteLine("                \\ /");
            draw.MoveCursor(x, y + 8);
            Console.WriteLine("                 '");

            draw.MoveCursor(x, y + 9);

            Console.ResetColor();


        }
        public void TitleBox()
        {
            int[][] map = new int[MAP_SIZE_X][];

            // 맵 생성
            for (int height = 0; height < MAP_SIZE_Y; height++)
            {
                map[height] = new int[MAP_SIZE_X];
                for (int width = 0; width < MAP_SIZE_X; width++)
                {
                    map[height][width] = WALL;
                    if (height == 0)
                    {
                        map[height][width] = FLOOR;
                    }

                }
            }
            // 바닥 생성
            for (int height = 2; height < MAP_SIZE_Y - 1; height++)
            {
                for (int width = 1; width < MAP_SIZE_X - 1; width++)
                {
                    map[height][width] = FLOOR;

                }
            }
            for (int height = 0; height < MAP_SIZE_Y; height++)
            {
                for (int width = 0; width < MAP_SIZE_X; width++)
                {
                    if (map[height][width] == WALL)
                    {
                        draw.TitleBox();
                    }
                    else if (height == 0 && map[height][width] == FLOOR)
                    {
                        draw.Empty();

                    }
                    else if (map[height][width] == FLOOR)
                    {
                        draw.Floor();
                    }
                  
                }
                Console.WriteLine();
            }
        }
        // shop box

        public void ShopBox()
        {
            int[][] map = new int[MAP_SIZE_X][];

            // 맵 생성
            for (int height = 0; height < MAP_SIZE_Y; height++)
            {
                map[height] = new int[MAP_SIZE_X];
                for (int width = 0; width < MAP_SIZE_X; width++)
                {
                    map[height][width] = WALL;
                    if (height == 0)
                    {
                        map[height][width] = FLOOR;
                    }
                }
            }
            // 바닥 생성
            for (int height = 2; height < MAP_SIZE_Y - 1; height++)
            {
                for (int width = 1; width < MAP_SIZE_X - 1; width++)
                {
                    map[height][width] = FLOOR;
                }
            }
            for (int height = 0; height < MAP_SIZE_Y; height++)
            {
                for (int width = 0; width < MAP_SIZE_X; width++)
                {
                    if (map[height][width] == WALL)
                    {
                        draw.ShopBox();
                    }
                    else if (height == 0 && map[height][width] == FLOOR)
                    {
                        draw.Empty();

                    }
                    else if (map[height][width] == FLOOR)
                    {
                        draw.Floor();
                    }
                    else if (map[height][width] == PORTAL)
                    {
                        draw.Portal();
                    }
                }
                Console.WriteLine();
            }
        }
        // shop box
        public void HealBox()
        {
            int[][] map = new int[MAP_SIZE_X][];

            // 맵 생성
            for (int height = 0; height < MAP_SIZE_Y; height++)
            {
                map[height] = new int[MAP_SIZE_X];
                for (int width = 0; width < MAP_SIZE_X; width++)
                {
                    map[height][width] = WALL;
                    if (height == 0)
                    {
                        map[height][width] = FLOOR;
                    }
                }
            }
            // 바닥 생성
            for (int height = 2; height < MAP_SIZE_Y - 1; height++)
            {
                for (int width = 1; width < MAP_SIZE_X - 1; width++)
                {
                    map[height][width] = FLOOR;
                }
            }
            for (int height = 0; height < MAP_SIZE_Y; height++)
            {
                for (int width = 0; width < MAP_SIZE_X; width++)
                {
                    if (map[height][width] == WALL)
                    {
                        draw.HealBox();
                    }
                    else if (height == 0 && map[height][width] == FLOOR)
                    {
                        draw.Empty();

                    }
                    else if (map[height][width] == FLOOR)
                    {
                        draw.Floor();
                    }
                    else if (map[height][width] == PORTAL)
                    {
                        draw.Portal();
                    }
                }
                Console.WriteLine();
            }
        }
        // heal box
        //public void GAMEOVER()
        //{
        //    Console.ForegroundColor = ConsoleColor.Magenta;
        //    Console.Write("     _______  _______  __   __  _______ \n");
        //    Console.Write("    |       ||   _   ||  |_|  ||       |\n");
        //    Console.Write("    |    ___||  |_|  ||       ||    ___|\n");
        //    Console.Write("    |   | __ |       ||       ||   |___ \n");
        //    Console.Write("    |   ||  ||       ||       ||    ___|\n");
        //    Console.Write("    |   |_| ||   _   || ||_|| ||   |___ \n");
        //    Console.Write("    |_______||__| |__||_|   |_||_______|\n");
        //    Console.Write("\n");
        //    Console.Write("     _______  __   __  _______  ______   \n");
        //    Console.Write("    |       ||  | |  ||       ||    _ |  \n");
        //    Console.Write("    |   _   ||  |_|  ||    ___||   | ||  \n");
        //    Console.Write("    |  | |  ||       ||   |___ |   |_||_ \n");
        //    Console.Write("    |  |_|  ||       ||    ___||    __  |\n");
        //    Console.Write("    |       | |     | |   |___ |   |  | |\n");
        //    Console.Write("    |_______|  |___|  |_______||___|  |_|\n");
        //    Console.ResetColor();

        //}

        public void GAMEOVER(int x, int y)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            draw.MoveCursor(x, y);
            Console.Write(" _______  _______  _______  _______    _______           _______  _______ \r\n");
            draw.MoveCursor(x, y+1);
            Console.Write("(  ____ \\(  ___  )(       )(  ____ \\  (  ___  )|\\     /|(  ____ \\(  ____ )\r\n");
            draw.MoveCursor(x, y+2);
            Console.Write("| (    \\/| (   ) || () () || (    \\/  | (   ) || )   ( || (    \\/| (    )|\r\n");
            draw.MoveCursor(x, y+3);
            Console.Write("| |      | (___) || || || || (__      | |   | || |   | || (__    | (____)|\r\n");
            draw.MoveCursor(x, y+4);
            Console.Write("| | ____ |  ___  || |(_)| ||  __)     | |   | |( (   ) )|  __)   |     __)\r\n");
            draw.MoveCursor(x, y+5);
            Console.Write("| | \\_  )| (   ) || |   | || (        | |   | | \\ \\_/ / | (      | (\\ (   \r\n");
            draw.MoveCursor(x, y+6);
            Console.Write("| (___) || )   ( || )   ( || (____/\\  | (___) |  \\   /  | (____/\\| ) \\ \\__\r\n");
            draw.MoveCursor(x, y+7);
            Console.Write("(_______)|/     \\||/     \\|(_______/  (_______)   \\_/   (_______/|/   \\__/\r\n");
            Console.ResetColor();



        }

    }
}
