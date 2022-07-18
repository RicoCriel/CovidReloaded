using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1.Enemies
{
    public class OmniEnemy : Enemy
    {

        //health = 3 verticale positie movement 3 schiet (enkel tekenen als score += 500 is)
        public OmniEnemy(Texture2D texture, Vector2 position, Vector2 size, Vector2 movement, int health)
            : base(texture, position, size, movement, health)
        {

        }
    }
}
