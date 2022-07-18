using Microsoft.Xna.Framework;
using CovidReloaded_V1.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace CovidReloaded_V1
{
    public static class GameSettings
    {
        public static Screen StartScreen { get; set; }
        public static Screen PlayScreen { get; set; }
        public static Screen ActiveScreen { get; set; }
        public static Screen GameOverScreen { get; set; }
        public static Screen ParallaxScreen { get; set; }

        public const int WINDOWWIDTH = 1000;
        public const int WINDOWHEIGHT = 500;

        public const int HUDPOSITIONX = WINDOWWIDTH/2;
        public const int HUDPOSITIONY = 10;

        public const int GROUNDHEIGHT = 50;

        public const int PLAYERHEIGHT = 70;
        public const int PLAYERWIDTH = 50;
        public const int PLAYERXSTARTPOS = 30;
        public const int PLAYERYSTARTPOS = 130;

        public const int OBSTACLEXSPAWNPOS = 1250;
        public const int OBSTACLESPAWNYPOS = 250;
        public const int OBSTACLEDISTANCE = 600;

        public static Vector2 OBSTACLEMOVEMENT = new Vector2(-1, 0);
        public static Vector2 SCROLLMOVEMENT = new Vector2(-0.5f, 0);
        public static Vector2 BULLETMOVEMENT = new Vector2(10, 0);
        public static Vector2 ENEMYBULLETMOVEMENT = new Vector2(-5, 0);
        public static Vector2 GRAVITY = new Vector2(0, 1f);
        public static Vector2 PLAYERJUMP = new Vector2(0, -350.0f);
        public static Vector2 PLAYERHORIZONTALMOVEMENT = new Vector2(2, 0);

        public static Vector2 HUDPOSITION = new Vector2(WINDOWWIDTH / 2, 10);
        public static Vector2 HIGHSCOREHUDPOSITION = new Vector2(10, 10);
        public static Vector2 TIMERPOSITION = new Vector2(WINDOWWIDTH - 150, 10);

        public const float MUSICVOLUME = 0.3f;

        public static bool ISGAMEOVER = false;

    }
}
