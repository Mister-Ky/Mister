using Mister.Utils;
using System;

namespace Mister.Framework.Input
{
    public class Keyboard
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        public bool IsKeyDown(Keys key)
        {
            return (WindowsApi.GetKeyState((int)key) & 0x8000) != 0;
        }

        public bool IsKeyUp(Keys key)
        {
            return (WindowsApi.GetKeyState((int)key) & 0x8000) == 0;
        }
    }
}
