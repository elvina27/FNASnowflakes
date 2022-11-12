using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FNASnowflakes
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D snezhinka, fon;

        public List<Snowflake> snowflakes = new List<Snowflake>();
        private int stop = 0;
        public Game1() 
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            Add();
            IsMouseVisible = true;
        }

        protected void Add()
        {
            var rnd = new Random();
            for (int i = 0; i < 2000; i++)
            {
                snowflakes.Add(new Snowflake
                {
                    X = rnd.Next(graphics.PreferredBackBufferWidth),
                    Y = -rnd.Next(graphics.PreferredBackBufferHeight),
                    Size = rnd.Next(15, 40)
                });
            }
        }

        //protected override void Initialize()
        //{
        //    base.Initialize();
        //}

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fon = TextureLoader.Load("fon", Content);
            snezhinka = TextureLoader.Load("snow", Content);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            Click();
        }

        protected override void Draw(GameTime gameTime)
        {
            if (stop == 0)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(fon, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);

                foreach (var snowflake in snowflakes)
                {
                    snowflake.Y += snowflake.Size / 8;
                    if (snowflake.Y > graphics.PreferredBackBufferHeight)
                    {
                        snowflake.Y = -snowflake.Size;
                    }
                }
                foreach (var snowflake in snowflakes)
                {
                    spriteBatch.Draw(snezhinka, new Rectangle(snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size), Color.White);
                }

                spriteBatch.End();
            }
        }
        private void Click()
        {
            if (Input.KeyPressed(Keys.Space))
            {
                if (stop == 0)
                {
                    stop = 1;
                }

                else if (stop == 1)
                {
                    stop = 0;
                }
            }
        }
    }
}
