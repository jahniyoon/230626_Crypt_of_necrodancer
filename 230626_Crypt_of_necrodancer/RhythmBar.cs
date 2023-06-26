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
        const int VOID_HEART = 3;
        const int MAX_HEIGHT = 3;
        const int MAX_WIDTH = 35;
        const int TEMPO = 23;
        int barCount = default;
        Draw draw = new Draw();


        public void Bar(int left, int top)
        {
            draw.MoveCursor(left, top);

            Console.WriteLine("이곳에 출력");

            List<Position> leftPositions = new List<Position>();
            List<Position> rightPositions = new List<Position>();
            List<Position> voidHeartPositions = new List<Position>();


            Init(ref leftPositions, ref rightPositions, ref voidHeartPositions);

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
           
                while (barCount <= 15)
                {
                    for (int i = 0; i < MAX_HEIGHT; i++)
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

                        
                    
                    draw.GrayHeart(left + 30, top);
                    if (barCount >= 12)
                    {
                        draw.Heart(left + 30, top);

                    }

                }
                Thread.Sleep(TEMPO);
                    barCount++;

                }
                barCount = default;
                Init(ref leftPositions, ref rightPositions, ref voidHeartPositions);
            


        }
        // public bar 종료

        public void Init(ref List<Position> leftPositions, ref List<Position> rightPositions, ref List<Position> voidHeartPositions)
        {

            leftPositions = new List<Position>();
            rightPositions = new List<Position>();
            voidHeartPositions = new List<Position>();
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
