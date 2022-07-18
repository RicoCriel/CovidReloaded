using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1.Enemies
{
    public class AlphaEnemy : Enemy
    {
        //standaard vijand 1 health, horizontale movement = 1
        public AlphaEnemy(Texture2D texture, Vector2 position, Vector2 size, Vector2 movement, int health)
            : base(texture, position, size, movement, health)
        {

        }
    }
}
