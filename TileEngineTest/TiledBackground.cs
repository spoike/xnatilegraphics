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
    public class TiledBackground : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D tiles;
        private Texture2D selection;
        private SpriteBatch spriteBatch;
        private int xOffset = 10;
        private int yOffset = 25;
        private int xSelect = 0;
        private int ySelect = 0;
        private int height = 10;
        private int length = 10;
        private int tileSize = 40;
        private List<Rectangle> rects;

        public TiledBackground(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public override void Initialize()
        {
            base.Initialize();

            rects = new List<Rectangle>(10);

            for (int i = 0; i < 2; i++)
            {
                rects.Add(new Rectangle(i * 40, 0, 40, 40));
            }
        }

        protected override void LoadContent()
        {
            Game.Content.RootDirectory = "Content";
            tiles = Game.Content.Load<Texture2D>("080502_tiles");
            selection = Game.Content.Load<Texture2D>("selection_tile");
            loaded = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (tiles != null)
                tiles.Dispose();
            if (selection != null)
                selection.Dispose();
        }

        protected Boolean MouseIsInside(int x, int y)
        {
            return x > xOffset && y > yOffset && x < (length * tileSize) + xOffset && y < (height * tileSize) + yOffset;
        }

        private Boolean loaded = false;

        public override void Draw(GameTime gameTime)
        {
            if (loaded)
            {
                spriteBatch.Begin();

                int size = height * length;
                for (int i = 0; i < size; i++)
                {
                    spriteBatch.Draw(tiles, new Vector2(i * 40 % (length * 40) + xOffset, (i / length) * 40 + yOffset), rects[0], Color.White);
                }
                spriteBatch.Draw(selection, new Vector2(xSelect * 40 + xOffset, ySelect * 40 + yOffset), Color.White);

                spriteBatch.End();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                int x = Mouse.GetState().X;
                int y = Mouse.GetState().Y;
                if (MouseIsInside(x, y))
                {
                    xSelect = (x - xOffset) / tileSize;
                    ySelect = (y - yOffset) / tileSize;
                }
            }

            base.Update(gameTime);
        }

        public Point SelectedTile
        {
            get
            {
                return new Point(xSelect, ySelect);
            }
        }
    }
}