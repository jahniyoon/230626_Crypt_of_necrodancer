using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    internal class BossStage : GameInfo
    {
        public static bool controlTiming = false;

        int[][] map = new int[MAP_SIZE_X][];

        Draw draw = new Draw();
        Enemy enemy = new Enemy();
        RhythmBar rhythmBar = new RhythmBar();
        Image image = new Image();





        public void Run(ref int playerHP, ref int playerMaxHP, ref int playerAttack, ref int gold, ref int score )
        {
            // 가이드 UI
            Console.ForegroundColor = ConsoleColor.DarkGray;
            draw.MoveCursor(0, 25);
            image.GuideBox();
            Console.ForegroundColor = ConsoleColor.Yellow;
            draw.MoveCursor(2, 26);
            Console.Write("●");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" : Player");
            draw.MoveCursor(16, 26);
            Console.Write("◈ : Enemy");
            draw.MoveCursor(30, 26);
            draw.Portal();
            Console.Write(" : Next Stage");
            draw.MoveCursor(48, 26);
            draw.Gold();
            Console.Write(" : Gold");
            draw.MoveCursor(60, 26);
            draw.HeartItem();
            Console.Write(" : HP+");
            Console.ResetColor();

            bossHP = 30;
            gameover = false;
            hunterPositions = new List<Position>();


            while (gameover == false) // 게임오버시 탈출
            {
                // stage 표시
                draw.MoveCursor(60, 19);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Boss Stage");
                Console.ResetColor();

                // 플레이어, 보스 포지션 생성
                Position playerPos = new Position(MAP_SIZE_X / 2, MAP_SIZE_Y / 2);
                Position BossPos = new Position(MAP_SIZE_X / 2, 4);


                // 맵 생성
                draw.MoveCursor(0, 0);
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



                // 장애물 생성
                map[6][1] = WALL;
                map[6][33] = WALL;
                map[MAP_SIZE_Y / 2-1][MAP_SIZE_X / 2] = WALL;

                while (wallCount < WALL_VALUE)
                {
                    int randomHeight = random.Next(7, MAP_SIZE_Y - 2);
                    int randomWidth = random.Next(3, MAP_SIZE_X - 2);

                    if (map[randomHeight][randomWidth] == FLOOR && randomHeight != playerPos.y || randomWidth != playerPos.x)
                    {
                        map[randomHeight][randomWidth] = WALL;
                        wallCount++;
                    }
                }
                wallCount = 0;


                // 출력 파트
                for (int height = 0; height < MAP_SIZE_Y; height++)
                {
                    for (int width = 0; width < MAP_SIZE_X; width++)
                    {
                        if (map[height][width] == WALL)
                        {
                            draw.Wall();
                        }
                        else if (height == 0 && map[height][width] == FLOOR)
                        {
                            draw.Empty();

                        }
                        else if (map[height][width] == FLOOR|| map[height][width] == ENEMY)
                        {
                            draw.Floor();
                        }
                        
                    }
                    Console.WriteLine();
                }

                // 플레이어 생성
                map[playerPos.y][playerPos.x] = PLAYER;

                //보스 생성
                for (int i = 0; i < 3 ; i++) // 보스가 9칸이기에 for문 사용
                {
                    for (int j = 0; j < 3; j++)
                    {
                        map[BossPos.y-1+i][BossPos.x-1+j] = BOSS;
                    }
                }

                // 플레이어 출력
                draw.MoveCursor(playerPos.x * 2, playerPos.y);
                draw.Player();
                draw.MoveCursor(30, 0);


                // 플레이어, 적 이동
                while (gameover == false)
                {
                    // score 표시
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    draw.MoveCursor(0, 19);
                    Console.Write("[Score] {00}", score);
                    Console.ResetColor();

                    //Boss Move
                    enemy.Boss(ref map, ref playerPos, ref BossPos, ref playerHP, ref bossHP);

                    // 15턴마다 헌터 3마리 생성
                    if (hunterMove % 15 == 0) 
                    {
                        map[17][1] = ENEMY;
                        hunterPositions.Add(new Position(1, 17));
                        map[17][33] = ENEMY;
                        hunterPositions.Add(new Position(33, 17));
                        map[17][16] = ENEMY;
                        hunterPositions.Add(new Position(16, 17));
                    }

                    // 헌터 이동
                    enemy.HunterMove(ref map, ref playerPos, ref hunterPositions, ref playerHP);
                   
                    // 헌터 출력
                    foreach (var enemy in hunterPositions)
                    {
                        draw.MoveCursor(enemy.x * 2, enemy.y);
                        draw.Hunter();
                    }

                    // 게임오버 확인
                    if (playerHP <= 0 || bossHP <= 0)
                    {
                        gameover = true;
                        break;
                    }

                    // player HP, player Gold UI
                    draw.PlayerHP(ref playerHP, ref playerMaxHP);
                    draw.PlayerGold(ref gold);

                    // 스탑워치 시작
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    bool inputSuccess = false;

                    rhythmBar.Bar(0, 21);
                    Console.Beep(300, 100);

                    //입력대기
                    while (true)
                    {
                        draw.MoveCursor(28, 19);
                        Console.WriteLine("             ");
                        // 버리는 타이밍 
                        if (stopwatch.ElapsedMilliseconds >= HEART_TIMING + 25)
                        {
                            break;
                        }
                        // 정확한 타이밍에 입력 받기
                        if (stopwatch.ElapsedMilliseconds >= HEART_TIMING - 25 && stopwatch.ElapsedMilliseconds < HEART_TIMING + 25)
                        {
                            if (Console.KeyAvailable)
                            {

                                ConsoleKeyInfo key = Console.ReadKey(true);

                                // 플레이어 ▲
                                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
                                {
                                    if (map[playerPos.y-1][playerPos.x] == BOSS)
                                    {
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player_Up();
                                        bossHP -= playerAttack;
                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.Boss(BossPos.x * 2, BossPos.y);
                                        Thread.Sleep(50);
                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.BossHurt(BossPos.x * 2, BossPos.y);
                                        Thread.Sleep(50);

                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.Boss(BossPos.x * 2, BossPos.y);
                                       

                                    }

                                    else if (map[playerPos.y - 1][playerPos.x] != WALL)
                                    {
                                        playerPos.y--;
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player_Up();
                                        draw.MoveCursor(playerPos.x * 2, (playerPos.y + 1));
                                        draw.Floor();
                                    }
                                    // 플레이어와 적의 충돌 체크
                                    if (map[playerPos.y][playerPos.x] == ENEMY)
                                    {
                                        Position enemyToRemove = null;

                                        foreach (var enemy in hunterPositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }
                                        foreach (var enemy in greenSlimePositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }

                                        if (enemyToRemove != null)
                                        {
                                            map[playerPos.y][playerPos.x] = HEART;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.HeartItem(); // 적을 사망시킨 위치를 바닥으로 변경
                                            playerPos.y++;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Player_Up();
                                            hunterPositions.Remove(enemyToRemove);

                                            // 적을 해당 리스트에서 제거
                                            hunterPositions.Remove(enemyToRemove);
                                            greenSlimePositions.Remove(enemyToRemove);
                                            blueSlimePositions.Remove(enemyToRemove);
                                        }
                                    }
                                    

                                }

                                // 플레이어 ▼
                                if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                                {
                                    if (map[playerPos.y + 1][playerPos.x] == BOSS)
                                    {
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player_Down();
                                        bossHP -= playerAttack;
                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.Boss(BossPos.x * 2, BossPos.y);
                                        Thread.Sleep(50);
                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.BossHurt(BossPos.x * 2, BossPos.y);
                                        Thread.Sleep(50);

                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.Boss(BossPos.x * 2, BossPos.y);
                                        

                                    }

                                    else if (map[playerPos.y + 1][playerPos.x] != WALL)
                                    {
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Floor();
                                        playerPos.y++;
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player_Down();

                                    }
                                    // 플레이어와 적의 충돌 체크
                                    if (map[playerPos.y][playerPos.x] == ENEMY)
                                    {
                                        Position enemyToRemove = null;

                                        foreach (var enemy in hunterPositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }
                                        foreach (var enemy in greenSlimePositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }
                                        foreach (var enemy in blueSlimePositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }
                                        if (enemyToRemove != null)
                                        {
                                            map[playerPos.y][playerPos.x] = HEART;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.HeartItem(); // 적을 사망시킨 위치를 바닥으로 변경
                                            playerPos.y--;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Player_Down();
                                            hunterPositions.Remove(enemyToRemove);

                                            // 적을 해당 리스트에서 제거
                                            hunterPositions.Remove(enemyToRemove);
                                            greenSlimePositions.Remove(enemyToRemove);
                                            blueSlimePositions.Remove(enemyToRemove);
                                        }
                                    }
                                }

                                // 플레이어 ◀
                                if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                                {
                                    if (map[playerPos.y][playerPos.x - 1] == BOSS)
                                    {
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player_Left();
                                        bossHP -= playerAttack;
                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.Boss(BossPos.x * 2, BossPos.y);
                                        Thread.Sleep(50);
                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.BossHurt(BossPos.x * 2, BossPos.y);
                                        Thread.Sleep(50);

                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.Boss(BossPos.x * 2, BossPos.y);
                                        

                                    }

                                    else if (map[playerPos.y][playerPos.x - 1] != WALL)
                                    {
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Floor();
                                        playerPos.x--;
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player_Left();

                                    }
                                    // 플레이어와 적의 충돌 체크
                                    if (map[playerPos.y][playerPos.x] == ENEMY)
                                    {
                                        Position enemyToRemove = null;

                                        foreach (var enemy in hunterPositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }
                                        foreach (var enemy in greenSlimePositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }
                                        foreach (var enemy in blueSlimePositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }
                                        if (enemyToRemove != null)
                                        {
                                            map[playerPos.y][playerPos.x] = HEART;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.HeartItem(); // 적을 사망시킨 위치를 바닥으로 변경
                                            playerPos.x++;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Player_Left();
                                            hunterPositions.Remove(enemyToRemove);

                                            // 적을 해당 리스트에서 제거
                                            hunterPositions.Remove(enemyToRemove);
                                            greenSlimePositions.Remove(enemyToRemove);
                                            blueSlimePositions.Remove(enemyToRemove);
                                        }
                                    }
                                }

                                // 플레이어 ▶
                                if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                                {
                                    if (map[playerPos.y][playerPos.x + 1] == BOSS)
                                    {
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player_Right();
                                        bossHP -= playerAttack;
                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.Boss(BossPos.x * 2, BossPos.y);
                                        Thread.Sleep(50);
                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.BossHurt(BossPos.x * 2, BossPos.y);
                                        Thread.Sleep(50);

                                        draw.MoveCursor(BossPos.x * 2, BossPos.y);
                                        draw.Boss(BossPos.x * 2, BossPos.y);
                                      

                                    }

                                    else if (map[playerPos.y][playerPos.x + 1] != WALL)
                                    {
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Floor();
                                        playerPos.x++;
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player_Right();
                                    }
                                    // 플레이어와 적의 충돌 체크
                                    if (map[playerPos.y][playerPos.x] == ENEMY)
                                    {
                                        Position enemyToRemove = null;

                                        foreach (var enemy in hunterPositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }
                                        foreach (var enemy in greenSlimePositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }
                                        foreach (var enemy in blueSlimePositions)
                                        {
                                            if (enemy.x == playerPos.x && enemy.y == playerPos.y)
                                            {
                                                enemyToRemove = enemy;
                                                break;
                                            }
                                        }


                                        if (enemyToRemove != null)
                                        {
                                            map[playerPos.y][playerPos.x] = HEART;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.HeartItem(); // 적을 사망시킨 위치를 바닥으로 변경
                                            playerPos.x--;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Player_Right();
                                            hunterPositions.Remove(enemyToRemove);

                                            // 적을 해당 리스트에서 제거
                                            hunterPositions.Remove(enemyToRemove);
                                            greenSlimePositions.Remove(enemyToRemove);
                                            blueSlimePositions.Remove(enemyToRemove);
                                        }
                                    }
                                }
                            
                                if (key.Key == ConsoleKey.P)
                                {
                                    if (DEBUG_MODE == true)
                                    {
                                        DEBUG_MODE = false;
                                    }
                                    else
                                        DEBUG_MODE = true;
                                    break;
                                }

                                //debug
                                if (DEBUG_MODE == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    draw.MoveCursor(72, 0);
                                    Console.WriteLine("[DEBUG]");

                                    draw.MoveCursor(72, 1);
                                    Console.WriteLine("Timing : {0}ms   ", stopwatch.ElapsedMilliseconds - HEART_TIMING);

                                    draw.MoveCursor(72, 2);
                                    Console.WriteLine("Last Timing : {0}", stopwatch.ElapsedMilliseconds);
                                    Console.ResetColor();
                                }
                                //debug

                                combo++;
                                if (combo >= 10)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                }

                                draw.MoveCursor(31, 19);
                                Console.WriteLine("COMBO {0}", combo);
                                Console.ResetColor();

                                inputSuccess = true;
                                break;
                            }
                        }
                        // 입력 if 종료
                    }
                    // 입력대기 while 종료  

                    // 타이밍이 안맞을 경우 Missed
                    if (!inputSuccess)
                    {
                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                        draw.Player_Stop();
                        Console.Beep(150, 100);

                        draw.MoveCursor(32, 19);
                        Console.WriteLine("MISSED!");
                        score--;
                        combo = 0;

                        //debug
                        if (DEBUG_MODE == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            draw.MoveCursor(72, 2);
                            Console.WriteLine("Last Timing : {0}", stopwatch.ElapsedMilliseconds);
                            Console.ResetColor();
                        }
                    }

                    // 입력 성공 후 입력 버퍼 비우기
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }

                    // 하트 먹으면 회복
                    if (map[playerPos.y][playerPos.x] == HEART)
                    {
                        map[playerPos.y][playerPos.x] = FLOOR;
                        playerHP += 1;
                        if(playerHP>playerMaxHP)
                        {
                        playerHP = playerMaxHP;
                        }
                    }
                    score++;
                    hunterMove++;
                    if (score >= highscore)
                    {
                        highscore = score;
                    }
                }//플레이어, 적 이동 while

                //게임 오버 판정
                if (playerHP <= 0)
                {
                    draw.PlayerHP(ref playerHP, ref playerMaxHP);

                    draw.MoveCursor(playerPos.x * 2, playerPos.y);
                    draw.PlayerDead();
                    draw.DeadHeart(30, 21);
                    Thread.Sleep(1000);

                    draw.MoveCursor(26, 5);
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  RUN SUMMARY  ");
                    Console.ResetColor();


                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    draw.MoveCursor(24, 10);
                    Console.Write(" SCORE : {0} ", score);
                    draw.MoveCursor(24, 11);
                    Console.Write(" HiGH SCORE : {0} ", highscore);
                    Console.ResetColor();

                    Console.ReadKey();
                    Console.Clear();
                    image.GAMEOVER(0, 5);
                    Console.ReadKey();
                    Console.Clear();


                    break;
                } // 게임오버시 while

                //게임 오버 판정
                if (bossHP <= 0)
                {
                    Console.ReadKey();

                    draw.PlayerHP(ref playerHP, ref playerMaxHP);
                    draw.MoveCursor(BossPos.x * 2, BossPos.y);
                    draw.BossDead(BossPos.x * 2, BossPos.y);
                    Thread.Sleep(1000);


                    draw.MoveCursor(26, 5);
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  RUN SUMMARY  ");
                    draw.MoveCursor(20, 7);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write("  Heart Breaker를 무찔렀다!  ");


                    Console.ResetColor();


                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    draw.MoveCursor(24, 10);
                    Console.Write(" SCORE : {0} ", score);
                    draw.MoveCursor(24, 11);
                    Console.Write(" HiGH SCORE : {0} ", highscore);
                    Thread.Sleep(1000);


                    draw.MoveCursor(30, 25);
                    Console.Write(" NEXT ▶");
                    Console.ResetColor();


                    Console.ReadKey();
                    Console.Clear();
                    break;
                } // 게임오버시 while 탈출

            }
            // } while 종료 게임오버시 탈출
        }
        // } Run 종료
    }
}
