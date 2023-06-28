using System;
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
                Shop(ref gold, ref playerHP,ref playerMaxHP,ref invenSize, ref strings, ref inventory);
            }
            else if (randomNum >= 6)
            {
                Heal(ref playerHP, ref playerMaxHP, ref gold);
            }
        }


        public void Shop(ref int Gold, ref int playerHP, ref int playerMaxHP, ref int invenSize, ref String[] strings, ref String[] inventory)
        {
            Dictionary<string, int> itemInventory = new Dictionary<string, int>();
            itemInventory.Add("거인의 대검", 500);
            itemInventory.Add("롱롱 스피어", 1500);
            itemInventory.Add("개사기템", 9999);
            itemInventory.Add("하트 보관함", 2000);
            itemInventory.Add("하트 보관함 x 3", 3000);
            itemInventory.Add("회복 포션", 500);
            itemInventory.Add("열쇠", 5000);

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
            Console.WriteLine(" ※ 1 ~ 3 : 아이템 선택    4 : 상점 나가기");
            Console.ResetColor();

            draw.MoveCursor(leftPadding - 2, topPadding); // 상인 대화
            Console.WriteLine("[상점 주인 : 어서오시오. 마음껏 골라보시게.");

            draw.MoveCursor(Box1leftPadding, Box1topPadding); // 박스 위치
            image.Box(Box1leftPadding, Box1topPadding);

            draw.MoveCursor(8, 12);
            Console.WriteLine("[ {0} ]", strings[ShopItemNum], itemInventory[strings[ShopItemNum]]);
            draw.MoveCursor(12, 13);
            Console.WriteLine("{1} G", strings[ShopItemNum], itemInventory[strings[ShopItemNum]]);

            draw.MoveCursor(24, 12);
            Console.WriteLine("[ {0} ]", strings[ShopItemNum + 1], itemInventory[strings[ShopItemNum + 1]]);
            draw.MoveCursor(29, 13);
            Console.WriteLine(" {1} G", strings[ShopItemNum + 1], itemInventory[strings[ShopItemNum + 1]]);

            draw.MoveCursor(41, 12);
            Console.WriteLine("[ {0} ]", strings[ShopItemNum + 2], itemInventory[strings[ShopItemNum + 2]]);
            draw.MoveCursor(45, 13);
            Console.WriteLine("{1} G", strings[ShopItemNum + 2], itemInventory[strings[ShopItemNum + 2]]);



            // 구매 체크
            bool inputCheck = false;
            while (!inputCheck)
            {
                Thread.Sleep(800);

                draw.MoveCursor(leftPadding - 2, topPadding + 1);
                Console.Write("무엇을 살까? => ");


                if (int.TryParse(Console.ReadLine(), out inputNum)) // 숫자 체크
                {
                    draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                    Console.WriteLine("                                                      ");
                    draw.MoveCursor(leftPadding - 2, topPadding + 1);
                    Console.WriteLine("                                                      ");

                    if (inputNum >= 1 && inputNum <= 4) // 1 ~ 4 값 체크
                    {
                        break;
                    }
                    else
                    {
                        draw.MoveCursor(leftPadding - 2, topPadding);
                        Console.WriteLine("물건은 3번까지 밖에 없다네...");

                        inputCheck = false;
                    }
                }
                else
                {
                    draw.MoveCursor(leftPadding - 2, topPadding); // 대화 초기화
                    Console.WriteLine("                                                      ");
                    draw.MoveCursor(leftPadding - 2, topPadding + 1);
                    Console.WriteLine("                                                      ");

                    draw.MoveCursor(leftPadding - 2, topPadding);
                    Console.WriteLine("숫자로 가르켜주게나...");


                    inputCheck = false;
                }
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
                        draw.MoveCursor(leftPadding - 2, topPadding);

                        buyItem = strings[ShopItemNum];

                        Console.WriteLine("'{0}' 아이템을 구매하기로했습니다.", strings[ShopItemNum]);
                        Thread.Sleep(800);

                        if (broke(itemInventory[strings[ShopItemNum]], Gold) == 0)
                        {
                            Gold = Gold - itemInventory[strings[ShopItemNum]];
                            inventory[invenSize] = strings[ShopItemNum];
                            invenSize++;
                            itemInfo.ItemIs(ref buyItem, ref playerHP, ref playerMaxHP);
                        }

                        draw.PlayerHP(ref playerHP, ref playerMaxHP);
                        draw.PlayerGold(ref Gold);

                        draw.MoveCursor(leftPadding + 18, topPadding + 1);
                        Thread.Sleep(800);

                        Console.WriteLine("                             Next >");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        draw.MoveCursor(leftPadding - 2, topPadding);
                        
                        buyItem = strings[ShopItemNum + 1];

                        Console.WriteLine("'{0}' 아이템을 구매하기로했습니다.", strings[ShopItemNum + 1]);
                        if (broke(itemInventory[strings[ShopItemNum + 1]], Gold) == 0)
                        {
                            Gold = Gold - itemInventory[strings[ShopItemNum + 1]];
                            inventory[invenSize] = strings[ShopItemNum + 1];
                            invenSize++;
                        itemInfo.ItemIs(ref buyItem, ref playerHP, ref playerMaxHP);

                        }
                        draw.PlayerHP(ref playerHP, ref playerMaxHP);
                        draw.PlayerGold(ref Gold);

                        draw.MoveCursor(leftPadding + 18, topPadding + 1);
                        Thread.Sleep(800);

                        Console.WriteLine("                             Next >");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        draw.MoveCursor(leftPadding - 2, topPadding);

                         buyItem = strings[ShopItemNum + 2];

                        Console.WriteLine("'{0}' 아이템을 구매하기로했습니다.", strings[ShopItemNum + 2]);
                        if (broke(itemInventory[strings[ShopItemNum + 2]], Gold) == 0)
                        {
                            Gold = Gold - itemInventory[strings[ShopItemNum + 2]];
                            inventory[invenSize] = strings[ShopItemNum + 2];
                            invenSize++;
                        itemInfo.ItemIs(ref buyItem, ref playerHP, ref playerMaxHP);
                        }

                        draw.PlayerHP(ref playerHP, ref playerMaxHP);
                        draw.PlayerGold(ref Gold);

                        draw.MoveCursor(leftPadding + 18, topPadding + 1);
                        Thread.Sleep(800);

                        Console.WriteLine("                             Next >");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        draw.MoveCursor(leftPadding - 2, topPadding);
                        Console.WriteLine("상점을 나갑니다.");
                        Thread.Sleep(800);

                        draw.MoveCursor(leftPadding + 18, topPadding + 1);
                        Console.WriteLine("                             Next >");
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
