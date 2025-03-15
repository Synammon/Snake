using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Snake
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Snake _snake;
        private static int _highScore;

        public const int WIDTH = 840;
        public const int HEIGHT = 840;
        public const int GRID_SIZE = 40;

        public static int HighScore
        {
            get { return _highScore; }
            set
            {
                if (_highScore < value)
                {
                    _highScore = value;
                    WriteHighScore(_highScore);
                }
            }
        }

        public static Texture2D FoodTexture { get; private set; }
        public static Texture2D SnakePartTexture { get; private set; }
        public static Texture2D HeadUpTexture { get; private set; }
        public static Texture2D HeadDownTexture { get; private set; }
        public static Texture2D HeadLeftTexture { get; private set; }
        public static Texture2D HeadRightTexture { get; private set; }

        public static Random Random = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT;
            _graphics.ApplyChanges();

            _highScore = 1;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            HeadUpTexture = Content.Load<Texture2D>("head-up");
            HeadDownTexture = Content.Load<Texture2D>("head-down");
            HeadLeftTexture = Content.Load<Texture2D>("head-left");
            HeadRightTexture = Content.Load<Texture2D>("head-right");

            SnakePartTexture = Content.Load<Texture2D>("body");
            FoodTexture = Content.Load<Texture2D>("food");

            HighScore = ReadHighScore();

            _snake = new Snake();
            _snake.Reset();
            Board _board = new Board();

            Board.Reset();

            Board.Cells[10, 10].Contents = CellContents.Snake;
            Board.AddFood();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            Window.Title = $"Snake Length: { Snake.Parts.Count } " + $"High Score: { _highScore }";
            _snake.Update(gameTime, Keyboard.GetState());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            base.Draw(gameTime);

            Board.Draw(_spriteBatch);

            _snake.Draw(_spriteBatch);

            _spriteBatch.End();
        }

        private static int ReadHighScore()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SnakeHighScore.txt");
            if (File.Exists(path))
            {
                string highScoreText = File.ReadAllText(path);
                if (int.TryParse(highScoreText, out int highScore))
                {
                    return highScore;
                }
            }
            return 0;
        }

        private static void WriteHighScore(int highScore)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SnakeHighScore.txt");
            File.WriteAllText(path, highScore.ToString());
        }
    }
}
