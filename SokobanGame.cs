
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace first_mono_game
{
    internal class SokobanGame : Game
    {
        private char[,] testLevel;


        public const int WIDTH = 960 / 4;
        public const int HEIGHT = 540 / 4;
        public const int GAME_UPSCALE = 2 * 2;
        public const int CELL_SIZE = 8;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private Board board;

        private Camera camera;

        public SokobanGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = WIDTH * GAME_UPSCALE;
            _graphics.PreferredBackBufferHeight = HEIGHT * GAME_UPSCALE;
            _graphics.ApplyChanges();

            board = new(Content);

            camera = new(board);

            testLevel = new char[10, 10]
            {
                {'O','X','X','O','P','O','X','O','O','O',},
                {'O','X','X','O','O','O','X','O','O','O',},
                {'O','O','O','O','O','O','X','O','X','O',},
                {'O','O','O','O','O','O','X','O','O','O',},
                {'O','O','O','O','O','O','O','O','O','O',},
                {'O','O','O','O','O','O','X','X','X','X',},
                {'O','O','X','O','O','O','O','O','O','O',},
                {'O','O','X','O','O','O','O','O','O','O',},
                {'O','O','X','O','O','O','O','O','O','O',},
                {'O','O','X','O','O','O','O','O','O','O',},

            };
            //testLevel = new char[9, 14]
            //{
            //    {'H','H','H','O','O','O','O','O','O','O','H','H','H','H',},
            //    {'H','H','H','O','O','O','O','O','O','O','O','O','H','H',},
            //    {'O','O','O','O','O','O','O','O','O','O','H','O','H','H',},
            //    {'O','O','O','H','H','H','H','H','H','H','H','O','H','H',},
            //    {'O','O','O','H','H','H','H','H','H','H','H','O','H','H',},
            //    {'O','P','O','H','H','O','O','O','H','H','O','O','O','O',},
            //    {'O','O','O','H','H','O','G','O','H','H','O','O','O','O',},
            //    {'H','H','H','H','H','O','O','O','O','O','O','O','O','O',},
            //    {'H','H','H','H','H','O','O','O','O','O','O','O','O','O',},
            //};

            board.SetBoard(testLevel);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            InputSystem.Update();

            board.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix scalingMatrix = Matrix.CreateScale(GAME_UPSCALE);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: scalingMatrix);

            board.Draw(spriteBatch, camera.GetOffset());


            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}
