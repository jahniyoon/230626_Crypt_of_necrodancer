using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    public class Enemy : GameInfo
    {
        
        public void Boss(ref int[][] map, ref Position playerPos, ref Position BossPos, ref int playerHP, ref int bossHP)
        {

            Draw draw = new Draw();
            draw.MoveCursor(BossPos.x * 2, BossPos.y);
            if (bossHP < 15)
            {
                draw.BossHarf(BossPos.x * 2, BossPos.y);
            }
            else
                draw.Boss(BossPos.x * 2, BossPos.y);


            if (bossTurn % 2 == 0)
            {

                // 보스 우측 이동 로직 (플레이어를 추적)
                if (BossPos.x < playerPos.x && map[BossPos.y][BossPos.x + 2] == FLOOR)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            map[BossPos.y - 1 + i][BossPos.x - 1 + j] = FLOOR;
                        }
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            map[BossPos.y - 1 + i][BossPos.x + j] = BOSS;
                            if (map[BossPos.y - 1 + i][BossPos.x + j] == map[playerPos.y][playerPos.x])
                            {
                                playerPos.x++;
                                if (playerPos.x >= MAP_SIZE_X-1)
                                {
                                    playerHP = 0;
                                }
                                else
                                {
                                    draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                    playerHP--;
                                    draw.PlayerHurt();
                                }
                            }
                        }
                    }
                    draw.MoveCursor(BossPos.x * 2, BossPos.y);
                    draw.BossFloor(BossPos.x * 2, BossPos.y);
                    BossPos.x++;

                    draw.MoveCursor(BossPos.x * 2, BossPos.y);
                    if (bossHP < 15)
                    {
                        draw.BossHarf(BossPos.x * 2, BossPos.y);
                    }
                    else
                    draw.Boss(BossPos.x * 2, BossPos.y);
                }
                // 보스 좌측 이동 로직 (플레이어를 추적)
                else if (BossPos.x > playerPos.x && map[BossPos.y][BossPos.x - 2] == FLOOR)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            map[BossPos.y - 1 + i][BossPos.x - 1 + j] = FLOOR;
                        }
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            map[BossPos.y - 1 + i][BossPos.x - 2 + j] = BOSS;
                            if (map[BossPos.y - 1 + i][BossPos.x - 2 + j] == map[playerPos.y][playerPos.x])
                            {
                                playerPos.x--;
                                if (playerPos.x <= 0)
                                {
                                    playerHP = 0;
                                }
                                else
                                {
                                    draw.MoveCursor(playerPos.x * 2, playerPos.y);
                                    playerHP--;
                                    draw.PlayerHurt();
                                }
                            }


                        }
                    }
                    draw.MoveCursor(BossPos.x * 2, BossPos.y);
                    draw.BossFloor(BossPos.x * 2, BossPos.y);
                    BossPos.x--;

                    draw.MoveCursor(BossPos.x * 2, BossPos.y);
                    if (bossHP < 15)
                    {
                        draw.BossHarf(BossPos.x * 2, BossPos.y);
                    }
                    else
                        draw.Boss(BossPos.x * 2, BossPos.y);
                }

                //공격
                else if (BossPos.x == playerPos.x && bossTurn != 0)
                {
                    for (int i = 1; bossAttack < 1; i++)
                    {
                        draw.MoveCursor(BossPos.x * 2, BossPos.y + i);
                        draw.BossAttack();

                        if (map[BossPos.y + i + 1][BossPos.x] != FLOOR)
                        {
                            bossAttack = 1;
                        }
                        else if (BossPos.y + i + 1 ==playerPos.y)
                        {
                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                            draw.BossAttack();
                            playerHP -= 2;
                            bossAttack = 1;

                        }

                    }
                    bossAttack = 0;

                    Thread.Sleep(100);

                    for (int i = 1; bossAttack < 1; i++)
                    {
                        draw.MoveCursor(BossPos.x * 2, BossPos.y + i);
                        draw.Floor();

                        if (map[BossPos.y + i + 1][BossPos.x] != FLOOR)
                        {
                            bossAttack = 1;
                        }
                        else if (BossPos.y + i + 1 == playerPos.y)
                        {
                            draw.MoveCursor(playerPos.x * 2, playerPos.y);
                            draw.PlayerHurt();
                            bossAttack = 1;

                        }

                    }

                    bossAttack = 0;
                    draw.MoveCursor(BossPos.x * 2, BossPos.y);
                    if (bossHP < 15)
                    {
                        draw.BossHarf(BossPos.x * 2, BossPos.y);
                    }
                    else
                        draw.Boss(BossPos.x * 2, BossPos.y);
                }
            }
            bossTurn++;

        }

    



        // 상하 움직이는 그린슬라임
        public void GreenSlimeMove(ref int[][] map, ref Position playerPos, ref List<Position> enemyPositions, ref int playerHP, int greenSlimeMove)
        {
            Draw draw = new Draw();
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y;

                if (greenSlimeMove == 0)
                {
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.GreenSlime();
                }

                else if (greenSlimeMove == 1)    
                {
                    if (map[enemyY + 1][enemyX] != WALL|| map[enemyY + 1][enemyX] != ENEMY|| map[enemyY + 1][enemyX] != PORTAL)
                    {
                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY + 1][enemyX] = ENEMY;
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.Floor();
                        enemyPos.y++;
                        if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                        {
                            map[enemyY][enemyX] = FLOOR;
                            map[enemyY - 1][enemyX] = ENEMY;
                            draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                            draw.PlayerHurt();
                            playerHP -= 1;
                            enemyPos.y--;
                        }
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.GreenSlime();

                }
                else if (greenSlimeMove == 2)
                {
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.GreenSlime();
                }
                else if (greenSlimeMove == 3)  
                {
                    if (map[enemyY - 1][enemyX] != WALL || map[enemyY - 1][enemyX] != ENEMY || map[enemyY - 1][enemyX] != PORTAL)
                    {
                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY - 1][enemyX] = ENEMY;
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.Floor();
                        enemyPos.y--;
                        if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                        {
                            map[enemyY][enemyX] = FLOOR;
                            map[enemyY + 1][enemyX] = ENEMY;
                            draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                            draw.PlayerHurt();
                            playerHP -= 1;
                            enemyPos.y++;
                        }
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.GreenSlime();

                }

            }
        }
        //상하좌우 움직이는 블루슬라임
        public void BlueSlimeMove(ref int[][] map, ref Position playerPos, ref List<Position> enemyPositions, ref int playerHP, int blueSlimeMove)
        {
            Draw draw = new Draw();
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y;

                if (blueSlimeMove == 0)
                {
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }

                else if (blueSlimeMove == 1)
                {
                    if (map[enemyY + 1][enemyX] == FLOOR)
                    {

                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY + 1][enemyX] = ENEMY;
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.Floor();
                        enemyPos.y++;
                        if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                        {
                            map[enemyY][enemyX] = FLOOR;
                            map[enemyY - 1][enemyX] = ENEMY;
                            draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                            draw.PlayerHurt();
                            playerHP -= 1;
                            enemyPos.y--;
                        }
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }

                else if (blueSlimeMove == 2)
                {
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                else if (blueSlimeMove == 3)
                {
                    if (map[enemyY][enemyX + 1] == FLOOR)
                    {

                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY][enemyX + 1] = ENEMY;
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.Floor();
                        enemyPos.x++;
                        if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                        {
                            map[enemyY][enemyX] = FLOOR;
                            map[enemyY][enemyX - 1] = ENEMY;
                            draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                            draw.PlayerHurt();
                            playerHP -= 1;
                            enemyPos.x--;
                        }
                    }

                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                else if (blueSlimeMove == 4)
                {
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                else if (blueSlimeMove == 5)
                {
                    if (map[enemyY - 1][enemyX] == FLOOR)
                    {
                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY - 1][enemyX] = ENEMY;
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.Floor();
                        enemyPos.y--;
                        if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                        {
                            map[enemyY][enemyX] = FLOOR;
                            map[enemyY + 1][enemyX] = ENEMY;
                            draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                            draw.PlayerHurt();
                            playerHP -= 1;
                            enemyPos.y++;
                        }
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                else if (blueSlimeMove == 6)
                {
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                else if (blueSlimeMove == 7)
                {
                    if (map[enemyY][enemyX - 1] == FLOOR)
                    {
                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY][enemyX - 1] = ENEMY;
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.Floor();
                        enemyPos.x--;
                        if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                        {
                            map[enemyY][enemyX] = FLOOR;
                            map[enemyY][enemyX + 1] = ENEMY;
                            draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                            draw.PlayerHurt();
                            playerHP -= 1;
                            enemyPos.x++;
                        }
                    }

                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                

            }
                
        }
        public void HunterMove(ref int[][] map, ref Position playerPos, ref List<Position> enemyPositions, ref int playerHP)
        {
            Draw draw = new Draw();

            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y;

                // 적 이동 로직 (플레이어를 추적)
                if (enemyY < playerPos.y && map[enemyY + 1][enemyX] == FLOOR || map[enemyY + 1][enemyX] == GOLD || map[enemyY + 1][enemyX] == HEART)
                {
                    if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y + 1)
                    {
                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                        draw.PlayerHurt();
                        playerHP -= 1;
                    }

                    else
                    {
                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY + 1][enemyX] = ENEMY;
                        draw.MoveCursor(enemyX * 2, enemyY);
                        draw.Floor();
                        enemyPos.y++;
                    }

                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();
                }
                else if (enemyY > playerPos.y && map[enemyY - 1][enemyX] == FLOOR || map[enemyY - 1][enemyX] == GOLD || map[enemyY - 1][enemyX] == HEART)
                {
                    if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y - 1)
                    {
                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                        draw.PlayerHurt();
                        playerHP -= 1;
                    }

                    else
                    {
                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY - 1][enemyX] = ENEMY;
                        draw.MoveCursor(enemyX * 2, enemyY);
                        draw.Floor();
                        enemyPos.y--;
                    }
                    
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();

                }

                else if (enemyX < playerPos.x && map[enemyY][enemyX + 1] == FLOOR || map[enemyY][enemyX + 1] == GOLD || map[enemyY][enemyX + 1] == HEART)
                {
                    if (playerPos.x == enemyPos.x + 1 && playerPos.y == enemyPos.y)
                    {
                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                        draw.PlayerHurt();
                        playerHP -= 1;
                    }

                    else
                    {
                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY][enemyX + 1] = ENEMY;
                        draw.MoveCursor(enemyX * 2, enemyY);
                        draw.Floor();
                        enemyPos.x++;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();

                }
                else if (enemyX > playerPos.x && map[enemyY][enemyX - 1] == FLOOR || map[enemyY][enemyX - 1] == GOLD || map[enemyY][enemyX - 1] == HEART)
                {
                    if (playerPos.x == enemyPos.x - 1 && playerPos.y == enemyPos.y)
                    {
                        draw.MoveCursor(playerPos.x * 2, playerPos.y);
                        draw.PlayerHurt();
                        playerHP -= 1;
                    }
                    else
                    {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY][enemyX - 1] = ENEMY;
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Floor();
                    enemyPos.x--;
                    }


                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();

                }
            }

        }

        //// 플레이어를 쫓는 헌터
        //public void HunterMove(ref int[][] map, ref Position playerPos, ref List<Position> enemyPositions, ref int playerHP)
        //{
        //    Draw draw = new Draw();

        //    for (int i = 0; i < enemyPositions.Count; i++)
        //    {
        //        Position enemyPos = enemyPositions[i];

        //        int enemyX = enemyPos.x;
        //        int enemyY = enemyPos.y;

        //        // 적 이동 로직 (플레이어를 추적)
        //         if (enemyY < playerPos.y && map[enemyY + 1][enemyX] == FLOOR)
        //        {

        //            map[enemyY][enemyX] = FLOOR;
        //            map[enemyY + 1][enemyX] = ENEMY;
        //            draw.MoveCursor(enemyX * 2, enemyY);
        //            draw.Floor();
        //            enemyPos.y++;
        //            if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
        //            {
        //                map[enemyY][enemyX] = FLOOR;
        //                map[enemyY - 1][enemyX] = ENEMY;
        //                draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
        //                draw.PlayerHurt();
        //                playerHP -= 1;
        //                enemyPos.y--;
        //            }
        //            draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
        //            draw.Hunter();
        //        }
        //        else if (enemyY > playerPos.y && map[enemyY - 1][enemyX] == FLOOR)
        //        {
        //            map[enemyY][enemyX] = FLOOR;
        //            map[enemyY - 1][enemyX] = ENEMY;
        //            draw.MoveCursor(enemyX * 2, enemyY);
        //            draw.Floor();
        //            enemyPos.y--;
        //            if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
        //            {
        //                map[enemyY][enemyX] = FLOOR;
        //                map[enemyY + 1][enemyX] = ENEMY;
        //                draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
        //                draw.PlayerHurt();
        //                playerHP -= 1;
        //                enemyPos.y++;
        //            }
        //            draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
        //            draw.Hunter();

        //        }

        //        else if (enemyX < playerPos.x && map[enemyY][enemyX + 1] == FLOOR)
        //        {
        //            map[enemyY][enemyX] = FLOOR;
        //            map[enemyY][enemyX + 1] = ENEMY;
        //            draw.MoveCursor(enemyX * 2, enemyY);
        //            draw.Floor();
        //            enemyPos.x++;

        //            if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
        //            {
        //                map[enemyY][enemyX] = FLOOR;
        //                map[enemyY][enemyX - 1] = ENEMY;
        //                draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
        //                draw.PlayerHurt();
        //                playerHP -= 1;
        //                enemyPos.x--;
        //            }
        //            draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
        //            draw.Hunter();

        //        }
        //        else if (enemyX > playerPos.x && map[enemyY][enemyX - 1] == FLOOR)
        //        {
        //            map[enemyY][enemyX] = FLOOR;
        //            map[enemyY][enemyX - 1] = ENEMY;
        //            draw.MoveCursor(enemyX * 2, enemyY);
        //            draw.Floor();
        //            enemyPos.x--;
        //            if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
        //            {
        //                map[enemyY][enemyX] = FLOOR;
        //                map[enemyY][enemyX + 1] = ENEMY;
        //                draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
        //                draw.PlayerHurt();
        //                playerHP -= 1;
        //                enemyPos.x++;
        //            }
        //            draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
        //            draw.Hunter();

        //        }
        //    }

        //}

    }
}
