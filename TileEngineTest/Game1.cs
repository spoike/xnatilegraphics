using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace TileEngineTest
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TiledBackground tiledBackground;
        InGameGui gameGui;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            tiledBackground = new TiledBackground(this, 10, 25);
            tiledBackground.DrawOrder = 2;
            Components.Add(tiledBackground);

            gameGui = new InGameGui(this, tiledBackground);
            gameGui.DrawOrder = 1;
            Components.Add(gameGui);

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            if (spriteBatch != null)
            {
                spriteBatch.Dispose();
            }
            if (tiledBackground != null)
            {
                tiledBackground.Dispose();
            }
            if (gameGui != null)
            {
                gameGui.Dispose();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
