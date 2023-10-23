using Mister.Utils;
using System;

namespace Mister.Framework.Input
{
    public class Mouse
    {
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;

        public bool IsLeftButtonDown()
        {
            return (WindowsApi.GetKeyState(0x01) & 0x80) != 0;
        }

        public bool IsRightButtonDown()
        {
            return (WindowsApi.GetKeyState(0x02) & 0x80) != 0;
        }

        public POINT GetCursorPosition()
        {
            POINT point;
            WindowsApi.GetCursorPos(out point);
            return point;
        }
    }
}
