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
    public class HexagonTiledBackground : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private TileGraphic tiles;
        private Texture2D selection;
        private Texture2D mask;
        private SpriteBatch spriteBatch;
        private Point selected;
        private int xOffset;
        private int yOffset;
        private int height = 10;
        private int length = 10;
        private int tileSize;
        private int tileWidth = 30;
        private int tileHeight;
        private int[,] tileContent;

        public HexagonTiledBackground(Game game, int xOffset, int yOffset)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.xOffset = xOffset;
            this.yOffset = yOffset;

            tileContent = new int[length, height];
        }

        public override void Initialize()
        {
            base.Initialize();

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tileContent[i, j] = 0;
                }
            }
        }

        protected override void LoadContent()
        {
            Game.Content.RootDirectory = "Content";
            tiles = new TileGraphic(Game.Content.Load<Texture2D>("hexa_tiles"), 2, 1);
            tileSize = tiles.TileWidth;
            tileHeight = tiles.TileHeight;
            selection = Game.Content.Load<Texture2D>("hexa_select");
            mask = Game.Content.Load<Texture2D>("hexa_mask");
            loaded = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (tiles != null)
            {
                tiles.Dispose();
            }
            if (selection != null)
            {
                selection.Dispose();
            }
        }

        protected Boolean MouseIsInside(Point mousePosition)
        {
            int x = mousePosition.X;
            int y = mousePosition.Y;
            return x > xOffset && y > yOffset && x < (length * tileSize) + xOffset && y < (height * tileSize) + yOffset;
        }

        private Boolean loaded = false;

        public override void Draw(GameTime gameTime)
        {
            if (loaded)
            {
                spriteBatch.Begin();

                int size = height * length;
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        spriteBatch.Draw(
                            tiles.Texture,
                            new Vector2(i * tileWidth + xOffset, j * tileSize + yOffset + ((i%2) * tileHeight / 2) ),
                            tiles.Tile(tileContent[i, j], 0),
                            Color.White);
                    }
                }
                spriteBatch.Draw(
                    selection, 
                    new Vector2(selected.X * tileWidth + xOffset, selected.Y * tileSize + ( selected.X % 2 ) * (tileHeight / 2) + yOffset), 
                    Color.White);

                spriteBatch.End();
            }
        }

        private bool isPressed = false;

        public override void Update(GameTime gameTime)
        {
            if (!isPressed && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Point mousePosition = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                if (MouseIsInside(mousePosition))
                {
                    selected = Select(mousePosition);
                    MutateTile(selected);
                }
                isPressed = true;
            }
            else if (isPressed && Mouse.GetState().LeftButton == ButtonState.Released)
            {
                isPressed = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Fetch the selected tile's coordinate. Uses tiling as base instead of screen pixels.
        /// </summary>
        /// <param name="mousePosition">Position of mouse</param>
        /// <returns>Point coordinates with tiling as base</returns>
        public Point Select(Point mousePosition)
        {
            int xSelect = (mousePosition.X - xOffset) / tileWidth;
            int ySelect = (mousePosition.Y - yOffset) / tileSize;
            return new Point(xSelect, ySelect);
        }

        /// <summary>
        /// Mutates the selected tile state
        /// </summary>
        /// <param name="selected">The selected tile's coordinates with tiling as base</param>
        public void MutateTile(Point selected)
        {
            int x = selected.X;
            int y = selected.Y;
            tileContent[x, y] = tileContent[x, y] + 1;
        }

        public Point SelectedTile
        {
            get
            {
                return selected;
            }
        }
    }
}