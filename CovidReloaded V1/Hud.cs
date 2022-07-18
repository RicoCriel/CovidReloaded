using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace CovidReloaded_V1
{
    public class Hud
    {
        public SpriteFont HudFont { get; private set; }
        public string ScoreHudText { get; private set; }
        public string HighScoreHudText { get; private set; }
        public string TimerHudText { get; private set; }
        public Vector2 Position { get; private set; }
        public int Score { get; set; } //toegang nodig in playscreen
        public int HighScore { get; private set; } 
        public float Timer { get; private set; }

        public Hud(SpriteFont hudFont, Vector2 position, string scoreHudText,
            int score, string highScoreHudText,int highScore, string timerHudText, float timer)
        {
            HudFont = hudFont;
            Position = position;
            ScoreHudText = scoreHudText;
            Score = score;
            HighScoreHudText = highScoreHudText;
            HighScore = highScore;
            TimerHudText = timerHudText;
            Timer = timer;
            LoadScores();
            Debug.WriteLine(DisplayHighScore());
        }

        public void Update(GameTime gameTime)
        {
            UpdateTimer(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(HudFont, ScoreHudText + Score.ToString(), Position, Color.White);
            spriteBatch.DrawString(HudFont, HighScoreHudText + DisplayHighScore().ToString(),
                GameSettings.HIGHSCOREHUDPOSITION, Color.White);
            spriteBatch.DrawString(HudFont, TimerHudText + (int)Timer, GameSettings.TIMERPOSITION, Color.White);
        }

        private string ReadFile(string fileName)
        {
            if (File.Exists(fileName)) //testen of file bestaat
                using (StreamReader streamReader = new StreamReader(fileName))//exception unhandles being used by other process
                //wordt enkel gebruikt om te lezen
                {
                    string total = "";
                    bool isFirstLine = true;
                    string line = streamReader.ReadLine(); //lees lijn
                    while (line != null) //zolang we niet op het einde van de file zijn
                    {
                        if (isFirstLine)
                        {
                            total = line;
                            isFirstLine = false;
                        }
                        else
                            total += "\n" + line; //wordt toegevoegd aan vorige (of eerste) lijn
                        line = streamReader.ReadLine(); //volgende lijn lezen
                    }//na het lezen van volledige file geven we total terug
                    return total;
                }
            else
                return "File not found";
        }

        private int DisplayHighScore()
        {
            string[] lines = File.ReadAllLines("HighScore.txt");
            int highScore = lines.Length - 1;
            string lastLine = lines[highScore]; //we vragen de laatste lijn in het bestand op
            if (File.Exists("HighScore.txt") && int.TryParse(lastLine, out highScore)) 
            {//we parsen de laatste lijn om deze later te kunnen vergelijken met de huidige score
                //return high score
                return highScore;
            }
            else if(lines.Length > 1)//als er nog niets in het bestand staat geven we 0 terug
            {
                return 0;
            }
            else
            {
                //geen bestand gevonden of niet mogelijk om te parsen naar int
                //default highscore is 0
                return 0;
            }
        }

        public void WriteFile(string fileName)
        {
             using (StreamWriter sw = new StreamWriter(fileName, true))
             {
                //Schrijf de huidige score waarde naar het bestand
                if(Score> HighScore) //als deze hoger is dan de huidige highScore
                {
                    sw.WriteLine(Score.ToString());
                }
            }
        }

        private void LoadScores()
        {
            //laad de vorige opgeslagen high score bij het opstarten
            ReadFile("HighScore.txt");
            HighScore = DisplayHighScore();
            Reset();
        }

        private void Reset()
        {
            //voor het resetten bij gameover sla de huidige score op
            //naar highscore opslag als het hoger is dan de huidge highscore
            if(Score > HighScore)
            {
                WriteFile("HighScore.txt");
                HighScore = Score;
            }
        }

        private void UpdateTimer(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Timer -= deltaTime;
            if(Timer <= 0)
            {
                GameSettings.ISGAMEOVER = true;
            }
        }










    }
}
