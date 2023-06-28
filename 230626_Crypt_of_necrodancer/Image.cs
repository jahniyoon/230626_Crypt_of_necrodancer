using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("■■■■■■     ■■■■■■     ■■■■■■");
            draw.MoveCursor(x, y + 1);
            Console.WriteLine("■        ■     ■        ■     ■        ■");
            draw.MoveCursor(x, y + 2);
            Console.WriteLine("■   ①   ■     ■   ②   ■     ■   ③   ■");
            draw.MoveCursor(x, y + 3);
            Console.WriteLine("■        ■     ■        ■     ■        ■");
            draw.MoveCursor(x, y + 4);
            Console.WriteLine("■■■■■■     ■■■■■■     ■■■■■■");
            Console.ResetColor();

        }

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
        public void GAMEOVER()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("     _______  _______  __   __  _______ \n");
            Console.Write("    |       ||   _   ||  |_|  ||       |\n");
            Console.Write("    |    ___||  |_|  ||       ||    ___|\n");
            Console.Write("    |   | __ |       ||       ||   |___ \n");
            Console.Write("    |   ||  ||       ||       ||    ___|\n");
            Console.Write("    |   |_| ||   _   || ||_|| ||   |___ \n");
            Console.Write("    |_______||__| |__||_|   |_||_______|\n");
            Console.Write("\n");
            Console.Write("     _______  __   __  _______  ______   \n");
            Console.Write("    |       ||  | |  ||       ||    _ |  \n");
            Console.Write("    |   _   ||  |_|  ||    ___||   | ||  \n");
            Console.Write("    |  | |  ||       ||   |___ |   |_||_ \n");
            Console.Write("    |  |_|  ||       ||    ___||    __  |\n");
            Console.Write("    |       | |     | |   |___ |   |  | |\n");
            Console.Write("    |_______|  |___|  |_______||___|  |_|\n");
            Console.ResetColor();

        }

    }
}
