using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1
{
    public class Obstacle : GameObject
    {

        public Obstacle(Texture2D texture, Vector2 position, Vector2 size, Vector2 movement)
            : base(texture, position, size, movement)
        {

        }

        public override void Update()
        {
            if(IsActive)
            {
                Position += Movement;
                if (DestinationRectangle.Right < 0 )
                {
                    IsActive = false;
                }
            }
        }







    }
}
