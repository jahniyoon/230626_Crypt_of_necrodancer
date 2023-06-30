using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(70, 28);
            Console.CursorVisible = false;

            MainGame maingame = new MainGame();
            RhythmBar bar = new RhythmBar();
            BossStage testroom = new BossStage();

            while (true)
            {
                maingame.Title();
                maingame.Story();
                maingame.Run();

            }

        }
    }
}
