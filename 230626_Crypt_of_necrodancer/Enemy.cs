using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    public class Enemy : GameInfo
    {
        



        // 상하 움직이는 그린슬라임
        public void GreenSlimeMove(ref int[][] map, Position playerPos, ref List<Position> enemyPositions, ref int playerHP)
        {
            Draw draw = new Draw();
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y;

                if (greenSlimeMove == 0)
                {
                    greenSlimeMove++;
                }

                else if (greenSlimeMove == 1)    
                {
                    greenSlimeMove++;
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY+1][enemyX] = ENEMY;
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
                        greenSlimeMove--;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.GreenSlime();

                }
                else if (greenSlimeMove == 2)
                {
                    greenSlimeMove++;
                }
                else if (greenSlimeMove == 3)  
                {
                    greenSlimeMove=0;
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
                        greenSlimeMove = 2;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.GreenSlime();

                }

            }
        }
        //상하좌우 움직이는 블루슬라임
        public void BlueSlimeMove(ref int[][] map, Position playerPos, ref List<Position> enemyPositions, ref int playerHP)
        {
            Draw draw = new Draw();
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y;

                if (blueSlimeMove == 0)
                {
                    blueSlimeMove++;
                }

                else if (blueSlimeMove == 1)
                {
                    blueSlimeMove++;
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
                        blueSlimeMove--;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }

                else if (blueSlimeMove == 2)
                {
                    blueSlimeMove++;
                }
                else if (blueSlimeMove == 3)
                {
                    blueSlimeMove++;
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
                        blueSlimeMove--;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                else if (blueSlimeMove == 4)
                {
                    blueSlimeMove++;
                }
                else if (blueSlimeMove == 5)
                {
                    blueSlimeMove++;
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
                        blueSlimeMove--;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                else if (blueSlimeMove == 6)
                {
                    blueSlimeMove++;
                }
                else if (blueSlimeMove == 7)
                {
                    blueSlimeMove++;
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
                        blueSlimeMove--;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                else if (blueSlimeMove == 8)
                {
                    blueSlimeMove=0;
                }

            }
                
        }

        // 플레이어를 쫓는 헌터
        public void HunterMove(ref int[][] map, Position playerPos, ref List<Position> enemyPositions, ref int playerHP)
        {
            Draw draw = new Draw();

            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y;

                // 적 이동 로직 (플레이어를 추적)
                if (enemyX < playerPos.x && map[enemyY][enemyX + 1] != WALL && map[enemyY][enemyX + 1] != PORTAL && map[enemyY][enemyX + 1] != ENEMY)
                {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY][enemyX + 1] = ENEMY;
                    draw.MoveCursor(enemyX * 2, enemyY);
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
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();

                }
                else if (enemyX > playerPos.x && map[enemyY][enemyX - 1] != WALL && map[enemyY][enemyX - 1] != PORTAL && map[enemyY][enemyX - 1] != ENEMY)
                {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY][enemyX - 1] = ENEMY;
                    draw.MoveCursor(enemyX * 2, enemyY);
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
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();

                }
                else if (enemyY < playerPos.y && map[enemyY + 1][enemyX] != WALL && map[enemyY + 1][enemyX] != PORTAL && map[enemyY + 1][enemyX] != ENEMY)
                {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY+1][enemyX] = ENEMY;
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Floor();
                    enemyPos.y++;
                    if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                    {
                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY-1][enemyX] = ENEMY;
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.PlayerHurt();
                        playerHP -= 1;
                        enemyPos.y--;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();
                }
                else if (enemyY > playerPos.y && map[enemyY - 1][enemyX] != WALL && map[enemyY - 1][enemyX] != PORTAL && map[enemyY - 1][enemyX] != ENEMY)
                {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY-1][enemyX] = ENEMY;
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Empty();
                    enemyPos.y--;
                    if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                    {
                        map[enemyY][enemyX] = FLOOR;
                        map[enemyY+1][enemyX] = ENEMY;
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.PlayerHurt();
                        playerHP -= 1;
                        enemyPos.y++;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();

                }
            }

        }
    }
}
