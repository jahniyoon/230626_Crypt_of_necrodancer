﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _230626_Crypt_of_necrodancer
{
    public class ClearEvent : GameInfo
    {
        Draw draw = new Draw();
        Image image = new Image();
        ItemInfo itemInfo = new ItemInfo(); 

        int leftPadding = 5;
        int topPadding = 20;
        int Box1leftPadding = 8;
        int Box1topPadding = 5;
        string buyItem = default;


        public void Run(ref int gold, ref int playerHP, ref int playerMaxHP)
        {

            int randomNum = random.Next(1,10);

            if (randomNum <= 5)
            {
                Shop(ref gold, ref playerHP,ref playerMaxHP,ref invenSize, ref strings, ref inventory, ref stage, ref playerAttack);
            }
            else if (randomNum >= 6)
            {
                Heal(ref playerHP, ref playerMaxHP, ref gold);
            }
        }


        public void Shop(ref int Gold, ref int playerHP, ref int playerMaxHP, ref int invenSize, ref String[] strings, ref String[] inventory, ref int stage, ref int playerAttack)
        {
            Dictionary<string, int> itemInventory = new Dictionary<string, int>();
            itemInventory.Add("하트 보관함", 200);
            itemInventory.Add("하트 보관함+", 500);
            itemInventory.Add("평범한 붕대", 100);
            itemInventory.Add("회복 포션", 300);
            itemInventory.Add("낡은 지도", 500);
            itemInventory.Add("파워 포션", 200);
            itemInventory.Add("뜨거운 여섯", 1500);

            // 초기화
            int ShopItemNum = default;
            int inputNum = default;



            ShopItemNum = random.Next(0, 5);             // 상점 배치 랜덤번호 



            // 상점 영역
            image.ShopBox();
            image.TextBox();
            draw.PlayerHP(ref playerHP, ref playerMaxHP);
            draw.PlayerGold(ref Gold);
            draw.MoveCursor(1, 25);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" ※ 방향키 : 선택    Enter : 결정");
            Console.ResetColor();

            draw.MoveCursor(leftPadding - 2, topPadding); // 상인 대화
            Console.WriteLine("[상점 주인]");    
            draw.MoveCursor(leftPadding - 2, topPadding+2); // 상인 대화
            Console.WriteLine("어서오시오 이방인이여. 마음껏 골라보시게.");

            image.Box(Box1leftPadding - 2, Box1topPadding);
            image.Box(Box1leftPadding+18, Box1topPadding);
            image.Box(Box1leftPadding+38, Box1topPadding);

            // Item number
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            draw.MoveCursor(12, 12); 
            Console.WriteLine("  ①  ");
            draw.MoveCursor(32, 12);
            Console.WriteLine("  ②  ");
            draw.MoveCursor(52, 12);
            Console.WriteLine("  ③  ");
            Console.ResetColor();

            draw.MoveCursor(8, 14);
            Console.WriteLine("[ {0} ]", strings[ShopItemNum]);
            draw.MoveCursor(28, 14);
            Console.WriteLine("[ {0} ]", strings[ShopItemNum + 1]);
            draw.MoveCursor(48, 14);
            Console.WriteLine("[ {0} ]", strings[ShopItemNum + 2]);

            Console.ForegroundColor = ConsoleColor.Yellow;
            draw.MoveCursor(12, 15);
            Console.WriteLine("{0} G ", itemInventory[strings[ShopItemNum]]);
            draw.MoveCursor(32, 15);
            Console.WriteLine("{0} G", itemInventory[strings[ShopItemNum + 1]]);
            draw.MoveCursor(52, 15);
            Console.WriteLine("{0} G",itemInventory[strings[ShopItemNum + 2]]);
            Console.ResetColor();

            draw.MoveCursor(60, 3);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("EXIT ☞");
            Console.ResetColor();


            //draw.Arrow(14, 16);
            //draw.Arrow(34, 16);
            //draw.Arrow(54, 16);



            draw.MoveCursor(leftPadding+55, topPadding + 3);
            Console.Write("NEXT ▶");
            Console.ReadKey();

            draw.Arrow(14, 16);
            draw.MoveCursor(0, 19);
            image.TextBox();
            draw.MoveCursor(leftPadding - 2, topPadding + 1);
            Console.WriteLine("[ {0} ] {1}G", strings[ShopItemNum], itemInventory[strings[ShopItemNum]]);
            itemInfo.ItemInfoIs(ref strings[ShopItemNum]);

            // 구매 체크
            int shopItemNum = 1;
            bool inputCheck = false;
            while (!inputCheck)
            {

                if (Console.KeyAvailable)
                {
                    
                    draw.MoveCursor(30, 20);

                    ConsoleKeyInfo key = Console.ReadKey(true);

                    // 플레이어 ◀
                    if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                    {
                        if (shopItemNum > 1)
                        {
                            shopItemNum--;
                            draw.MoveCursor(0, 19);
                            image.TextBox();
                        }

                    }
                    // 플레이어 ▶
                    else if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                    {
                        if (shopItemNum < 4)
                        {
                            shopItemNum++;
                            draw.MoveCursor(0, 19);
                            image.TextBox();
                        }
                       
                    }
                    // 플레이어 ▲
                    else if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
                    {
                        shopItemNum = 4;
                        draw.MoveCursor(0, 19);
                        image.TextBox();

                    }
                    // 플레이어 ▼
                    else if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                    {
                        if (shopItemNum == 4)
                        {
                            shopItemNum = 3;
                            draw.MoveCursor(0, 19);
                            image.TextBox();
                        }
                    }
                    // 플레이어 확인
                    else if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
                    {
                        inputNum = shopItemNum;
                        draw.MoveCursor(0, 19);
                        image.TextBox();
                        break;
                    }

                    // 화살표 지우기
                    draw.Arrow_OFF(14, 16);
                    draw.Arrow_OFF(34, 16);
                    draw.Arrow_OFF(54, 16);
                    draw.Right_Arrow_OFF(54, 3);

                  

                    if (shopItemNum == 1)
                    {
                        draw.Arrow(14, 16);

                        draw.MoveCursor(leftPadding - 2, topPadding + 1);
                        Console.WriteLine("[ {0} ] {1}G", strings[ShopItemNum], itemInventory[strings[ShopItemNum]]);
                        itemInfo.ItemInfoIs(ref strings[ShopItemNum]);

                    }

                    if (shopItemNum == 2)
                    {
                        draw.Arrow(34, 16);

                        draw.MoveCursor(leftPadding - 2, topPadding + 1);
                        Console.WriteLine("[ {0} ] {1}G", strings[ShopItemNum+1], itemInventory[strings[ShopItemNum+1]]);
                        itemInfo.ItemInfoIs(ref strings[ShopItemNum+1]);

                    }
                    if (shopItemNum == 3)
                    {
                        draw.Arrow(54, 16);

                        draw.MoveCursor(leftPadding - 2, topPadding + 1);
                        Console.WriteLine("[ {0} ] {1}G", strings[ShopItemNum+2], itemInventory[strings[ShopItemNum+2]]);
                        itemInfo.ItemInfoIs(ref strings[ShopItemNum+2]);
                    }
                    if (shopItemNum == 4)
                    {
                        draw.Right_Arrow(54, 3);

                        draw.MoveCursor(leftPadding - 2, topPadding + 1); 
                        Console.WriteLine("출구가 보인다. 그냥 나갈까?");
                    }

                }

              
                //if (int.TryParse(Console.ReadLine(), out inputNum)) // 숫자 체크
                //{
                //    draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                //    Console.WriteLine("                                                      ");
                //    draw.MoveCursor(leftPadding - 2, topPadding + 1);
                //    Console.WriteLine("                                                      ");

                //    if (inputNum >= 1 && inputNum <= 4) // 1 ~ 4 값 체크
                //    {
                //        break;
                //    }
                //    else
                //    {
                //        draw.MoveCursor(leftPadding - 2, topPadding);
                //        Console.WriteLine("물건은 3번까지 밖에 없다네...");

                //        inputCheck = false;
                //    }
                //}
                //else
                //{
                //    draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                //    Console.WriteLine("                                                      ");
                //    draw.MoveCursor(leftPadding - 2, topPadding + 1);
                //    Console.WriteLine("                                                      ");

                //    draw.MoveCursor(leftPadding - 2, topPadding);
                //    Console.WriteLine("숫자로 가르켜주게나...");


                //    inputCheck = false;
                //}
            }
            Console.Write("");

            // 구매 결정
            if (inputNum < 5)
            {
                draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                Console.WriteLine("                                                      ");
                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.WriteLine("                                                      ");

                switch (inputNum)
                {
                    case 1:
                        draw.MoveCursor(leftPadding - 2, topPadding + 1);

                        buyItem = strings[ShopItemNum];

                        Console.WriteLine("'{0}' 아이템을 구매하기로했습니다.", strings[ShopItemNum]);
                        Thread.Sleep(800);
                        draw.MoveCursor(0, 19);
                        image.TextBox();

                        if (broke(itemInventory[strings[ShopItemNum]], Gold) == 0)
                        {
                            Gold = Gold - itemInventory[strings[ShopItemNum]];
                            inventory[invenSize] = strings[ShopItemNum];
                            invenSize++;
                            itemInfo.ItemIs(ref buyItem, ref playerHP, ref playerMaxHP, ref stage, ref playerAttack);
                        }

                        draw.PlayerHP(ref playerHP, ref playerMaxHP);
                        draw.PlayerGold(ref Gold);

                        draw.MoveCursor(leftPadding + 18, topPadding + 1);
                        Thread.Sleep(800);

                        draw.MoveCursor(leftPadding + 55, topPadding + 3);
                        Console.Write("NEXT ▶");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:

                        buyItem = strings[ShopItemNum + 1];
                        draw.MoveCursor(leftPadding - 2, topPadding + 1);

                        Console.WriteLine("'{0}' 아이템을 구매하기로했습니다.", strings[ShopItemNum + 1]);
                        Thread.Sleep(800);
                        draw.MoveCursor(0, 19);
                        image.TextBox();
                        if (broke(itemInventory[strings[ShopItemNum + 1]], Gold) == 0)
                        {
                            Gold = Gold - itemInventory[strings[ShopItemNum + 1]];
                            inventory[invenSize] = strings[ShopItemNum + 1];
                            invenSize++;
                            itemInfo.ItemIs(ref buyItem, ref playerHP, ref playerMaxHP, ref stage, ref playerAttack);

                        }
                        draw.PlayerHP(ref playerHP, ref playerMaxHP);
                        draw.PlayerGold(ref Gold);

                        draw.MoveCursor(leftPadding + 18, topPadding + 1);
                        Thread.Sleep(800);

                        draw.MoveCursor(leftPadding + 55, topPadding + 3);
                        Console.Write("NEXT ▶"); 
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        draw.MoveCursor(leftPadding - 2, topPadding + 1);

                        buyItem = strings[ShopItemNum + 2];

                        Console.WriteLine("'{0}' 아이템을 구매하기로했습니다.", strings[ShopItemNum + 2]);
                        Thread.Sleep(800);
                        draw.MoveCursor(0, 19);
                        image.TextBox();
                        if (broke(itemInventory[strings[ShopItemNum + 2]], Gold) == 0)
                        {
                            Gold = Gold - itemInventory[strings[ShopItemNum + 2]];
                            inventory[invenSize] = strings[ShopItemNum + 2];
                            invenSize++;
                            itemInfo.ItemIs(ref buyItem, ref playerHP, ref playerMaxHP, ref stage, ref playerAttack);
                        }

                        draw.PlayerHP(ref playerHP, ref playerMaxHP);
                        draw.PlayerGold(ref Gold);

                        draw.MoveCursor(leftPadding + 18, topPadding + 1);
                        Thread.Sleep(800);

                        draw.MoveCursor(leftPadding + 55, topPadding + 3);
                        Console.Write("NEXT ▶");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        draw.MoveCursor(leftPadding - 2, topPadding + 1);
                        Console.WriteLine("상점을 나갑니다.");
                        Thread.Sleep(800);

                        draw.MoveCursor(leftPadding + 18, topPadding + 1);
                        draw.MoveCursor(leftPadding + 55, topPadding + 3);
                        Console.Write("NEXT ▶");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }





            }


        }
        // Shop 종료
        public int broke(int playGold, int buyGold)
        {
            if (playGold > buyGold)
            {
                draw.MoveCursor(leftPadding - 2, topPadding + 1);

                Console.WriteLine("골드가 부족합니다...");
                return 1;
            }
            return 0;

        }



        public void Heal(ref int playerHP, ref int playerMaxHP,ref int Gold)
        {
            image.HealBox();
            image.TextBox();
            draw.Heart(30,8);

            draw.MoveCursor(leftPadding-2, topPadding);
            Console.WriteLine("잠시 쉴만한 곳을 찾았습니다.");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            draw.MoveCursor(leftPadding - 2, topPadding+1);
            Console.WriteLine("♥");
            Console.ResetColor();
        

            draw.MoveCursor(leftPadding, topPadding+1);
            Console.WriteLine("을 회복합니다.");
            draw.PlayerHP(ref playerHP, ref playerMaxHP);
            draw.PlayerGold(ref Gold);

            playerHP++;
            Console.ReadKey();

            if (playerHP > playerMaxHP)
            {
                draw.MoveCursor(leftPadding - 2, topPadding+2);
                Console.WriteLine("하지만 이미 체력이 가득 찼습니다.");
                playerHP = 3;
            }
            Console.ReadKey();

        }
        // Heal 종료

    }
}
