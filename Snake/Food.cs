using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Food
    {
        private Point _position;

        public Food(Point position)
        {
            _position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Game1.FoodTexture, 
                new Rectangle(
                    _position.X * Game1.GRID_SIZE, 
                    _position.Y * Game1.GRID_SIZE, 
                    Game1.GRID_SIZE, 
                    Game1.GRID_SIZE), 
                Color.Red);
        }
    }
}
