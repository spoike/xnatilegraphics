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
        //Rectangle[,] sourceRectangles;
        List<Rectangle> rects;
        Texture2D tiles;
        Texture2D selection;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;
            rects = new List<Rectangle>(10);

            for (int i = 0; i < 2; i++)
            {
                rects.Add( new Rectangle(i * 40, 0, 40, 40) );
            }

            //sourceRectangles = new Rectangle[2, 1];
            //for (int i = 0; i < 2; i++)
            //{
            //    sourceRectangles[i, 0] = new Rectangle(i*40, 0, 40, 40);
            //}


        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tiles = Content.Load<Texture2D>("080502_tiles");
            selection = Content.Load<Texture2D>("selection_tile");
            font = Content.Load<SpriteFont>("Lucida Console");
        }

        protected override void UnloadContent()
        {
            if (tiles != null)
                tiles.Dispose();
            if (selection != null)
                selection.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                int x = Mouse.GetState().X;
                int y = Mouse.GetState().Y;
                if ( MouseIsInside(x,y) )
                {
                    xSelect = (x - xOffset) / tileSize;
                    ySelect = (y - yOffset) / tileSize;
                }
            }

            base.Update(gameTime);
        }

        protected Boolean MouseIsInside(int x, int y)
        {
            return x > xOffset && y > yOffset && x < (length * tileSize) + xOffset && y < (height * tileSize) + yOffset;
        }

        //Rectangle tile1 = new Rectangle(0, 0, 40, 40);
        //Rectangle tile2 = new Rectangle(40, 0, 40, 40);
        int ySelect = 0;
        int xSelect = 0;
        int yOffset = 25;
        int xOffset = 10;
        int height = 10;
        int length = 10;
        int tileSize = 40;

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            int size = height * length;
            for (int i = 0; i < size; i++)
            {
                spriteBatch.Draw(tiles, new Vector2(i*40%(length*40) + xOffset, (i/length)*40 + yOffset), rects[0], Color.White);
            }
            spriteBatch.Draw(selection, new Vector2(xSelect*40 + xOffset, ySelect*40 + yOffset), Color.White);
            spriteBatch.DrawString(font, "Hai Wurldz. You have selected tile " + ySelect + "," + xSelect + ".", new Vector2(1.0f, 1.0f), Color.Black); 
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
