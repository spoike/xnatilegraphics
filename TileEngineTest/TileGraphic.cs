using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileEngineTest
{
    public class TileGraphic : IDisposable
    {
        private Texture2D graphic;
        private int cols;
        private int rows;
        private int colWidth;
        private int rowHeight;
        private Rectangle[,] sources;

        public TileGraphic(Texture2D graphic, int cols, int rows)
        {
            this.graphic = graphic;
            this.cols = cols;
            colWidth = graphic.Width / cols;
            this.rows = rows;
            rowHeight = graphic.Height / rows;

            sources = new Rectangle[cols, rows];
            for (int i = 0; i < cols; i++)
            {
                sources[i, 0] = new Rectangle(i * colWidth, 0, colWidth, rowHeight);
            }
        }

        public Texture2D Texture
        {
            get
            {
                return graphic;
            }
        }

        public Rectangle Tile(int x, int y)
        {
            return sources[x % cols, y % rows];
        }

        public int TileWidth
        {
            get
            {
                return colWidth;
            }
        }

        public int TileHeight
        {
            get
            {
                return rowHeight;
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            if (graphic != null)
            {
                graphic.Dispose();
            }
        }

        #endregion
    }
}
