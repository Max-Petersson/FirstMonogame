using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_mono_game
{
    internal static class InputSystem
    {
        public static KeyboardState current;
        public static KeyboardState previous;

        public static void Update()
        {
            previous = current;
            current = Keyboard.GetState();
        }

        public static bool IsKeyHeld(Keys key) => current.IsKeyDown(key);

        public static bool IsKeyPressed(Keys key)
        {
            return current.IsKeyDown(key) && !previous.IsKeyDown(key);
        } 


    }
}
