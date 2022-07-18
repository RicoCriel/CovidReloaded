using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1.Screens
{
    public abstract class Screen
    {
        protected KeyboardState _currentKeyboardState, _previousKeyboardState;
        protected MouseState _currentMouseState, _previousMouseState;

        public void Update(GameTime gameTime)
        {
            SetCurrentStates();
            UpdateLogic(gameTime);
            SetPreviousStates();
        }

        protected abstract void UpdateLogic(GameTime gameTime);

        protected void SetPreviousStates()
        {
            _previousKeyboardState = _currentKeyboardState;
            _previousMouseState = _currentMouseState;
        }

        protected void SetCurrentStates()
        {
            _currentKeyboardState = Keyboard.GetState();
            _currentMouseState = Mouse.GetState();
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public Boolean IsKeyClick(Keys key)
        {
            return _currentKeyboardState.IsKeyUp(key) && _previousKeyboardState.IsKeyDown(key);
        }

        public Boolean IsLeftMouseClicked
        {
            get
            {
                return _currentMouseState.LeftButton == ButtonState.Released
                 && _previousMouseState.LeftButton == ButtonState.Pressed;
            }
        }



    }
}
