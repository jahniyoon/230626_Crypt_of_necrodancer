using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 30);
            Console.CursorVisible = false;

            MainGame maingame = new MainGame();
            RhythmBar bar = new RhythmBar();
            TestRoom testroom = new TestRoom();

            while (true)
            {
                maingame.Run();
                //testroom.Run();

                //bar.Bar();
            }

        }
    }
}
