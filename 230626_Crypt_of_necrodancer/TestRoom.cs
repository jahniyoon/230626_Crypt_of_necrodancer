﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    internal class TestRoom : GameInfo
    {
        public static bool controlTiming = false;


        int[][] map = new int[MAP_SIZE_X][];

        Draw draw = new Draw();
        Enemy enemy = new Enemy();
        RhythmBar rhythmBar = new RhythmBar();
        Image image = new Image();





        public void Run()
        {
            ClearCheck = false;
            playerHP = 3;
            hunterMove = 3;
            greenSlimeMove = default;
            blueSlimeMove = default;

            while (playerHP > 0) // 게임오버시 탈출
            {
                Position playerPos = new Position(MAP_SIZE_X / 2, MAP_SIZE_Y / 2);


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
                // 헌터 가두는 벽
                //map[3][4] = WALL;
                //map[4][3] = WALL;


                //// 그린 슬라임 생성
                while (greenSlimeCount == 0)
                {
                    int slimeHeight = random.Next(3, MAP_SIZE_Y - 2);
                    int slimeWidth = random.Next(3, MAP_SIZE_X - 2);

                    if (map[slimeHeight][slimeWidth] == FLOOR && map[slimeHeight + 1][slimeWidth] == FLOOR)
                    {
                        map[slimeHeight][slimeWidth] = ENEMY;
                        greenSlimePositions.Add(new Position(slimeWidth, slimeHeight));
                        greenSlimeCount++;
                    }
                }
                //// 블루 슬라임 생성
                while (blueSlimeCount == 0)
                {
                    int slimeHeight = random.Next(3, MAP_SIZE_Y - 2);
                    int slimeWidth = random.Next(3, MAP_SIZE_X - 2);

                    if (map[slimeHeight][slimeWidth] == FLOOR && map[slimeHeight + 1][slimeWidth] == FLOOR && map[slimeHeight][slimeWidth + 1] == FLOOR && map[slimeHeight + 1][slimeWidth + 1] == FLOOR)
                    {
                        map[slimeHeight][slimeWidth] = ENEMY;
                        blueSlimePositions.Add(new Position(slimeWidth, slimeHeight));
                        blueSlimeCount++;
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
                        else if (map[height][width] == FLOOR|| map[height][width] == ENEMY)
                        {
                            draw.Floor();
                        }
                        
                    }
                    Console.WriteLine();
                }

                // 캐릭터 생성
                draw.MoveCursor(playerPos.x * 2, playerPos.y);
                draw.Player();
                draw.MoveCursor(30, 0);
                Console.Write("[TEST ROOM]");

                // 플레이어, 적 이동
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    draw.MoveCursor(0, 19);
                    Console.Write("[Score] {00}", score);
                    Console.ResetColor();

                    score++;
                    greenSlimeMove++;
                    blueSlimeMove++;


                    // 헌터 이동
                    enemy.HunterMove(ref map, playerPos, ref hunterPositions, ref playerHP);
                    hunterMove++;
                    //if (hunterMove == 4)
                    //{
                    //    hunterPositions.Add(new Position(3, 3));
                    //    map[3][3] = ENEMY;
                    //}

                    // 헌터 출력
                    foreach (var enemy in hunterPositions)
                    {
                        draw.MoveCursor(enemy.x * 2, enemy.y);
                        draw.Hunter();
                    }
                    draw.PlayerHP(ref playerHP, ref playerMaxHP);


                    // Slime 출력
                    enemy.GreenSlimeMove(ref map, playerPos, ref greenSlimePositions, ref playerHP, ref greenSlimeMove);
                    enemy.BlueSlimeMove(ref map, playerPos, ref blueSlimePositions, ref playerHP, ref blueSlimeMove);

                    

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
                                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                            draw.Floor(); // 적을 사망시킨 위치를 바닥으로 변경
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
                                        draw.Gold();
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


                    //게임 오버 판정
                    if (playerHP <= 0)
                    {
                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                        draw.PlayerDead();
                        Thread.Sleep(1000);
                        Console.Clear();
                        image.GAMEOVER();
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
