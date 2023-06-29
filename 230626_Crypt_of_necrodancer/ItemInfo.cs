using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    public class ItemInfo : GameInfo
    {
        Draw draw = new Draw();
        int leftPadding = 5;
        int topPadding = 20;

        // 상점에서 구매하는 아이템
        public void ItemIs(ref string itemIs, ref int playerHP, ref int playerMaxHP)
        {

            if (itemIs == "회복 포션")
            {
                draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                Console.WriteLine("                                                      ");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("                                                      ");

                draw.MoveCursor(leftPadding - 2, topPadding);
                Console.WriteLine("체력을 회복합니다.");

                playerHP++;
                if (playerHP > playerMaxHP)
                {
                    playerHP = playerMaxHP;
                    draw.MoveCursor(leftPadding - 2, topPadding);
                    Console.WriteLine("최대 체력이 한계에 도달했습니다...");
                }
                draw.PlayerHP(ref playerHP, ref playerMaxHP);

            }
            if (itemIs == "하트 보관함")
            {
                draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                Console.WriteLine("                                                      ");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("                                                      ");

                draw.MoveCursor(leftPadding - 2, topPadding);
                Console.WriteLine("최대 체력이 1 상승합니다.");

                playerMaxHP++;
                playerHP++;

                if (playerMaxHP > 20)
                {
                    draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                    Console.WriteLine("                                                      ");
                    draw.MoveCursor(leftPadding - 2, topPadding + 1);
                    Console.WriteLine("                                                      ");

                    draw.MoveCursor(leftPadding - 2, topPadding);
                    Console.WriteLine("최대 체력이 한계에 도달했습니다...");
                    playerMaxHP = 20;
                    if (playerHP > 20)
                    {
                        playerHP = 20;
                    }
                }

                draw.PlayerHP(ref playerHP, ref playerMaxHP);

            }
            if (itemIs == "하트 보관함 x 3")
            {
                draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                Console.WriteLine("                                                      ");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("                                                      ");

                draw.MoveCursor(leftPadding - 2, topPadding);
                Console.WriteLine("최대 체력이 3 상승합니다.");

                playerMaxHP += 3;
                playerHP += 3;

                if (playerMaxHP > 20)
                {
                    draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                    Console.WriteLine("                                                      ");
                    draw.MoveCursor(leftPadding - 2, topPadding + 1);
                    Console.WriteLine("                                                      ");

                    draw.MoveCursor(leftPadding - 2, topPadding);
                    Console.WriteLine("최대 체력이 한계에 도달했습니다...");
                    playerMaxHP = 20;
                    if (playerHP > 20)
                    {
                        playerHP = 20;
                    }
                }

                draw.PlayerHP(ref playerHP, ref playerMaxHP);
            }
        }

    }
}
