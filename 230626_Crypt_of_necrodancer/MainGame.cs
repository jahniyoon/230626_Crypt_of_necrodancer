using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;


namespace _230626_Crypt_of_necrodancer
{
    internal class MainGame : GameInfo
    {
        public static bool controlTiming = false;
        private static System.Timers.Timer timer;


        int[][] map = new int[MAP_SIZE_X][];

        Draw draw = new Draw();
        Image image = new Image();
        Enemy enemy = new Enemy();
        ClearEvent clearevent = new ClearEvent();
        RhythmBar rhythmBar = new RhythmBar();
        BossStage bossstage = new BossStage();

        public void Run()
        {
            // 기본값
            hunterPositions = new List<Position>();
            greenSlimePositions = new List<Position>();
            blueSlimePositions = new List<Position>();
            stageturn = default;
            gameover = false;
            ClearCheck = false;

            playerHP = 3;
            playerMaxHP = 3;
            playerAttack = 1;

            stage = 1;
            score = default;
            gold = default;

            hunterMove = default;
            greenSlimeMove = default;
            greenSlimeCount = default;
            blueSlimeMove = default;
            blueSlimeCount = default;

            while (gameover == false) // 게임오버시 탈출
            {
                retry = false;

                // 보스스테이지 체크
                if (stage >= BOSS_STAGE)
                {
                    break;
                }

                Position playerPos = new Position(MAP_SIZE_X / 2, MAP_SIZE_Y / 2);
                // stage 표시
                Console.ForegroundColor = ConsoleColor.DarkGray;
                draw.MoveCursor(64, 19);
                Console.Write("Stage{00}", stage);
                Console.ResetColor();


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
               
                // 네모난 방 생성
                int roomSize = random.Next(5, 10); // 랜덤한 방 크기
                int startX = random.Next(1, MAP_SIZE_X - roomSize - 1); // 시작 X 좌표 (벽과 겹치지 않도록 범위 조절)
                int startY = random.Next(2, MAP_SIZE_Y - roomSize - 1); // 시작 Y 좌표 (벽과 겹치지 않도록 범위 조절)
                int doorSide = random.Next(4); // 문이 위치할 방의 한 변 (0: 위쪽, 1: 오른쪽, 2: 아래쪽, 3: 왼쪽)

                for (int height = startY; height < startY + roomSize; height++)
                {
                    for (int width = startX; width < startX + roomSize; width++)
                    {
                        // 방 내부
                        if (height > startY && height < startY + roomSize - 1 && width > startX && width < startX + roomSize - 1)
                        {
                            map[height][width] = FLOOR;
                        }
                        // 방과 겹치는 외곽 벽
                        else
                        {
                            map[height][width] = WALL;
                        }

                        // 문 생성
                        if (doorSide == 0 && height == startY && width >= startX && width <= startX + roomSize - 1)
                        {
                            map[height][width] = FLOOR;
                            break; // 한 칸만 문이 위치하도록 하기 위해 반복문 종료
                        }
                        else if (doorSide == 1 && width == startX + roomSize - 1 && height >= startY && height <= startY + roomSize - 1)
                        {
                            map[height][width] = FLOOR;
                            break;
                        }
                        else if (doorSide == 2 && height == startY + roomSize - 1 && width >= startX && width <= startX + roomSize - 1)
                        {
                            map[height][width] = FLOOR;
                            break;
                        }
                        else if (doorSide == 3 && width == startX && height >= startY && height <= startY + roomSize - 1)
                        {
                            map[height][width] = FLOOR;
                            break;
                        }
                    }
                }

                // 장애물 벽 생성
                while (wallCount < WALL_VALUE)
                {
                    int randomHeight = random.Next(3, MAP_SIZE_Y - 2);
                    int randomWidth = random.Next(3, MAP_SIZE_X - 2);

                    if (map[randomHeight][randomWidth] == FLOOR && randomHeight != MAP_SIZE_Y / 2 || randomWidth != MAP_SIZE_X / 2)
                    {
                        map[randomHeight][randomWidth] = WALL;
                        wallCount++;
                    }
                }
                wallCount = 0;

                // 랜덤 하트 생성
                int randomHeart = random.Next(1, 10);

                if (randomHeart > 5)
                {
                    while (true)
                    {
                        int HeartHeight = random.Next(2, MAP_SIZE_Y - 1);
                        int HeartWidth = random.Next(2, MAP_SIZE_X - 1);

                        if (map[HeartHeight][HeartWidth] == FLOOR && HeartWidth != playerPos.x || HeartHeight != playerPos.y)
                        {
                            map[HeartHeight][HeartWidth] = HEART;
                            break;
                        }
                    }
                }

                // 출구 생성
                while (true)
                {
                    int PortalHeight = random.Next(3, MAP_SIZE_Y - 2);
                    int PortalWidth = random.Next(3, MAP_SIZE_X - 2);

                    if (map[PortalHeight][PortalWidth] == FLOOR && PortalWidth < playerPos.x - 12 || PortalHeight < playerPos.y - 3 || PortalWidth > playerPos.x + 12 || PortalHeight > playerPos.y + 3)
                    {
                        map[PortalHeight][PortalWidth] = PORTAL;
                        break;
                    }
                }

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
                        else if (map[height][width] == FLOOR)
                        {
                            draw.Floor();
                        }
                        else if (map[height][width] == PORTAL)
                        {
                            draw.Portal();
                        }
                        else if (map[height][width] == HEART)
                        {
                            draw.HeartItem();
                        }
                    }
                    Console.WriteLine();
                }

                // 캐릭터 생성
                draw.MoveCursor(playerPos.x * 2, playerPos.y);
                draw.Player();

                // 플레이어, 적 이동
                while (true)
                {


                    // score 표시
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    draw.MoveCursor(0, 19);
                    Console.Write("[Score] {00}", score);
                    Console.ResetColor();




                    // 헌터 생성
                    if (stageturn > 16 - stage)
                    {
                        hunterMove++;

                        if (hunterMove % 10 == 0) // 10턴마다 생성
                        {
                            int enemyY = random.Next(2, MAP_SIZE_Y - 2);
                            int enemyX = random.Next(2, MAP_SIZE_X - 2);

                            if (map[enemyY][enemyX] == FLOOR && map[enemyY][enemyX] == map[playerPos.y][playerPos.x])
                            {
                                map[enemyY][enemyX] = ENEMY;
                                hunterPositions.Add(new Position(enemyX, enemyY)); // 적의 위치를 리스트에 추가
                            }
                        }
                    }
                    //// 그린 슬라임 생성
                    while (greenSlimeCount < stage * 4/2)
                    {
                        int slimeHeight = random.Next(4, MAP_SIZE_Y - 3);
                        int slimeWidth = random.Next(4, MAP_SIZE_X - 3);

                        if (map[slimeHeight][slimeWidth] == FLOOR && map[slimeHeight - 1][slimeWidth] == FLOOR && map[slimeHeight + 1][slimeWidth] == FLOOR)
                        {
                            map[slimeHeight][slimeWidth] = ENEMY;
                            greenSlimePositions.Add(new Position(slimeWidth, slimeHeight));
                            greenSlimeCount++;
                            break;
                        }
                    }
                    //// 블루 슬라임 생성
                    while (blueSlimeCount < stage * 3/2)
                    {
                        int slimeHeight = random.Next(4, MAP_SIZE_Y - 3);
                        int slimeWidth = random.Next(4, MAP_SIZE_X - 3);

                        if (map[slimeHeight][slimeWidth] == FLOOR && map[slimeHeight + 1][slimeWidth] == FLOOR && map[slimeHeight - 1][slimeWidth] == FLOOR && map[slimeHeight + 1][slimeWidth + 1] == FLOOR && map[slimeHeight][slimeWidth + 1] == FLOOR && map[slimeHeight][slimeWidth - 1] == FLOOR)
                        {
                            map[slimeHeight][slimeWidth] = ENEMY;
                            blueSlimePositions.Add(new Position(slimeWidth, slimeHeight));
                            blueSlimeCount++;
                            break;
                        }
                    }

                    // 헌터 이동
                    enemy.HunterMove(ref map, ref playerPos, ref hunterPositions, ref playerHP);

                    // Slime 출력
                    enemy.GreenSlimeMove(ref map, ref playerPos, ref greenSlimePositions, ref playerHP, greenSlimeMove);
                    enemy.BlueSlimeMove(ref map, ref playerPos, ref blueSlimePositions, ref playerHP, blueSlimeMove);

                    draw.PlayerHP(ref playerHP, ref playerMaxHP);
                    draw.PlayerGold(ref gold);

                    //게임 오버 판정
                    if (playerHP <= 0)
                    {
                        gameover = true;
                        break;
                    } // 게임오버시 while 탈출

                    // 스탑워치 시작
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    bool inputSuccess = false;

                    rhythmBar.Bar(0, 20);
                    Console.Beep(300, 100);

                    //입력대기
                    while (true)
                    {
                        draw.MoveCursor(32, 19);
                        Console.WriteLine("       ");
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
                                    if (map[playerPos.y - 1][playerPos.x] != WALL)
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
                                            map[playerPos.y][playerPos.x] = GOLD;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Gold(); // 적을 사망시킨 위치를 바닥으로 변경
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
                                    if (map[playerPos.y + 1][playerPos.x] != WALL)
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
                                            map[playerPos.y][playerPos.x] = GOLD;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Gold(); // 적을 사망시킨 위치를 바닥으로 변경
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
                                    if (map[playerPos.y][playerPos.x - 1] != WALL)
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
                                            map[playerPos.y][playerPos.x] = GOLD;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Gold(); // 적을 사망시킨 위치를 바닥으로 변경
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
                                    if (map[playerPos.y][playerPos.x + 1] != WALL)
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
                                            map[playerPos.y][playerPos.x] = GOLD;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Gold(); // 적을 사망시킨 위치를 바닥으로 변경
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

                                // 재시작
                                if (key.Key == ConsoleKey.R)
                                {
                                    hunterPositions = new List<Position>();
                                    hunterMove = default;
                                    stageturn = default;
                                    gameover = false;
                                    ClearCheck = false;
                                    retry = true;
                                    break;
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

                                inputSuccess = true;
                                break;
                            }
                        }
                        // 입력 if 종료
                    }
                    // 입력대기 while 종료

                    if (!inputSuccess)
                    {
                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                        draw.Player_Stop();
                        Console.Beep(150, 100);
                        draw.MoveCursor(32, 19);
                        Console.WriteLine("MISSED!");
                        score--;

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


                    if (map[playerPos.y][playerPos.x] == PORTAL) // 포탈을 탔을 때
                    {
                        stage++;
                        Console.Clear();
                        hunterPositions = new List<Position>();
                        ClearCheck = true;
                        clearevent.Run(ref gold, ref playerHP, ref playerMaxHP);
                        Console.Clear();
                        break;
                    }
                    if (map[playerPos.y][playerPos.x] == GOLD)
                    {
                        map[playerPos.y][playerPos.x] = FLOOR;
                        gold += 100;

                    }

                    //재시작
                    if (retry == true)
                    {
                        Console.Clear();
                        hunterPositions = new List<Position>();
                        break;

                    }

                    // 하트 먹으면 회복
                    if (map[playerPos.y][playerPos.x] == HEART)
                    {
                        map[playerPos.y][playerPos.x] = FLOOR;
                        playerHP += 1;
                        if (playerHP > playerMaxHP)
                        {
                            playerHP = playerMaxHP;
                        }
                    }
                    score++;
                    stageturn++; // 헌터 출현 턴 조정
                    hunterMove++;
                    greenSlimeMove++;
                    blueSlimeMove++;
                    if (score >= highscore)
                    {
                        highscore = score;
                    }
                    if (greenSlimeMove >= 4)
                    {
                        greenSlimeMove = 0;
                    }
                    if (blueSlimeMove >= 8)
                    {
                        blueSlimeMove = 0;
                    }
                }//플레이어, 적 이동 while

                

                //게임 오버 판정
                if (playerHP <= 0)
                {
                    draw.PlayerHP(ref playerHP, ref playerMaxHP);

                    draw.MoveCursor(playerPos.x * 2, playerPos.y);
                    draw.PlayerDead();
                    draw.DeadHeart(30, 20);
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

                // 보스스테이지 이동
                if (stage >= BOSS_STAGE)
                {
                    bossstage.Run(ref playerHP, ref playerMaxHP, ref playerAttack, ref gold, ref score);
                    break;
                }

            }
            // } while 종료 게임오버시 탈출


        }
        // } Run 종료



        public void Title()
        {
            image.TitleBox();
            image.TitleLogo(12, 4);

            timer = new System.Timers.Timer(1000); // 1초(1000ms) 간격으로 타이머 설정
            timer.Elapsed += TimerElapsed; // 타이머 이벤트 핸들러 등록
            timer.AutoReset = true; // 타이머를 반복적으로 실행할지 여부 설정
            timer.Start(); // 타이머 시작
            draw.MoveCursor(26, 16);
            Console.Write(" Press Any Key");
            Console.ReadKey();
            Console.Clear();
            timer.Stop(); // 타이머 정지
            timer.Dispose(); // 타이머 리소스 해제

        }
        public void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            draw.MoveCursor(26, 16);
            Console.Write("               ");
            Thread.Sleep(500);

            draw.MoveCursor(26, 16);
            Console.Write(" Press Any Key");
            //draw.MoveCursor(26, 16);
            //Console.Write(" Press Any Key");
        }


        public void Story()
        {
            Thread.Sleep(1);

            Console.Clear();
            image.HealBox();
            image.Synopsis(18, 2);

            draw.MoveCursor(3, 12);
            Console.Write(" 어느날 자신의 빚을 갚기 위해 악마와 계약한 노름꾼이 있었다.");
            Console.ReadKey();

            draw.MoveCursor(3, 14);
            Console.Write(" 그 계약은 바로 심장 박동에 맞춰 영원히 움직여야하는 저주.");
            Console.ReadKey();

            draw.MoveCursor(3, 16);
            Console.Write(" 노름꾼은 자기 심장을 잠재울 악마");
            draw.MoveCursor(38, 16);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Heart Breaker");
            Console.ResetColor();

            draw.MoveCursor(51, 16);
            Console.Write("를 찾아나선다...");



            Console.ReadKey();
            Console.Clear();

        }

    }
}
