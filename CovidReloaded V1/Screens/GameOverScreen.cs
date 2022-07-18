using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1.Screens
{
    public class GameOverScreen : Screen
    {
        private Texture2D Texture { get; set; }

        public GameOverScreen(Texture2D texture)
        {
            Texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle(0, 0, GameSettings.WINDOWWIDTH, GameSettings.WINDOWHEIGHT);
            spriteBatch.Draw(Texture, destinationRectangle, Color.White);
        }

        protected override void UpdateLogic(GameTime gameTime)
        {
            if (IsKeyClick(Keys.Escape))
            {
                GameSettings.ActiveScreen = GameSettings.StartScreen;
                GameSettings.ISGAMEOVER = false;
            }
        }
        
            
            
        



    }
}
