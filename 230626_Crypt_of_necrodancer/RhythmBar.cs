using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    internal class RhythmBar
    {
        int[][] bar = new int[MAX_WIDTH][];
        const int EMPTY = 0;
        const int LEFT_STICK = 1;
        const int RIGHT_STICK = 2;
        const int MAX_HEIGHT = 3;
        const int MAX_WIDTH = 32;
        const int TEMPO = 28;
        int barCount = default;
        Draw draw = new Draw();


        public void Bar(int left, int top)
        {
            draw.MoveCursor(left, top);

            Console.WriteLine("이곳에 출력");

            List<Position> leftPositions = new List<Position>();
            List<Position> rightPositions = new List<Position>();


            Init(ref leftPositions, ref rightPositions);

            draw.MoveCursor(left, top);

            // 타이밍 표 출력
            for (int height = 0; height < MAX_HEIGHT; height++)
            {
                for (int width = 0; width < MAX_WIDTH; width++)
                {
                    if (bar[height][width] == EMPTY)
                    {
                        draw.Empty();
                    }
                    if (bar[height][width] == LEFT_STICK || bar[height][width] == RIGHT_STICK)
                    {
                        draw.Stick();
                    }
                }
                Console.WriteLine();
            }
           
                while (barCount <= 13)
                {
                    

                    for (int i = 0; i < 3; i++)
                    {

                        Position leftPos = leftPositions[i];    // 왼쪽 스틱
                        

                        draw.MoveCursor(leftPos.x * 2, top+i);
                        draw.Empty();
                        leftPos.x++;
                        draw.MoveCursor(leftPos.x * 2, top + i);
                        draw.Stick();

                        Position rightPos = rightPositions[i];  // 오른쪽 스틱
                        

                        draw.MoveCursor(rightPos.x * 2, top + i);
                        draw.Empty();
                        rightPos.x--;
                        draw.MoveCursor(rightPos.x * 2, top + i);
                        draw.Stick();

                        // 마지막 잔여 스틱 제거
                        draw.MoveCursor(28, top + i);
                        draw.Empty();
                        draw.MoveCursor(34, top + i);
                        draw.Empty();
                }
                    Thread.Sleep(TEMPO);
                    barCount++;

                }
                barCount = default;
                Init(ref leftPositions, ref rightPositions);
            


        }
        // public bar 종료

        public void Init(ref List<Position> leftPositions, ref List<Position> rightPositions)
        {

            leftPositions = new List<Position>();
            rightPositions = new List<Position>();
            // 타이밍 표 생성
            for (int height = 0; height < MAX_HEIGHT; height++)
            {
                bar[height] = new int[MAX_WIDTH];
                for (int width = 0; width < MAX_WIDTH; width++)
                {
                    bar[height][width] = EMPTY;
                    if (width == 0)
                    {
                        int leftY = height;
                        int leftX = width;
                        bar[height][width] = LEFT_STICK;
                        leftPositions.Add(new Position(leftX, leftY));
                    }
                    if (width == MAX_WIDTH - 1)
                    {
                        int rightY = height;
                        int rightX = width;
                        bar[height][width] = RIGHT_STICK;
                        rightPositions.Add(new Position(rightX, rightY));
                    }
                }
            }
        }

    }
}
