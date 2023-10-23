using System;
using System.Runtime.InteropServices;

namespace Mister.Utils
{
    //WindowsApiBeta.cs:

    public static partial class WindowsApi
    {
        //beta:

        [DllImport("winmm.dll")]
        public static extern bool PlaySound(byte[] sound, IntPtr hMod, uint flags);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Beep(uint frequency, uint duration);
    }
}
