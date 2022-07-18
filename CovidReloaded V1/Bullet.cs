using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1
{
    public class Bullet : GameObject
    {
        private float _initialRotation = MathF.PI / 2;
        public float Rotation { get; private set; }
        //monogame rotation = clockwise math rotation = counterclockwise
        public Bullet(Texture2D texture, Vector2 position, Vector2 size, Vector2 movement)
            : base(texture, position, size, movement)
        {
            Rotation = MathF.Atan2(-Movement.Y, Movement.X);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, null, Color.White,
                -Rotation + _initialRotation, new Vector2(Texture.Width, Texture.Height), SpriteEffects.None, 0);
        }

        public override Rectangle Hitbox
        {
            get
            {
                Rectangle dr = DestinationRectangle;
                return new Rectangle((int)(dr.X - Size.X/2), (int)(dr.Y-Size.Y), dr.Width, dr.Height);
            }
        }

        


    }
}
