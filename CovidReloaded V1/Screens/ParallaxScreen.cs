using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using CovidReloaded_V1;

namespace CovidReloaded_V1.Screens
{
    public class ParallaxScreen
    {
        public Texture2D BackgroundImage { get; private set; }
        public float BackgroundShift { get; private set; }
        public float ShiftSpeed { get; private set; } = 2.0f;
        public int ImageCount { get; private set; }

        public ParallaxScreen(Texture2D backgroundImage)
        {
            BackgroundImage = backgroundImage;
        }

        public void Update()
        {
            UpdateBackGroundPosition();
        }

        private void UpdateBackGroundPosition()
        {
            BackgroundShift -= ShiftSpeed;
            if(BackgroundShift < -BackgroundImage.Width)
            {
                BackgroundShift += BackgroundImage.Width;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            ShowBackground(spriteBatch);
        }

        private void ShowBackground(SpriteBatch spriteBatch)
        {
            ImageCount = GameSettings.WINDOWWIDTH / BackgroundImage.Width;
            if (GameSettings.WINDOWWIDTH % BackgroundImage.Width != 0)
            {
                ImageCount++;
            }
            ImageCount++;

            for (int i = 0; i < ImageCount; i++)
            {
                int x = (int)(BackgroundShift + i * BackgroundImage.Width);
                spriteBatch.Draw(BackgroundImage, new Rectangle(new Point(x, 0),
                    new Point(BackgroundImage.Width, GameSettings.WINDOWHEIGHT)), Color.White);
            }
        }


    }
}
