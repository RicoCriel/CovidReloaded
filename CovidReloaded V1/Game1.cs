using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using GeoSketch;
using CovidReloaded_V1.Screens;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace CovidReloaded_V1
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private PlayScreen _playscreen;
        private GameOverScreen _gameOverScreen;
        private ParallaxScreen _parallaxScreen;
        private Texture2D _groundTexture, _playerTexture,
            _toiletPaperTexture, _sanitizerTexture, _spriteSheetTexture, 
            _alphaTexture, _deltaTexture, _lamdaTexture,
            _omniTexture, _shootingEnemyTexture,_bulletTexture;
        private SpriteFont _Arial;
        private SoundEffect _jumpSfx, _shootSfx, _impactSfx, _gameOverSfx;
        private Song _backgroundSong;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = GameSettings.WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = GameSettings.WINDOWHEIGHT;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _groundTexture = Content.Load<Texture2D>(@"Graphics/Ground");
            _playerTexture = Content.Load<Texture2D>(@"Graphics/PlayerTest");
            _toiletPaperTexture = Content.Load<Texture2D>(@"Graphics/WcPapier");
            _sanitizerTexture = Content.Load<Texture2D>(@"Graphics/Sanitizer");
            _spriteSheetTexture = Content.Load<Texture2D>(@"Graphics/CharacterSprite");
            _bulletTexture = Content.Load<Texture2D>(@"Graphics/bullet");

            _alphaTexture = Content.Load<Texture2D>(@"Graphics/AlphaVariant");
            _deltaTexture = Content.Load<Texture2D>(@"Graphics/DeltaVariant");
            _lamdaTexture = Content.Load<Texture2D>(@"Graphics/LamdaVariant");
            _omniTexture = Content.Load<Texture2D>(@"Graphics/OmniVariant");
            _shootingEnemyTexture = Content.Load<Texture2D>(@"Graphics/ShootingEnemy");

            _Arial = Content.Load<SpriteFont>(@"Fonts/Arial");
            _shootSfx = Content.Load<SoundEffect>(@"Audio/Laser");
            _jumpSfx = Content.Load<SoundEffect>(@"Audio/Jump");
            _impactSfx = Content.Load<SoundEffect>(@"Audio/ImpactSfx");
            _gameOverSfx = Content.Load<SoundEffect>(@"Audio/GameOverSound");
            _backgroundSong = Content.Load<Song>(@"Audio/FinalBackgroundSong");

            Texture2D controls = Content.Load<Texture2D>("Graphics/ControlScreen");
            GameSettings.ActiveScreen = GameSettings.StartScreen = new StartScreen(controls);
            Texture2D gameOver = Content.Load<Texture2D>("Graphics/GameOverScreen");
            Texture2D parallax = Content.Load<Texture2D>("Graphics/Achtergrond");

            _gameOverScreen = new GameOverScreen(gameOver);
            _parallaxScreen = new ParallaxScreen(parallax);
            _playscreen = new PlayScreen(_groundTexture, _playerTexture, _toiletPaperTexture,
                _sanitizerTexture, _spriteSheetTexture, _alphaTexture,
                _deltaTexture, _lamdaTexture, _omniTexture, _shootingEnemyTexture, _bulletTexture, _Arial, _shootSfx,
                _jumpSfx, _impactSfx, _gameOverSfx, _backgroundSong,20);
            GameSettings.PlayScreen = _playscreen;
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            GameSettings.ActiveScreen.Update(gameTime);
            _parallaxScreen.Update();
            UpdateGameOver();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _parallaxScreen.Draw(_spriteBatch);
            GameSettings.ActiveScreen.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void UpdateGameOver()
        {
            if (GameSettings.ISGAMEOVER == true)
            {
                GameSettings.ActiveScreen = _gameOverScreen;
            }
        }


    }
}
