using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using CovidReloaded_V1;
using CovidReloaded_V1.Enemies;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace CovidReloaded_V1.Screens
{
    public class PlayScreen : Screen
    {
        public Texture2D GroundTexture { get; private set; }
        public Texture2D PlayerTexture { get; private set; }
        public Texture2D ToiletPaperTexture { get; private set; }
        public Texture2D SanitizerTexture { get; private set; }
        public Texture2D AlphaTexture { get; private set; }
        public Texture2D LamdaTexture { get; private set; }
        public Texture2D DeltaTexture { get; private set; }
        public Texture2D OmniTexture { get; private set; }
        public Texture2D ShootingEnemyTexture { get; private set; }
        public Texture2D BulletTexture { get; private set; }
        public Texture2D SpriteSheetTexture { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public SoundEffect ShootSfx { get; private set; }
        public SoundEffect JumpSfx { get; private set; }
        public SoundEffect ImpactSfx { get; private set; }
        public SoundEffect GameOverSfx { get; private set; }
        public Song BackgroundSong { get; private set; }
        public float BulletDelay { get; private set; }

        private Background _ground;
        public Hud _hud;
        private Player _player;
        private SpriteSheetAnimation _spriteSheetAnimation;
        private List<Obstacle> _obstacles = new List<Obstacle>();
        private List<Enemy> _enemies = new List<Enemy>();
        private List<ShootingEnemy> _shootingEnemies = new List<ShootingEnemy>();
        private List<Bullet> _bullets = new List<Bullet>();
        private List<Bullet> _enemyBullets = new List<Bullet>();
        public PlayScreen(Texture2D groundTexture, Texture2D playerTexture,
            Texture2D toiletPaperTexture, Texture2D sanitizerTexture, Texture2D spriteSheetTexture,
            Texture2D alphaTexture, Texture2D deltaTexture, Texture2D lamdaTexture,
            Texture2D omniTexture, Texture2D shootingEnemyTexture, Texture2D bulletTexture, SpriteFont spriteFont,
            SoundEffect shootSfx, SoundEffect jumpSfx, SoundEffect impactSfx, SoundEffect gameOverSfx,
            Song backgroundSong, float bulletDelay)
        {
            //update this constructor if extra textures/properties are needed
            GroundTexture = groundTexture;
            PlayerTexture = playerTexture;
            ToiletPaperTexture = toiletPaperTexture;
            SanitizerTexture = sanitizerTexture;
            SpriteSheetTexture = spriteSheetTexture;
            AlphaTexture = alphaTexture;
            DeltaTexture = deltaTexture;
            LamdaTexture = lamdaTexture;
            OmniTexture = omniTexture;
            ShootingEnemyTexture = shootingEnemyTexture;
            BulletTexture = bulletTexture;
            SpriteFont = spriteFont;
            ShootSfx = shootSfx;
            JumpSfx = jumpSfx;
            ImpactSfx = impactSfx;
            GameOverSfx = gameOverSfx;
            BackgroundSong = backgroundSong;
            BulletDelay = bulletDelay;
            LoadGame();
        }

        //create new instances here instead of in loadcontent
        public void LoadGame()
        {
            _ground = new Background(GroundTexture, new Vector2(0, GameSettings.WINDOWHEIGHT - GameSettings.GROUNDHEIGHT),
                new Vector2(GameSettings.WINDOWWIDTH, GameSettings.GROUNDHEIGHT), Vector2.Zero);

            _spriteSheetAnimation = new SpriteSheetAnimation(SpriteSheetTexture,
                new Vector2(GameSettings.PLAYERXSTARTPOS, GameSettings.PLAYERYSTARTPOS),
                new Vector2(GameSettings.PLAYERWIDTH, GameSettings.PLAYERWIDTH), Vector2.Zero, 1, 4, 2);

            //player is positioned above the ground
            _player = new Player(_spriteSheetAnimation, SpriteSheetTexture, //change PlayerTexture.Height value 
                new Vector2(GameSettings.PLAYERXSTARTPOS, GameSettings.PLAYERYSTARTPOS),
                new Vector2(GameSettings.PLAYERWIDTH, GameSettings.PLAYERHEIGHT), Vector2.Zero, 1, JumpSfx);

            _hud = new Hud(SpriteFont, GameSettings.HUDPOSITION, "SCORE: ", 0, "HIGHSCORE:", 0, "TIME: ",300);

            CreateObstacles();
            CreateEnemies();
            PlayBackgroundMusic();
        }

        private void CreateObstacles()
        {
            Random random = new Random();
            int numberOfObstacles = 3;
            int x = GameSettings.OBSTACLEXSPAWNPOS;

            for (int i = 0; i < numberOfObstacles; i++)
            {
                int choice = random.Next(0, 2);
                int y = GameSettings.OBSTACLESPAWNYPOS;

                if (choice == 1)
                {
                    Obstacle obstacle = new Obstacle(ToiletPaperTexture, new Vector2(x, y),
                    new Vector2(ToiletPaperTexture.Width, ToiletPaperTexture.Height), GameSettings.OBSTACLEMOVEMENT);
                    _obstacles.Add(obstacle);
                }
                else
                {
                    Obstacle obstacle2 = new Obstacle(SanitizerTexture, new Vector2(x, y+GameSettings.GROUNDHEIGHT),
                    new Vector2(SanitizerTexture.Width, SanitizerTexture.Height), GameSettings.OBSTACLEMOVEMENT);
                    _obstacles.Add(obstacle2);
                }
                x += GameSettings.OBSTACLEDISTANCE;

            }
        }

        private void AddObstacle()
        {
            Random random = new Random();
            int numberOfObstacles = 3;
            Obstacle lastObstacle = _obstacles[_obstacles.Count - 1];
            int x = lastObstacle.DestinationRectangle.X + GameSettings.WINDOWWIDTH;
            int y = GameSettings.OBSTACLESPAWNYPOS;
            for (int i = 0; i < numberOfObstacles; i++)
            {
                int choice = random.Next(0, 2);
                if (choice == 1)
                {
                    Obstacle obstacle = new Obstacle(ToiletPaperTexture, new Vector2(x, y),
                    new Vector2(ToiletPaperTexture.Width, ToiletPaperTexture.Height), GameSettings.OBSTACLEMOVEMENT);
                    _obstacles.Add(obstacle);
                }
                else
                {
                    Obstacle obstacle2 = new Obstacle(SanitizerTexture, new Vector2(x, y+GameSettings.GROUNDHEIGHT),
                    new Vector2(SanitizerTexture.Width, SanitizerTexture.Height), GameSettings.OBSTACLEMOVEMENT);
                    _obstacles.Add(obstacle2);
                }
                x += GameSettings.OBSTACLEDISTANCE;
            }


        }

        private void RemoveInactiveObstacles()
        {
            for (int i = _obstacles.Count - 1; i >= 0; i--)
            {
                Obstacle obstacle = _obstacles[i];
                if (!obstacle.IsActive)
                {
                    _obstacles.RemoveAt(i);
                    AddObstacle();
                }
            }
        }

        private void CreateEnemies()
        {
            Random random = new Random();
            int numberOfEnemies = 6;
            int x = GameSettings.WINDOWWIDTH;
            int y = GameSettings.OBSTACLESPAWNYPOS;
            int randomY = random.Next(-4, 4);

            for (int i = 0; i < numberOfEnemies; i++)
            {
                int choice = random.Next(0, 6);
                if (choice == 1)
                {
                    AlphaEnemy alpha = new AlphaEnemy(AlphaTexture,
                        new Vector2(x, y), new Vector2(100, 100), new Vector2(-1, 0), 1);
                    _enemies.Add(alpha);
                }
                if (choice == 2)
                {
                    DeltaEnemy delta = new DeltaEnemy(DeltaTexture,
                        new Vector2(x, y), new Vector2(100, 100), new Vector2(-2, 0), 2);
                    _enemies.Add(delta);
                }
                if (choice == 3)
                {
                    LamdaEnemy lamda = new LamdaEnemy(LamdaTexture,
                        new Vector2(x, y), new Vector2(100, 100), new Vector2(-3, 0), 3);
                    _enemies.Add(lamda);
                }
                if (choice == 4)
                {
                    ShootingEnemy shootingEnemy = new ShootingEnemy(ShootingEnemyTexture, new Vector2(x,y + 100),
                        new Vector2(100, 100), new Vector2(-4, 0), 3);
                    _shootingEnemies.Add(shootingEnemy);
                }
                else
                {
                    OmniEnemy omni = new OmniEnemy(OmniTexture,
                        new Vector2(x, randomY), new Vector2(100, 100), new Vector2(-0.5f, 2), 4);
                    _enemies.Add(omni);
                }
                x += GameSettings.OBSTACLEDISTANCE;
            }
        }

        private void AddEnemy()
        {
            Random random = new Random();
            int numberOfEnemies = 6;
            Enemy lastEnemy = _enemies[_enemies.Count - 1];
            int x = lastEnemy.DestinationRectangle.X;
            int y = GameSettings.OBSTACLESPAWNYPOS;
            for (int i = 0; i < numberOfEnemies; i++)
            {
                int choice = random.Next(0, 6);
                int randomX = random.Next(0, 4); //random speed
                int randomY = random.Next(GameSettings.GROUNDHEIGHT - 50, 450);
                if (choice == 1)
                {
                    AlphaEnemy alpha = new AlphaEnemy(AlphaTexture,
                        new Vector2(x, randomY), new Vector2(100, 100), new Vector2(-6, 0), 1);
                    _enemies.Add(alpha);
                }
                if (choice == 2)
                {
                    DeltaEnemy delta = new DeltaEnemy(DeltaTexture,
                        new Vector2(x, y), new Vector2(100, 100), new Vector2(-1,0), 2);
                    _enemies.Add(delta);
                }
                if (choice == 3)
                {
                    y = GameSettings.OBSTACLESPAWNYPOS;
                    LamdaEnemy lamda = new LamdaEnemy(LamdaTexture,
                        new Vector2(x, randomY), new Vector2(100, 100), new Vector2(-randomX, 0), 3);
                    _enemies.Add(lamda);
                }
                if (choice == 4)
                {
                    ShootingEnemy shootingEnemy = new ShootingEnemy(ShootingEnemyTexture, new Vector2(x, y),
                    new Vector2(150, 150), new Vector2(-4, 0), 3);
                    _shootingEnemies.Add(shootingEnemy);
                }
                else
                {
                    OmniEnemy omni = new OmniEnemy(OmniTexture,
                        new Vector2(x, y), new Vector2(150, 150), new Vector2(-randomX, 0), 4);
                    _enemies.Add(omni);
                }
                x += GameSettings.OBSTACLEDISTANCE;
            }

        }

        private void RemoveInactiveEnemies()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = _enemies[i];
                if (!enemy.IsActive)
                {
                    _enemies.RemoveAt(i);
                    AddEnemy();
                }
            }
        }

        private void RemoveInactiveShootingEnemies()
        {
            for(int i = _shootingEnemies.Count -1; i >= 0; i--)
            {
                ShootingEnemy shootingEnemy = _shootingEnemies[i];
                if(!shootingEnemy.IsActive)
                {
                    _shootingEnemies.RemoveAt(i);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Obstacle obstacle in _obstacles) obstacle.Draw(spriteBatch);
            foreach (Enemy enemy in _enemies) enemy.Draw(spriteBatch);
            foreach (ShootingEnemy shootingEnemy in _shootingEnemies) shootingEnemy.Draw(spriteBatch);
            foreach (Bullet bullet in _bullets) bullet.Draw(spriteBatch);
            foreach (Bullet enemyBullet in _enemyBullets) enemyBullet.Draw(spriteBatch);
            _ground.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            _hud.Draw(spriteBatch);
        }

        protected override void UpdateLogic(GameTime gameTime)
        {
            if(GameSettings.ISGAMEOVER == false)
            {
                UpdateGame(gameTime);
            }
            IsGameOver();
        }

        private void UpdateGame(GameTime gameTime)
        {
            _ground.Update();
            _player.Update();
            _hud.Update(gameTime);
            IsCollidingWithGround();
            foreach (Bullet bullet in _bullets) bullet.Update();
            foreach (Bullet enemyBullet in _enemyBullets) enemyBullet.Update();
            if (IsLeftMouseClicked)
                CreateBullet();
            Debug.WriteLine(IsLeftMouseClicked);
            ResetPosition();
            foreach (Obstacle obstacle in _obstacles) obstacle.Update();
            foreach (Enemy enemy in _enemies) enemy.Update();
            foreach (ShootingEnemy shootingEnemy in _shootingEnemies) shootingEnemy.Update();
            RemoveInactiveObstacles();
            RemoveInactiveEnemies();
            RemoveInactiveShootingEnemies();
            ObstacleCollision();
            EnemyCollision();
            BulletCollision();
            EnemyBulletCollision();
            RemoveInactiveBullets();
            UpdateEnemyBullet(gameTime);
        }

        public void IsCollidingWithGround()
        {
            //if we touch the ground the y position is set to the ground top position
            if (_player.DestinationRectangle.Bottom > GameSettings.WINDOWHEIGHT - GameSettings.GROUNDHEIGHT)
            {
                _player.Position = new Vector2(_player.Position.X,
                    GameSettings.WINDOWHEIGHT - GameSettings.GROUNDHEIGHT - GameSettings.PLAYERHEIGHT);
            }
        }

        public void ObstacleCollision()
        {
            foreach (Obstacle obstacle in _obstacles)
            {
                if (obstacle.IsCollidingWith(_player))
                {
                    _player.Health--; //instant game over
                }
            }
        }

        public void EnemyCollision()
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy.IsCollidingWith(_player))
                {
                    _player.Health -= 1;
                }
            }
            foreach (ShootingEnemy shootingEnemy in _shootingEnemies)
            {
                if (shootingEnemy.IsCollidingWith(_player))
                {
                    _player.Health -= 1;
                }
            }
        }

        public void BulletCollision()
        {
            foreach (Bullet bullet in _bullets)
            {
                foreach (Enemy enemy in _enemies)
                {
                    if (bullet.Hitbox.Intersects(enemy.Hitbox))
                    {
                        enemy.Health-=1;
                        _hud.Score += 100;
                        ImpactSfx.Play(); 
                    }
                }
                foreach (ShootingEnemy shootingEnemy in _shootingEnemies)
                {
                    if(bullet.Hitbox.Intersects(shootingEnemy.Hitbox))
                    {
                        shootingEnemy.Health -= 1;
                        _hud.Score += 200;
                        ImpactSfx.Play();
                    }
                }
            }
        }

        public void EnemyBulletCollision()
        {
            foreach (Bullet bullet in _enemyBullets)
            {//als de enemy kogel de speler raakt gaat deze dood
                if (bullet.Hitbox.Intersects(_player.Hitbox))
                {
                    _player.Health -= 1;
                }
            }
        }

        public void ResetPosition()
        {
            //if the player goes out of screen his position gets reset to the starting position
            if (_player.IsOutOfScreen || IsGameOver())
            {
                _player.Position = new Vector2(GameSettings.PLAYERXSTARTPOS, GameSettings.WINDOWHEIGHT
                    - GameSettings.GROUNDHEIGHT - PlayerTexture.Height);
            }
        }

        public bool IsGameOver()
        {
            if (_player.Health < 0)
            {
                GameOverSfx.Play();
                GameSettings.ISGAMEOVER = true;
                _enemies.Clear();
                _shootingEnemies.Clear();
                _obstacles.Clear();
                _bullets.Clear();
                _enemyBullets.Clear();
                _hud.WriteFile("HighScore.txt");
                LoadGame();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreateBullet()
        {
            Vector2 delta = _currentMouseState.Position.ToVector2() - _player.CenterPosition;
            delta.Normalize();
            Vector2 bulletMovement = delta * GameSettings.BULLETMOVEMENT.X;
            Vector2 bulletPosition = _player.CenterPosition + 2 * bulletMovement;

            Bullet bullet = new Bullet(BulletTexture, bulletPosition, new Vector2(10, 10), bulletMovement);
            _bullets.Add(bullet);
            ShootSfx.Play();
        }

        private void RemoveInactiveBullets()
        {
            RemoveOutOfScreenBullets();
            RemoveEnemyImpactBullets();
            RemoveSpecialEnemyImpactBullets();
            RemoveBulletsonBulletImpact();
        }

        private void FireEnemyBullet()
        {
            foreach(ShootingEnemy shootingEnemy in _shootingEnemies)
            {
                Vector2 enemyBulletMovement = GameSettings.ENEMYBULLETMOVEMENT; //bullet fires left
                Vector2 enemyBulletPosition = shootingEnemy.CenterPosition + 8 * enemyBulletMovement; 
                Bullet ebullet = new Bullet(BulletTexture, enemyBulletPosition, new Vector2(10, 10), enemyBulletMovement);
                _enemyBullets.Add(ebullet);
                ShootSfx.Play();
            }
        }

        private void PlayBackgroundMusic()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(BackgroundSong);
            MediaPlayer.Volume = GameSettings.MUSICVOLUME;
        }

        private void RemoveOutOfScreenBullets()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {//removes bullets who go out of screen
                Bullet bullet = _bullets[i];
                if (!bullet.IsActive)
                {
                    _bullets.RemoveAt(i);
                }
            }
        }

        private void RemoveEnemyImpactBullets()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = _bullets[i];
                for (int j = _enemies.Count - 1; j >= 0; j--)
                {//remove bullets who hit enemy
                    Enemy enemy = _enemies[j];
                    if (bullet.Hitbox.Intersects(enemy.Hitbox))
                    {
                        _bullets.RemoveAt(i);
                    }
                }
            }
        }

        private void RemoveSpecialEnemyImpactBullets()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = _bullets[i];
                for (int k = _shootingEnemies.Count - 1; k >= 0; k--)
                {
                    ShootingEnemy shootingEnemy = _shootingEnemies[k];
                    if (bullet.Hitbox.Intersects(shootingEnemy.Hitbox))
                    {
                        _bullets.RemoveAt(i);
                    }
                }
            }
        }

        private void RemoveBulletsonBulletImpact()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = _bullets[i];
                for (int k = _enemyBullets.Count - 1; k >= 0; k--)
                {
                    Bullet enemyBullets = _enemyBullets[k];
                    if (bullet.Hitbox.Intersects(enemyBullets.Hitbox))
                    {
                        _bullets.RemoveAt(i);
                        _enemyBullets.RemoveAt(k);
                    }
                }
            }
        }

        private void UpdateEnemyBullet(GameTime gameTime)
        {
            //deze float gebruikt omdat de bulletdelay anders per frame uitgevoerd wordt
            //ipv realtime
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float tempBulletDelay = BulletDelay * deltaTime;
            BulletDelay = tempBulletDelay;

            foreach (ShootingEnemy shootingEnemy in _shootingEnemies)
            {
                if (BulletDelay > 0)
                {
                    BulletDelay--;
                }
                //als de bulletdelay op 0 staat en de enemy is niet dood of offscreen is schiet de enemy
                if (BulletDelay == 0 && !shootingEnemy.IsOutOfScreen)
                {
                    FireEnemyBullet();
                    //reset delay
                    BulletDelay = 20;
                }
            }
        }

    }
}























      

