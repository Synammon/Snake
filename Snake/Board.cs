using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Board
    {
        public const int WIDTH = 21;
        public const int HEIGHT = 21;
        public static Cell[,] Cells = new Cell[WIDTH, HEIGHT];
        public static Point Food { get; private set; }
        public static Rectangle FoodDestination { get { return new Rectangle(Food.X * Game1.GRID_SIZE, Food.Y * Game1.GRID_SIZE, Game1.GRID_SIZE, Game1.GRID_SIZE); } }

        public Board()
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    Cells[x, y] = new Cell();
                }
            }
        }

        public static void Update(GameTime gameTime)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    Cells[x, y].Update(gameTime);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    Cells[x, y].Draw(spriteBatch, x, y);
                }
            }
        }

        public static void Reset()
        {
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    Board.Cells[i, j] = new Cell();
                    Board.Cells[i, j].Contents = CellContents.Empty;
                }
            }
        }

        public static void AddFood()
        {
            int x = Game1.Random.Next(0, WIDTH);
            int y = Game1.Random.Next(0, HEIGHT);

            while (Cells[x, y].Contents != CellContents.Empty || Snake.Parts.Any(s => s.Position == new Point(x, y)))
            {
                x = Game1.Random.Next(0, WIDTH);
                y = Game1.Random.Next(0, HEIGHT);
            }

            Cells[x, y].Contents = CellContents.Food;
            Food = new Point(x, y);
        }   
    }
}
