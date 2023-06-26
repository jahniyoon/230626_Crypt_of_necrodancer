using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    public class Draw
    {

        public void MoveCursor(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }
        public void Player()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("●");
            Console.ResetColor();
        }
        public void PlayerDead()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("●");
            Console.ResetColor();
        }
        public void Enemy()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("◈");
            Console.ResetColor();

        }
        public void Wall()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("▦");
            Console.ResetColor();
        }
        public void Floor()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("□");
            Console.ResetColor();
        }
        public void Empty()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("■");
            Console.ResetColor();

        }

        public void Stick()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("▥");
            Console.ResetColor();
        }
        public void VoidHeart()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("♡");
            Console.ResetColor();
        }
        public void Heart(int left, int top)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            MoveCursor(left+2, top);
            Console.Write("  ");
            MoveCursor(left + 6, top);
            Console.Write("  ");
            MoveCursor(left, top+1);
            Console.Write("          ");
            MoveCursor(left + 2, top + 2);
            Console.Write("      ");
            MoveCursor(left + 4, top + 3);
            Console.Write("  ");
            Console.ResetColor();
        }
        public void GrayHeart(int left, int top)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            MoveCursor(left+2, top + 1);
            Console.Write("  ");
            MoveCursor(left + 6, top + 1);
            Console.Write("  ");
            MoveCursor(left + 4, top + 2);
            Console.Write("  ");
            Console.ResetColor();
            MoveCursor(left + 4, top + 3);

            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("■");
            Console.ResetColor();
        }
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
