﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


namespace _230626_Crypt_of_necrodancer
{
    internal class MainGame : GameInfo
    {
        public static bool controlTiming = false;


        int[][] map = new int[MAP_SIZE_X][];
        int wallCount = default;  // 벽 개수

        Draw draw = new Draw();
        Enemy enemy = new Enemy();
        RhythmBar rhythmBar = new RhythmBar();





        public void Run()
        {
            ClearCheck = false;
            playerHP = 3;
            hunterMove = 3;
            stage = 1;
            score = 0;

            


            while (playerHP > 0) // 게임오버시 탈출
            {
                Position playerPos = new Position(MAP_SIZE_X / 2, MAP_SIZE_Y / 2);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                draw.MoveCursor(0, 19);
                Console.Write("Score{00}", score);
                draw.MoveCursor(64, 19);
                Console.Write("Stage{00}", stage);
                Console.ResetColor();


                draw.MoveCursor(0, 0);
                // 맵 생성
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

                // 출구 생성

                int PortalHeight = random.Next(3, MAP_SIZE_Y - 2);
                int PortalWidth = random.Next(3, MAP_SIZE_X - 2);

                if (map[PortalHeight][PortalWidth] == FLOOR)
                {
                    map[PortalHeight][PortalWidth] = PORTAL;
                }

                // 네모난 방 생성
                int roomSize = random.Next(5, 10); // 랜덤한 방 크기 (3부터 14까지)
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
               
                // 장애물 생성
                while (wallCount < WALL_VALUE)
                {
                    int randomHeight = random.Next(3, MAP_SIZE_Y - 2);
                    int randomWidth = random.Next(3, MAP_SIZE_X - 2);

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
                    }
                    Console.WriteLine();
                }

                // 캐릭터 생성
                draw.MoveCursor(playerPos.x * 2, playerPos.y);
                draw.Player();

                // 플레이어, 적 이동
                while (true)
                {


                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    draw.MoveCursor(0, 19);
                    Console.Write("Score{00}", score);
                    Console.ResetColor();

                    // 헌터 이동
                    enemy.HunterMove(ref map, playerPos, ref hunterPositions, ref playerHP);
                    hunterMove++;
                    score++;


                    // 헌터 생성
                    if (hunterMove % 3 == 0) // 3턴마다 적 생성
                    {
                        int enemyY = random.Next(2, MAP_SIZE_Y - 2);
                        int enemyX = random.Next(2, MAP_SIZE_X - 2);

                        if (map[enemyY][enemyX] == FLOOR)
                        {
                            map[enemyY][enemyX] = ENEMY;
                            hunterPositions.Add(new Position(enemyX, enemyY)); // 적의 위치를 리스트에 추가
                        }
                    }
                   

                    // 적 출력
                    foreach (var enemy in hunterPositions)
                    {
                        draw.MoveCursor(enemy.x * 2, enemy.y);
                        draw.Enemy();
                    }
                    draw.PlayerHP(ref playerHP);


                    // 스탑워치 시작
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    bool inputSuccess = false;

                    rhythmBar.Bar(0, 20);
                    Console.Beep(300, 100);

                    //입력대기
                    while (true)
                    {
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

                                        if (enemyToRemove != null)
                                        {
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Floor(); // 적을 사망시킨 위치를 바닥으로 변경
                                            playerPos.y++;
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Player_Up();
                                            hunterPositions.Remove(enemyToRemove);
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
                                }

                                //debug
                                if (DEBUG_MODE == true)
                                {
                                    draw.MoveCursor(72, 0);
                                    Console.WriteLine("[DEBUG]");
                                    draw.MoveCursor(72, 1);
                                    Console.WriteLine("Timing : {0}ms", stopwatch.ElapsedMilliseconds - HEART_TIMING);
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

                        //debug
                        if (DEBUG_MODE == true)
                        {
                            draw.MoveCursor(72, 2);
                            Console.WriteLine("Last Timing : {0}", stopwatch.ElapsedMilliseconds);
                        }
                    }

                    // 입력 성공 후 입력 버퍼 비우기
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }


                    if (map[playerPos.y][playerPos.x] == PORTAL)
                    {
                        stage++;
                        Console.Clear();
                        hunterPositions = new List<Position>();
                        ClearCheck = true;
                        break;
                    }
                    
                    //게임 오버 판정
                    if (playerHP <= 0)
                    {
                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                        draw.PlayerDead();
                        Thread.Sleep(1000);
                        Console.Clear();
                        draw.GAMEOVER();
                        Thread.Sleep(1000);
                        Console.Clear();
                        hunterPositions = new List<Position>();
                        break;
                    } // 게임오버시 while 탈출


                }//플레이어, 적 이동 while

            }
            // } while 종료 게임오버시 탈출
        }
        // } Run 종료




    }
}
