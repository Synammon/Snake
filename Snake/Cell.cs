using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum CellContents { Empty, Snake, Food }

    public class Cell
    {
        public CellContents Contents { get; set; }

        public Cell() 
        {
            Contents = CellContents.Empty;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            if (Contents == CellContents.Food)
            {
                spriteBatch.Draw(Game1.FoodTexture, new Rectangle(x * Game1.GRID_SIZE, y * Game1.GRID_SIZE, Game1.GRID_SIZE, Game1.GRID_SIZE), Color.White);
            }
        }
    }
}
