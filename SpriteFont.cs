using Mister.Utils;
using System;
using System.Drawing;

namespace Mister.Framework
{
    public class SpriteFont
    {
        private IntPtr hdc;
        private IntPtr hFont;

        public SpriteFont(string fontFilePath, int fontSize)
        {
            hdc = WindowsApi.GetDC(WindowsApi.GetActiveWindow());
        }

        public SpriteFont(byte[] fontData, int fontSize)
        {
            hdc = WindowsApi.GetDC(WindowsApi.GetActiveWindow());
        }

        public Texture2D DrawString(string text)
        {
            Texture2D texture = new Texture2D();
            return texture;
        }
    }
}