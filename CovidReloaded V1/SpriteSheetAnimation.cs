using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1
{
    public class SpriteSheetAnimation : GameObject
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int FrameDelay { get; private set; }
        public int CurrentSpriteFrames { get; private set; }
        public int CurrentSprite { get; private set; }
        public SpriteSheetAnimation(Texture2D texture, Vector2 position, Vector2 size, Vector2 movement,
            int rows, int columns, int frameDelay)
            : base(texture, position, size, movement)
        {
            Rows = rows;
            Columns = columns;
            FrameDelay = frameDelay;
        }

        public override void Update()
        {

            CurrentSpriteFrames++;
            if (CurrentSpriteFrames >= FrameDelay)
            {
                CurrentSprite++;//volgende sprite
                if(CurrentSprite >= Rows * Columns) //groter dan aantal sprites op sheet reset naar positie 0
                {
                    CurrentSprite = 0;
                }
                CurrentSpriteFrames = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle(), Color.White);
        }

        public virtual void SetFrame(int newFrame)
        {
            CurrentSprite = newFrame;
        }

        public Rectangle SourceRectangle()
        {
            int row = CurrentSprite / Columns;
            int column = CurrentSprite % Columns;

            float rowHeight = Texture.Height / (float)Rows;
            float columnWidth = Texture.Width / (float)Columns;

            return new Rectangle(
                (int)(column * columnWidth),
                (int)(row * rowHeight),
                (int)columnWidth,
                (int)rowHeight);
        }

    }
}
