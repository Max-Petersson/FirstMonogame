using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_mono_game
{
    internal class SokobanGame : Game1
    {
        public const int WIDTH = 640;
        public const int HEIGHT = 480;
        public const int GAME_UPSCALE = 2;
        public const int CELL_SIZE = 32;
        protected override void Initialize()
        {
            base.Initialize();

        }
    }
}
