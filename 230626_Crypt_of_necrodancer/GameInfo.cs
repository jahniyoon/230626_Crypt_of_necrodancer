using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _230626_Crypt_of_necrodancer
{
    public class GameInfo
    {
        protected Random random = new Random();


        protected bool DEBUG_MODE = true; 

        protected const int MAP_SIZE_X = 35;
        protected const int MAP_SIZE_Y = 19;

        protected const int FLOOR = 0;
        protected const int ENEMY = 10;
        protected const int SLIME = 11;


        protected const int WALL = 1;
        protected const int PORTAL = 3;
        protected const int WALL_VALUE = 35;
        protected const int HEART_TIMING = 625;

        protected int playerHP = 3;
        protected int playerDamage = 1;
        protected bool ClearCheck = false;
        protected int stage = 1;
        protected int score = 0;



        protected int hunterMove = 3;     // 헌터 스폰 타이밍
        protected int slimeCount = default;     // 헌터 스폰 타이밍


        // 적 리스트
        protected List<Position> hunterPositions = new List<Position>();
        protected List<Position> slimePositions = new List<Position>();


    }
}
