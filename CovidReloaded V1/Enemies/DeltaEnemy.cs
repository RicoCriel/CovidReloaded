using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1.Enemies
{
    public class DeltaEnemy : Enemy
    {

        //health = 2, verticale movement
        public DeltaEnemy(Texture2D texture, Vector2 position, Vector2 size, Vector2 movement, int health)
            : base(texture, position, size, movement, health)
        {

        }
    }
}
