using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    internal class MainGame
    {
        public static bool controlTiming = false;
        const int MAP_SIZE_X = 32;
        const int MAP_SIZE_Y = 18;

        const int FLOOR = 0;
        const int ENEMY = 2;
        const int WALL = 999;
        const int WALL_VALUE = 30;

        int wallCount = default;
        int[][] map = new int[MAP_SIZE_X][];
        int enemyMove = 3;
        int enemyLevel = 2;

        Random random = new Random();
        Draw draw = new Draw();
        Position playerPos = new Position(MAP_SIZE_X / 2, MAP_SIZE_Y / 2);
        List<Position> enemyPositions = new List<Position>();
        RhythmBar rhythmBar = new RhythmBar();

        public static System.Timers.Timer timer;

        public async void Run()
        {

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
                // 장애물 생성
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

                    rhythmBar.Bar(0, 19);

                    // 적 무브
                    MoveEnemy(ref map, playerPos, ref enemyPositions, ref enemyLevel);
                    enemyMove++;
                    enemyLevel++;
                    Console.Beep(300, 100);

                    //적 생성
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
                            Thread.Sleep(500);
                            Console.Clear();
                            draw.GAMEOVER();
                            Console.ReadKey();
                            Console.Clear();
                            enemyPositions = new List<Position>();
                            gameoverCheck = 1;
                            break;
                        }
                    }
                    if (gameoverCheck == 1) { break; } // 게임오버시 while 탈출






                    if (Console.KeyAvailable)
                    {
                        var key = await WaitForSingleKey();

                        // 플레이어 ▲
                        if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
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
                        if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
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
                        if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
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
                        if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
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
                    }

                }//플레이어 컨트롤



            }
            // } while 종료 게임오버시 탈출
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
        //  MoveEnemy 종료

     

        static async Task<ConsoleKey?> WaitForSingleKey()
        {
            var startTime = DateTime.Now;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    return keyInfo.Key;
                }

                var currentTime = DateTime.Now;
                var elapsedTime = currentTime - startTime;

                if (elapsedTime.TotalMilliseconds >= 900 && elapsedTime.TotalMilliseconds <= 1000)
                {
                    // 대기 시간 범위 내에 키 입력이 없으면 null을 반환하도록 처리
                    return null;
                }

                await Task.Delay(100); // 100ms 대기
            }

        }

    }
}
