using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class SnakePart
    {
        private Direction _direction;
        private Point _position;
        private float _rotation;

        public float Rotation { get { return _rotation; } set { _rotation = value; } }
        public Direction Direction { get { return _direction; } set { _direction = value; } }
        public Point Position { get { return _position; } set { _position = value; } }

        public SnakePart(Direction direction, Point position)
        {
            _direction = direction;
            _position = position;
            _rotation = 0;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Game1.SnakePartTexture,
                new Rectangle(
                    _position.X * Game1.GRID_SIZE, 
                    _position.Y * Game1.GRID_SIZE, 
                    Game1.GRID_SIZE, 
                    Game1.GRID_SIZE),
                Color.White);
        }
    }
}
