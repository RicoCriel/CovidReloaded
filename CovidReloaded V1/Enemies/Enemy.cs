using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1.Enemies
{
    public class Enemy : GameObject
    {
        public int Health { get; set; } //public gemaakt om collision methode te maken in playscreen
        public Enemy(Texture2D texture, Vector2 position, Vector2 size, Vector2 movement, int health)
            : base(texture, position, size, movement)
        {
            Health = health;
        }

        public override void Update()
        {
            if(IsActive)
            {
                Position += Movement;
                //als health gelijk is of minder is dan 0 is deze niet meer actief
                //als de enemy zich niet meer in het scherm bevindt is deze ook niet meer actief
                if (DestinationRectangle.Right < 0 || Health <= 0) 
                {
                    IsActive = false;
                }
            }
        }
    }
}
