using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace TileEngineTest
{
    public class InGameGui : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteFont font;
        HexagonTiledBackground tiledBackground;
        SpriteBatch spriteBatch;

        public InGameGui(Game game, HexagonTiledBackground tiledBackground)
            : base(game)
        {
            this.tiledBackground = tiledBackground;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }
        
        public InGameGui(Game game)
            : this(game, null)
        {
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("Lucida Console");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            Point selected = tiledBackground.SelectedTile;
            spriteBatch.DrawString(font, "Hai Wurldz. You have selected tile " + selected.X + "," + selected.Y + ".", new Vector2(1.0f, 1.0f), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            if (spriteBatch != null)
            {
                spriteBatch.Dispose();
            }
            if (tiledBackground != null)
            {
                tiledBackground.Dispose();
            }
        }
    }
}