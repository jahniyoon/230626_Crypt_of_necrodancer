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
            Console.CursorVisible = false;

            MainGame maingame = new MainGame();

            while (true)
            {
                maingame.Run();
            }

        }
    }
}
