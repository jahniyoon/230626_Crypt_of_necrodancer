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
        public void GreenSlimeMove(ref int[][] map, Position playerPos, ref List<Position> enemyPositions, ref int playerHP, ref int slimeCount)
        {
            Draw draw = new Draw();
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y; 
                
                if (slimeCount == 1)    
                {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY+1][enemyX] = ENEMY;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.GreenSlime();
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Floor();
                    enemyPos.y++;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.GreenSlime();
                }
                if (slimeCount == 3)  
                {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY -1][enemyX] = ENEMY;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.GreenSlime();
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Floor();
                    enemyPos.y--;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.GreenSlime();
                }
                if (slimeCount == 4)   
                {
                    slimeCount = 0;
                }
            }
        }
        //상하좌우 움직이는 블루슬라임
        public void BlueSlimeMove(ref int[][] map, Position playerPos, ref List<Position> enemyPositions, ref int playerHP, ref int slimeCount)
        {
            Draw draw = new Draw();
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y;

                if (slimeCount == 1)
                {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY + 1][enemyX] = ENEMY;

                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Floor();
                    enemyPos.y++;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                if (slimeCount == 3)
                {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY][enemyX+1] = ENEMY;

                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Floor();
                    enemyPos.x++;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                if (slimeCount == 5)
                {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY - 1][enemyX] = ENEMY;

                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Floor();
                    enemyPos.y--;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                if (slimeCount == 7)
                {
                    map[enemyY][enemyX] = FLOOR;
                    map[enemyY][enemyX - 1] = ENEMY;

                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Floor();
                    enemyPos.x--;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.BlueSlime();
                }
                if (slimeCount == 8)
                {
                    slimeCount = 0;
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
                if (enemyX < playerPos.x && map[enemyY][enemyX + 1] != WALL && map[enemyY][enemyX + 1] != PORTAL)
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
                    map[enemyY][enemyX] = ENEMY;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();

                }
                else if (enemyX > playerPos.x && map[enemyY][enemyX - 1] != WALL && map[enemyY][enemyX - 1] != PORTAL)
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
                    map[enemyY][enemyX] = ENEMY;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();

                }
                else if (enemyY < playerPos.y && map[enemyY + 1][enemyX] != WALL && map[enemyY + 1][enemyX] != PORTAL)
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
                    map[enemyY][enemyX] = ENEMY;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();
                }
                else if (enemyY > playerPos.y && map[enemyY - 1][enemyX] != WALL && map[enemyY - 1][enemyX] != PORTAL)
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
                    map[enemyY][enemyX] = ENEMY;
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Hunter();

                }
            }

        }
    }
}
