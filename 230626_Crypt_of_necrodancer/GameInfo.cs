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


        protected bool DEBUG_MODE = false; 
        protected bool gameover = false; 

        // MagicNumber
        protected const int MAP_SIZE_X = 35;
        protected const int MAP_SIZE_Y = 19;
        protected const int WALL_VALUE = 50;
        protected const int HEART_TIMING = 625;
        protected const int BOSS_STAGE = 10; //보스 스테이지 출현 스테이지

        // 배열 표시
        protected const int FLOOR = 0;
        protected const int WALL = 1;
        protected const int PORTAL = 2;
        protected const int GOLD = 3;
        protected const int HEART = 4;
        protected const int PLAYER = 999;
        protected const int ENEMY = 10;
        protected const int BOSS = 99;
        protected const int BOSSATTACK = 98;

        protected bool ClearCheck = false;
        protected bool retry = false;

        protected int playerMaxHP = 3;
        protected int playerHP = 3;
        protected int playerAttack = 1;
        protected int playerDamage = 1;

        protected int stage = 1;
        protected int stageturn = 0;
        protected int score = 0;
        protected int highscore = 0;
        protected int gold = default;
        protected int invenSize = default;
        protected int wallCount = default;  // 벽 개수

        protected int greenSlimeMove = default;     // 그린 슬라임
        protected int greenSlimeCount = default;

        protected int blueSlimeMove = default;     // 블루 슬라임
        protected int blueSlimeCount = default;

        protected int hunterMove = 3;     // 헌터 스폰 타이밍

        protected int bossMove = default;     // 보스 무브
        protected int bossAttack = default;
        protected int bossAttackReady = default;
        protected int bossTurn = default;
        protected int bossHP = 30;

        // 적 리스트
        protected List<Position> hunterPositions = new List<Position>();
        protected List<Position> greenSlimePositions = new List<Position>();
        protected List<Position> blueSlimePositions = new List<Position>();

        // 아이템 리스트
        //protected String[] strings = { "막대기", "녹슨 검", "녹슨 방패", "병사의 검", "병사의 방패", "회복 포션", "다이아몬드" };
        protected String[] strings = {"하트 보관함", "큰 하트 보관함", "붕대", "회복 포션", "낡은 지도","파워 포션", "뜨거운 여섯" };
        protected String[] inventory = new string[99];

    }
}
