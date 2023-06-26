using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


namespace _230626_Crypt_of_necrodancer
{
    internal class MainGame
    {
        public static bool controlTiming = false;
        const int MAP_SIZE_X = 35;
        const int MAP_SIZE_Y = 18;

        const int FLOOR = 0;
        const int ENEMY = 2;
        const int WALL = 999;
        const int WALL_VALUE = 35;
        const int HEART_TIMING = 625;

        int wallCount = default;
        int[][] map = new int[MAP_SIZE_X][];
        int enemyMove = 3;
        int enemyLevel = 2;

        Random random = new Random();
        List<Position> enemyPositions = new List<Position>();
        Draw draw = new Draw();
        RhythmBar rhythmBar = new RhythmBar();




        public async void Run()
        {
            Position playerPos = new Position(MAP_SIZE_X / 2, MAP_SIZE_Y / 2);

            int gameoverCheck = default;

            while (gameoverCheck == 0) // 게임오버시 탈출
            {
                draw.MoveCursor(0, 0);
                // 맵 생성
                for (int height = 0; height < MAP_SIZE_Y; height++)
                {
                    map[height] = new int[MAP_SIZE_X];
                    for (int width = 0; width < MAP_SIZE_X; width++)
                    {
                        map[height][width] = WALL;
                    }
                }
                // 바닥 생성
                for (int height = 1; height < MAP_SIZE_Y - 1; height++)
                {
                    for (int width = 1; width < MAP_SIZE_X - 1; width++)
                    {
                        map[height][width] = FLOOR;
                    }
                }
                int roomSize = random.Next(5, 10); // 랜덤한 방 크기 (3부터 14까지)
                int startX = random.Next(1, MAP_SIZE_X - roomSize - 1); // 시작 X 좌표 (벽과 겹치지 않도록 범위 조절)
                int startY = random.Next(1, MAP_SIZE_Y - roomSize - 1); // 시작 Y 좌표 (벽과 겹치지 않도록 범위 조절)

                int doorSide = random.Next(4); // 문이 위치할 방의 한 변 (0: 위쪽, 1: 오른쪽, 2: 아래쪽, 3: 왼쪽)

                // 네모난 방 생성
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
                //// 장애물 생성
                while (wallCount < WALL_VALUE)
                {
                    int randomHeight = random.Next(2, MAP_SIZE_Y - 2);
                    int randomWidth = random.Next(2, MAP_SIZE_X - 2);

                    if (map[randomHeight][randomWidth] == FLOOR)
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
                        else if (map[height][width] == FLOOR)
                        {
                            draw.Floor();
                        }
                    }
                    Console.WriteLine();
                }
                // 캐릭터 생성
                draw.MoveCursor(playerPos.x * 2, playerPos.y);
                draw.Player();

                while (true)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    bool inputSuccess = false;

                    rhythmBar.Bar(0, 20);
                    Console.Beep(300, 100);

                    //입력대기
                    while (true)
                    {
                       
                        if (stopwatch.ElapsedMilliseconds >= HEART_TIMING+25)
                        {
                            break;
                        }

                        if (stopwatch.ElapsedMilliseconds >= HEART_TIMING-25 && stopwatch.ElapsedMilliseconds < HEART_TIMING+25)
                        {
                            if (Console.KeyAvailable)
                            {

                                ConsoleKeyInfo key = Console.ReadKey(true);

                                // 플레이어 ▲
                                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
                                {
                                    if (map[playerPos.y - 1][playerPos.x] == FLOOR)
                                    {
                                        playerPos.y--;
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player();
                                        draw.MoveCursor(playerPos.x * 2, (playerPos.y + 1));
                                        draw.Empty();
                                       
                                    }
                                }

                                // 플레이어 ▼
                                if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                                {
                                    if (map[playerPos.y + 1][playerPos.x] == FLOOR)
                                    {
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Empty();
                                        playerPos.y++;
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player();
                                       
                                    }
                                }

                                // 플레이어 ◀
                                if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                                {
                                    if (map[playerPos.y][playerPos.x - 1] == FLOOR)
                                    {
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Empty();
                                        playerPos.x--;
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player();
                                        
                                    }
                                }

                                // 플레이어 ▶
                                if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                                {
                                    if (map[playerPos.y][playerPos.x + 1] == FLOOR)
                                    {
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Empty();
                                        playerPos.x++;
                                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                        draw.Player();
                                       
                                    }
                                }

                                //debug
                                draw.MoveCursor(72, 0);
                                Console.WriteLine("[DEBUG]");
                                draw.MoveCursor(72, 1);
                                Console.WriteLine("Timing : {0}ms", stopwatch.ElapsedMilliseconds- HEART_TIMING);
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
                        draw.RedPlayer();
                        draw.MoveCursor(72, 2);
                        Console.WriteLine("Last Timing : {0}", stopwatch.ElapsedMilliseconds);
                        draw.MoveCursor(72, 2);
                        Console.Beep(150, 100);
                    }


                    // 입력 성공 후 입력 버퍼 비우기
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }

                    // 적 무브
                    MoveEnemy(ref map, playerPos, ref enemyPositions, ref enemyLevel);

                    enemyMove++;
                    enemyLevel++;

                    ////적 생성
                    if (enemyMove % 3 == 0) // 3턴마다 적 생성
                    {
                        int enemyY = random.Next(2, MAP_SIZE_Y - 2);
                        int enemyX = random.Next(2, MAP_SIZE_X - 2);

                        if (map[enemyY][enemyX] == FLOOR)
                        {
                            map[enemyY][enemyX] = ENEMY;
                            enemyPositions.Add(new Position(enemyX, enemyY)); // 적의 위치를 리스트에 추가
                        }
                    }
                    // 적 출력
                    foreach (var enemy in enemyPositions)
                    {
                        draw.MoveCursor(enemy.x * 2, enemy.y);
                        draw.Enemy();

                        // 게임오버 조건
                        if (playerPos.x == enemy.x && playerPos.y == enemy.y)
                        {
                            gameoverCheck = 1;
                            break;
                        }
                    }

                    //게임 오버 판정
                    if (gameoverCheck == 1)
                    {
                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                        draw.PlayerDead();
                        Thread.Sleep(1000);
                        Console.Clear();
                        draw.GAMEOVER();
                        Thread.Sleep(1000);
                        Console.Clear();
                        enemyPositions = new List<Position>();
                        break;
                    } // 게임오버시 while 탈출


                }//적 플레이어 이동 while

            }
            // } while 종료 게임오버시 탈출
            gameoverCheck = default;
        }
        // } Run 종료

        public void MoveEnemy(ref int[][] map, Position playerPos, ref List<Position> enemyPositions, ref int enemyLevel)
        {
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y;

                // 적 이동 로직 (플레이어를 추적)
                if (enemyX < playerPos.x && map[enemyY][enemyX + 1] == FLOOR)
                {
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Empty();
                    enemyPos.x++;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Enemy();
                }
                else if (enemyX > playerPos.x && map[enemyY][enemyX - 1] == FLOOR)
                {
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Empty();
                    enemyPos.x--;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Enemy();
                }
                else if (enemyY < playerPos.y && map[enemyY + 1][enemyX] == FLOOR)
                {
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Empty();
                    enemyPos.y++;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Enemy();
                }
                else if (enemyY > playerPos.y && map[enemyY - 1][enemyX] == FLOOR)
                {
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Empty();
                    enemyPos.y--;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Enemy();
                }
            }

        }
       

    }
}
