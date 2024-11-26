using Microsoft.Xna.Framework.Input;

namespace Pongo3.Utils
{
    internal class KeyboardManager
    {
        private static KeyboardState prevkbstate;
        private static KeyboardState currentkbstate;
        public static void Update()
        {
            prevkbstate = currentkbstate;
            currentkbstate = Keyboard.GetState();
        }
        public static bool IsKeyPress(Keys key)
        {
            return currentkbstate.IsKeyDown(key) && !prevkbstate.IsKeyDown(key);
        }
        public static bool IsKeyDown(Keys key)
        {
            return currentkbstate.IsKeyDown(key);
        }
    }
}
