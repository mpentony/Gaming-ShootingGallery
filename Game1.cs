using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingGallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D targetSprite;
        Texture2D crosshairsSprite;
        Texture2D backgroundSprite;
        SpriteFont gameFont;

        Vector2 targetPosition = new Vector2(300, 300);
        const int targetRadius = 45;

        MouseState mState;
        int score = 0;
        bool mReleased = true;

        double timer = 10;

        public Game1()
        {
            //basic settings like windows settings
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Load components into the game

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Load all images, sounds and other content
            targetSprite = Content.Load<Texture2D>("target");
            crosshairsSprite = Content.Load<Texture2D>("crosshairs");
            backgroundSprite = Content.Load<Texture2D>("sky");
            gameFont = Content.Load<SpriteFont>("galleryFont");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timer <0)
            {
                timer = 0;
            }
            // TODO: Add your update logic here
            //Game Loop which runs every frame of the game
            mState = Mouse.GetState();

            if (mState.LeftButton == ButtonState.Pressed  && mReleased == true)
            {
                float mouseTargetDist = Vector2.Distance(targetPosition, mState.Position.ToVector2());
                if (mouseTargetDist < targetRadius && timer>0)
                {
                    score++;
                    Random rand = new Random();
                    targetPosition.X = rand.Next(0, _graphics.PreferredBackBufferWidth);
                    targetPosition.Y = rand.Next(0, _graphics.PreferredBackBufferHeight);
                }
               
                mReleased = false;
            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //Anything that involves drawing on the screen and runs once every frame
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, new Vector2(0,0), Color.White);
            _spriteBatch.DrawString(gameFont, score.ToString(), new Vector2(3, 3), Color.White);
            _spriteBatch.DrawString(gameFont, Math.Ceiling(timer).ToString(), new Vector2(3, 40), Color.Red);
            if (timer > 0)
            {
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X - targetRadius, targetPosition.Y - targetRadius), Color.White);
            }
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
