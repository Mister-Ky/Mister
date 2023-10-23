using Mister.Utils;
using System;

namespace Mister.Audio
{
    //Beta class, not working

    public class Audio
    {
        private const uint SND_ASYNC = 0x0001;
        private const uint SND_MEMORY = 0x0004;
        private const uint SND_NODEFAULT = 0x0002;
        private const uint SND_FILENAME = 0x00020000;
        private const uint SND_PURGE = 0x0040;

        public static void PlaySound(byte[] soundData)
        {
            WindowsApi.PlaySound(null, IntPtr.Zero, SND_PURGE);

            WindowsApi.PlaySound(soundData, IntPtr.Zero, SND_ASYNC | SND_MEMORY);
        }

        public static void PlaySystemSound(uint frequency, uint duration)
        {
            WindowsApi.Beep(frequency, duration);
        }
    }
}
