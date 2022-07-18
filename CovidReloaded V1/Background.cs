using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1
{
    public class Background : GameObject
    {
        public Background(Texture2D texture, Vector2 position, Vector2 size, Vector2 movement)
            : base(texture, position, size, movement)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = DestinationRectangle;
            int x = destinationRectangle.Left;
            while (x < GameSettings.WINDOWWIDTH)
            {
                spriteBatch.Draw(Texture, new Rectangle(x, (int)Position.Y, Texture.Width, Texture.Height), Color.White);
                x += Texture.Width;
            }
        }

        
    }
}
