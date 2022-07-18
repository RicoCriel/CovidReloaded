using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1.Screens
{
    public class StartScreen : Screen
    {
        public Texture2D Texture { get; private set; }

        public StartScreen(Texture2D texture)
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
           if(IsKeyClick(Keys.Enter))
           {
               GameSettings.ActiveScreen = GameSettings.PlayScreen;
           }
        }
    }


}
