using System;

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
        public void Player_Left()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("◀");
            Console.ResetColor();
        }
        public void Player_Right()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("▶");
            Console.ResetColor();
        }
        public void Player_Up()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("▲");
            Console.ResetColor();
        }
        public void Player_Down()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("▼");
            Console.ResetColor();
        }
        public void Player_Stop()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("●");
            Console.ResetColor();
        }
        public void PlayerHurt()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("●");
            Console.ResetColor();
        }
        public void PlayerDead()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("◎");
            Console.ResetColor();
        }





        public void Hunter()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("◈");
            Console.ResetColor();

        }
        public void GreenSlime()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("●");
            Console.ResetColor();


        }
        public void BlueSlime()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("●");
            Console.ResetColor();

        }
        
        public void Boss(int left, int top)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Red;
            MoveCursor(left-2, top-1);
            Console.Write("▨▤▧");
            MoveCursor(left-2, top);
            Console.Write("▥");
            Console.ResetColor();
            MoveCursor(left, top);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("▼");
            Console.ResetColor();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Red;
            MoveCursor(left+2, top);
            Console.Write("▥");
            MoveCursor(left-2, top+1);
            Console.Write("▧▤▨");
            Console.ResetColor();


        }
        public void BossHurt(int left, int top)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            MoveCursor(left - 2, top - 1);
            Console.Write("▨▤▧");
            MoveCursor(left - 2, top);
            Console.Write("▥");
            Console.ResetColor();
            MoveCursor(left, top);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("▼");
            Console.ResetColor();

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            MoveCursor(left + 2, top);
            Console.Write("▥");
            MoveCursor(left - 2, top + 1);
            Console.Write("▧▤▨");
            Console.ResetColor();


        }
        public void BossDead(int left, int top)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            MoveCursor(left - 2, top - 1);
            Console.Write("▨▤▧");
            MoveCursor(left - 2, top);
            Console.Write("▥");
            Console.ResetColor();
            MoveCursor(left, top);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("◈");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            MoveCursor(left + 2, top);
            Console.Write("▥");
            MoveCursor(left - 2, top + 1);
            Console.Write("▧▤▨");
            Console.ResetColor();


        }
        public void BossFloor(int left, int top)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            MoveCursor(left - 2, top - 1);
            Console.Write("▦▦▦");
            MoveCursor(left - 2, top);
            Console.Write("▦▦▦");
                       MoveCursor(left - 2, top + 1);
            Console.Write("▦▦▦");
            Console.ResetColor();


        }
        public void BossAttack()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("▒▒");
            Console.ResetColor();

        }
        public void Wall()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("▦");
            Console.ResetColor();
        }
        public void TitleBox()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("▣");
            Console.ResetColor();
        }public void ShopBox()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("▩");
            Console.ResetColor();
        }
        public void HealBox()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("♥");
            Console.ResetColor();
        }
        public void Floor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("▦");
            Console.ResetColor();
        }
        public void Portal()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("▣");
            Console.ResetColor();

        }
        public void Empty()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("■");
            Console.ResetColor();

        }
        public void Gold()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("◐");
            Console.ResetColor();
        }
        public void Stick()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
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
            MoveCursor(left + 2, top);
            Console.Write("  ");
            MoveCursor(left + 6, top);
            Console.Write("  ");
            MoveCursor(left, top + 1);
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
            MoveCursor(left + 2, top + 1);
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
        public void HP_ON(int left, int top)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            MoveCursor(left, top);
            Console.Write("♥");
            Console.ResetColor();
        }
        public void HP_OFF(int left, int top)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            MoveCursor(left, top);
            Console.Write("♡");
            Console.ResetColor();
        }

        public void PlayerHP(ref int playerHP, ref int playerMaxHP)
        {

            for (int i = 0; i < playerMaxHP; i++)
            {
                HP_OFF(i * 2, 0);
            }
            for (int i = 0; i < playerHP; i++)
            {
                HP_ON(i * 2, 0);
            }  

        }
        public void PlayerGold(ref int gold)
        {
            MoveCursor(62,0);
            Gold();
            MoveCursor(64,0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("{00} G   ",gold);
            Console.ResetColor();


        }
       


    }
}
