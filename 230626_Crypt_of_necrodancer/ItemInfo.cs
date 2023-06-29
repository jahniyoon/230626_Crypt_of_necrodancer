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
        public void ItemIs(ref string itemIs, ref int playerHP, ref int playerMaxHP, ref int stage, ref int playerAttack)
        {
            if (itemIs == "낡은 지도")
            {
                draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                Console.WriteLine("                                                      ");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("                                                      ");

                int randomNum = random.Next(1, 3);
                stage += randomNum;

                if (stage > 10)
                {
                    stage = 10;
                }

                draw.MoveCursor(leftPadding - 2, topPadding);
                Console.WriteLine("낡은 지도에는 지름길이 표시되어 있었습니다.");
                draw.MoveCursor(leftPadding - 2, topPadding);
                Console.WriteLine("{0} 스테이지로 이동합니다.", stage);

                draw.PlayerHP(ref playerHP, ref playerMaxHP);

            }

            if (itemIs == "파워 포션")
            {
                draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                Console.WriteLine("                                                      ");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("                                                      ");

                draw.MoveCursor(leftPadding - 2, topPadding);
                Console.WriteLine("당신은 파워 포션을 마십니다.");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("심장이 더 두근거립니다!");
                playerAttack+=3;

            }
            if (itemIs == "뜨거운 여섯")
            {
                draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                Console.WriteLine("                                                      ");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("                                                      ");

                draw.MoveCursor(leftPadding - 2, topPadding);
                Console.WriteLine("당신은 뜨거운 여섯을 마십니다.");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("심장이 미친듯이 두근거립니다!");
                playerAttack += 10;

            }
            if (itemIs == "붕대")
            {
                draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                Console.WriteLine("                                                      ");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("                                                      ");

                draw.MoveCursor(leftPadding - 2, topPadding);
                Console.WriteLine("체력을 1 회복합니다.");

                playerHP += 1;
                if (playerHP > playerMaxHP)
                {
                    playerHP = playerMaxHP;
                    draw.MoveCursor(leftPadding - 2, topPadding);
                    Console.WriteLine("최대 체력이 한계에 도달했습니다...");
                }
                draw.PlayerHP(ref playerHP, ref playerMaxHP);

            }
            if (itemIs == "회복 포션")
            {
                draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                Console.WriteLine("                                                      ");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("                                                      ");

                draw.MoveCursor(leftPadding - 2, topPadding);
                Console.WriteLine("체력을 3 회복합니다.");

                playerHP += 3;
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
                Console.WriteLine("하트가 1칸 추가되었습니다.");

                playerMaxHP++;
                playerHP++;

                if (playerMaxHP > 20)
                {
                    draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                    Console.WriteLine("                                                      ");
                    draw.MoveCursor(leftPadding - 2, topPadding + 1);
                    Console.WriteLine("                                                      ");

                    draw.MoveCursor(leftPadding - 2, topPadding);
                    Console.WriteLine("하트가 한계에 도달했습니다...");
                    playerMaxHP = 20;
                    if (playerHP > 20)
                    {
                        playerHP = 20;
                    }
                }

                draw.PlayerHP(ref playerHP, ref playerMaxHP);

            }
            if (itemIs == "큰 하트 보관함")
            {
                draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                Console.WriteLine("                                                      ");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("                                                      ");

                draw.MoveCursor(leftPadding - 2, topPadding);
                Console.WriteLine("하트가 3칸 추가되었습니다.");

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
