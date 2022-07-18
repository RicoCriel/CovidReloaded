using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidReloaded_V1
{
    public class Ground
    {
        //not a gameobject but contains gameobjects
        public List<GameObject> _gameObjects = new List<GameObject>();
        public Ground(List<GameObject> gameObjects)
        {
            _gameObjects = gameObjects;
        }
        public void Update()
        {
            foreach (GameObject ground in _gameObjects)
                ground.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject ground in _gameObjects)
                ground.Draw(spriteBatch);
        }

        public Ground(Texture2D Texture, Vector2 position, Vector2 size, Vector2 movement)
        {
            float x = position.X;
            while (x < position.X + size.X)
            {
                _gameObjects.Add(new GameObject(Texture, new Vector2(x, position.Y),
                    new Vector2(Texture.Width, size.Y), Vector2.Zero));
                x += Texture.Width;
            }
        }



    }
}
