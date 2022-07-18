using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CovidReloaded_V1
{
    public class Player : GameObject
    {
        private KeyboardState _previousKeyboardState, _currentKeyboardState;
        public int Health { get; set; } //public gezet omdat ik toegang nodig had hiertoe in PlayScreen
        public SoundEffect JumpSfx {get; private set;}
        public SpriteSheetAnimation SpriteSheet { get; private set; }
        public Player(SpriteSheetAnimation spriteSheet,Texture2D texture, Vector2 position, 
            Vector2 size, Vector2 movement, int health, SoundEffect jumpSfx)
            : base(texture, position, size, movement)
        {
            SpriteSheet = spriteSheet;
            Health = health;
            JumpSfx = jumpSfx;
        }

        public override void Update()
        {
            SetStates();
            Walk();
            Jump();
            
            Position += Movement;
        }

        private void SetStates()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
        }

        private void Jump()
        {
            float velocity = 5f;
            
            Movement += GameSettings.GRAVITY * velocity;
            //if we have pressed space once and are not in the air we can jump
            if (_previousKeyboardState.IsKeyDown(Keys.Space) && _currentKeyboardState.IsKeyUp(Keys.Space)
                && IsGrounded) 
            {
                Movement += GameSettings.PLAYERJUMP;
                SpriteSheet.SetFrame(0);
                JumpSfx.Play();
            }
        }

        private void Walk()
        {
            Vector2 idle = new Vector2(0);
            float speed = 2f;

            if (IsPlayerMovingLeft)
            {
                MovePlayer(-GameSettings.PLAYERHORIZONTALMOVEMENT * speed);
                SpriteSheet.Update();
            }
            else if (IsPlayerMovingRight)
            {
                MovePlayer(GameSettings.PLAYERHORIZONTALMOVEMENT * speed);
                SpriteSheet.Update();
            }
            else
            {
                MovePlayer(idle);
                SpriteSheet.SetFrame(0);
            }
        }

        private bool IsPlayerMovingLeft
        {
            get
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left)
                    || Keyboard.GetState().IsKeyDown(Keys.A)) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool IsPlayerMovingRight
        {
            get
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right) ||
                    Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void MovePlayer(Vector2 movement)
        {
            Movement = movement;
            Position += Movement;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(IsPlayerMovingRight)
            {
                spriteBatch.Draw(SpriteSheet.Texture, DestinationRectangle,
                SpriteSheet.SourceRectangle(), Color.White);
            }
            if(IsPlayerMovingLeft)
            {
                spriteBatch.Draw(SpriteSheet.Texture, DestinationRectangle,
                SpriteSheet.SourceRectangle(), Color.White,0, new Vector2(0,0), SpriteEffects.FlipHorizontally,0);
            }
            else
            {
                spriteBatch.Draw(SpriteSheet.Texture, DestinationRectangle,
                SpriteSheet.SourceRectangle(), Color.White);
            }
        }

        public bool IsGrounded
        {
            get
            {//als we de grond raken dan geven true terug
                if (Position.Y == GameSettings.WINDOWHEIGHT
                    - GameSettings.GROUNDHEIGHT - GameSettings.PLAYERHEIGHT)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
