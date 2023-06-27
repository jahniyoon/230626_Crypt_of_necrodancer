using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    public class Enemy : GameInfo
    {


        public void SlimeMove(ref int[][] map, Position playerPos, ref List<Position> enemyPositions, ref int playerHP, ref int slimeCount)
        {
            Draw draw = new Draw();
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y;

                if (slimeCount == 1)    // 작업중
                {
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Floor();
                    enemyPos.y++;
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Slime();
                }
                //if (slimeCount == 2)
                //{
                //    draw.MoveCursor(enemyX * 2, enemyY);
                //    draw.Floor();
                //    enemyPos.x++;
                //    draw.MoveCursor(enemyX * 2, enemyY);
                //    draw.Slime();
                //}
                //if (slimeCount == 3)
                //{
                //    draw.MoveCursor(enemyX * 2, enemyY);
                //    draw.Floor();
                //    enemyPos.y--;
                //    draw.MoveCursor(enemyX * 2, enemyY);
                //    draw.Slime();
                //}
                //if (slimeCount == 4)
                //{
                //    draw.MoveCursor(enemyX * 2, enemyY);
                //    draw.Floor();
                //    enemyPos.x--;
                //    draw.MoveCursor(enemyX * 2, enemyY);
                //    draw.Slime();
                //    slimeCount = 0;
                //}
            }
        }


        public void HunterMove(ref int[][] map, Position playerPos, ref List<Position> enemyPositions, ref int playerHP)
        {
            Draw draw = new Draw();

            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Position enemyPos = enemyPositions[i];

                int enemyX = enemyPos.x;
                int enemyY = enemyPos.y;

                // 적 이동 로직 (플레이어를 추적)
                if (enemyX < playerPos.x && map[enemyY][enemyX + 1] == FLOOR)
                {
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Floor();
                    enemyPos.x++;

                    if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                    {
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.PlayerHurt();
                        playerHP -= 1;
                        enemyPos.x--;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Enemy();

                }
                else if (enemyX > playerPos.x && map[enemyY][enemyX - 1] == FLOOR)
                {
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Floor();
                    enemyPos.x--;
                    if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                    {
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.PlayerHurt();
                        playerHP -= 1;
                        enemyPos.x++;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Enemy();

                }
                else if (enemyY < playerPos.y && map[enemyY + 1][enemyX] == FLOOR)
                {
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Floor();
                    enemyPos.y++;
                    if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                    {
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.PlayerHurt();
                        playerHP -= 1;
                        enemyPos.y--;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Enemy();
                }
                else if (enemyY > playerPos.y && map[enemyY - 1][enemyX] == FLOOR)
                {
                    draw.MoveCursor(enemyX * 2, enemyY);
                    draw.Empty();
                    enemyPos.y--;
                    if (playerPos.x == enemyPos.x && playerPos.y == enemyPos.y)
                    {
                        draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                        draw.PlayerHurt();
                        playerHP -= 1;
                        enemyPos.y++;
                    }
                    draw.MoveCursor(enemyPos.x * 2, enemyPos.y);
                    draw.Enemy();

                }
            }

        }
    }
}
