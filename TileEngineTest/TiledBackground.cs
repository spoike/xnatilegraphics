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
        private TileGraphic tiles;
        private Texture2D selection;
        private SpriteBatch spriteBatch;
        private int xOffset;
        private int yOffset;
        private int xSelect = 0;
        private int ySelect = 0;
        private int height = 10;
        private int length = 10;
        private int tileSize;
        private int[,] tileContent;

        public TiledBackground(Game game, int xOffset, int yOffset)
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
            tiles = new TileGraphic(Game.Content.Load<Texture2D>("tiles"), 2, 1);
            tileSize = tiles.TileWidth;
            selection = Game.Content.Load<Texture2D>("selection_tile");
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
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        spriteBatch.Draw(
                            tiles.Texture,
                            new Vector2(i * tileSize + xOffset, j * tileSize + yOffset),
                            tiles.Tile(tileContent[i, j], 0),
                            Color.White);
                    }
                }
                spriteBatch.Draw(
                    selection, 
                    new Vector2(xSelect * tileSize + xOffset, ySelect * tileSize + yOffset), 
                    Color.White);

                spriteBatch.End();
            }
        }

        private bool isPressed = false;

        public override void Update(GameTime gameTime)
        {
            if (!isPressed && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                int x = Mouse.GetState().X;
                int y = Mouse.GetState().Y;
                if (MouseIsInside(x, y))
                {
                    xSelect = (x - xOffset) / tileSize;
                    ySelect = (y - yOffset) / tileSize;
                    tileContent[xSelect, ySelect] = tileContent[xSelect, ySelect] + 1;
                }
                isPressed = true;
            }
            else if (isPressed && Mouse.GetState().LeftButton == ButtonState.Released)
            {
                isPressed = false;
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