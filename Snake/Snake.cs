using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum Direction { Left, Down, Right, Up }

    public class Snake
    {
        private TimeSpan _moveTime = TimeSpan.FromSeconds(.25);
        private TimeSpan _timer = TimeSpan.Zero;
        private int _growthLeft;
        private float _rotation;
        public static List<SnakePart> Parts = new List<SnakePart>();

        public Rectangle Destination 
        { 
            get 
            { 
                return new Rectangle(
                    Parts[0].Position.X * Game1.GRID_SIZE, 
                    Parts[0].Position.Y * Game1.GRID_SIZE, 
                    Game1.GRID_SIZE, 
                    Game1.GRID_SIZE); 
            } 
        }
        public Point Position { get { return Parts[0].Position; } set { Parts[0].Position = value; } }
        
        public Snake()
        {
            _growthLeft = 0;
            _rotation = 0;
            Parts.Add(new SnakePart(Direction.Right, new Point(10, 10)));
        }

        public void Reset()
        {
            Parts.Clear();
            _growthLeft = 0;
            _rotation = 0;
            Parts.Add(new SnakePart(Direction.Right, new Point(10, 10)));
            Parts[0].Rotation = 0;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            _timer += gameTime.ElapsedGameTime;

            if (_timer >= _moveTime)
            {
                Move();
                _timer = TimeSpan.Zero;
                return;
            }

            if (Destination.Intersects(Board.FoodDestination))
            {
                Board.Cells[Parts[0].Position.X, Parts[0].Position.Y].Contents = CellContents.Snake;
                Grow();
                Board.AddFood();
            }

            if (keyboardState.IsKeyDown(Keys.Left) && Parts[0].Direction != Direction.Right)
            {
                Parts[0].Direction = Direction.Left;
                Parts[0].Rotation = Helpers.DegreesToRadians(180f);
            }
            else if (keyboardState.IsKeyDown(Keys.Right) && Parts[0].Direction != Direction.Left)
            {
                Parts[0].Direction = Direction.Right;
                Parts[0].Rotation = Helpers.DegreesToRadians(0);
            }
            else if (keyboardState.IsKeyDown(Keys.Up) && Parts[0].Direction != Direction.Down)
            {
                Parts[0].Direction = Direction.Up;
                Parts[0].Rotation = Helpers.DegreesToRadians(270f);
            }
            else if (keyboardState.IsKeyDown(Keys.Down) && Parts[0].Direction != Direction.Up)
            {
                Parts[0].Direction = Direction.Down;
                Parts[0].Rotation = Helpers.DegreesToRadians(90f);
            }
        }

        private void Move()
        {
            // Move the snake's body
            for (int i = Parts.Count - 1; i > 0; i--)
            {
                Parts[i].Position = Parts[i - 1].Position;
                Parts[i].Direction = Parts[i - 1].Direction;
            }

            // Move the snake's head
            switch (Parts[0].Direction)
            {
                case Direction.Left:
                    Parts[0].Position = new Point(Parts[0].Position.X - 1, Parts[0].Position.Y);
                    Parts[0].Rotation = Helpers.DegreesToRadians(180f);
                    break;
                case Direction.Right:
                    Parts[0].Position = new Point(Parts[0].Position.X + 1, Parts[0].Position.Y);
                    Parts[0].Rotation = Helpers.DegreesToRadians(0);
                    break;
                case Direction.Up:
                    Parts[0].Position = new Point(Parts[0].Position.X, Parts[0].Position.Y - 1);
                    Parts[0].Rotation = Helpers.DegreesToRadians(270f);
                    break;
                case Direction.Down:
                    Parts[0].Position = new Point(Parts[0].Position.X, Parts[0].Position.Y + 1);
                    Parts[0].Rotation = Helpers.DegreesToRadians(90f);
                    break;
            }

            // Check for collisions with the board boundaries
            if (Parts[0].Position.X < 0 || Parts[0].Position.Y < 0 ||
                Parts[0].Position.X >= Board.WIDTH || Parts[0].Position.Y >= Board.HEIGHT)
            {
                if (Parts.Count > Game1.HighScore)
                {
                    Game1.HighScore = Parts.Count;
                }

                Reset();
                return;
            }

            // Check for collisions with itself
            for (int i = 1; i < Parts.Count; i++)
            {
                if (Parts[0].Position == Parts[i].Position)
                {
                    if (Parts.Count > Game1.HighScore)
                    {
                        Game1.HighScore = Parts.Count;
                    }

                    Reset();
                    return;
                }
            }

            // Handle growth
            if (_growthLeft > 0)
            {
                Parts.Add(new SnakePart(Parts[Parts.Count - 1].Direction, Parts[Parts.Count - 1].Position));
                _growthLeft--;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture;
            switch (Parts[0].Direction)
            {
                case Direction.Left:
                    texture = Game1.HeadLeftTexture;
                    break;
                case Direction.Right:
                    texture = Game1.HeadRightTexture;
                    break;
                case Direction.Up:
                    texture = Game1.HeadUpTexture;
                    break;
                default:
                    texture = Game1.HeadDownTexture;
                    break;
            }

            spriteBatch.Draw(
                texture,
                new Rectangle(
                    Parts[0].Position.X * Game1.GRID_SIZE,
                    Parts[0].Position.Y * Game1.GRID_SIZE,
                    Game1.GRID_SIZE, Game1.GRID_SIZE),
                null,
                Color.White);

            if (Parts.Count > 1)
            {

                for (int i = 1; i < Parts.Count; i++)
                {
                    Parts[i].Draw(spriteBatch);
                }
            }
        }

        public void Grow()
        {
            _growthLeft += 3;
        }
    }
}
