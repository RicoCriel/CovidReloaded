using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1
{
    public class GameObject
    {
        //protected set used to give acces property within parent class or inherited class
        public Texture2D Texture { get; protected set; }
        public Vector2 Position { get; set; } // had toegang nodig in playscreen
        public Vector2 Size { get; private set; }
        public Vector2 Movement { get; protected set; }
        public Boolean IsActive { get; protected set; } = true;

        public GameObject(Texture2D texture, Vector2 position, Vector2 size, Vector2 movement)
        {
            Texture = texture;
            Position = position;
            Size = size;
            Movement = movement;
        }

        public virtual void Update()
        {
            if (IsActive)
            {
                Position += Movement;
                if (IsOutOfScreen)
                {
                    IsActive = false;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!IsActive) return;
            spriteBatch.Draw(Texture, DestinationRectangle, Color.White);
        }

        public Rectangle DestinationRectangle
        {
            get
            {
                return new Rectangle(Position.ToPoint(), Size.ToPoint());
            }
        }

        //aanpassen naar hitbox
        public virtual Boolean IsCollidingWith(GameObject other)
        {
            return this.Hitbox.Intersects(other.Hitbox);
        }

        //we gebruiken deze hitbox voor collisiondetection
        //omdat deze accurater is 
        public virtual Rectangle Hitbox 
        {
            get { return DestinationRectangle; }
        }

        public virtual Boolean IsOutOfScreen
        {
            get
            {
                //variable created for efficiency (doesnt call DestinationRectangle 4times)
                Rectangle rectangle = DestinationRectangle;
                if (rectangle.Right < 0) return true;
                if (rectangle.Left > GameSettings.WINDOWWIDTH) return true;
                if (rectangle.Bottom < 0) return true;
                if (rectangle.Top > GameSettings.WINDOWHEIGHT) return true;
                return false;
            }
        }

        public Vector2 CenterPosition
        {
            get
            {
                return Position + Size / 2;
            }
        }


    }
}
