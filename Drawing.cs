using Mister.ShaderGraphics;
using Mister.Utils;
using System;
using System.Drawing;

namespace Mister.Framework
{
    public class Drawing
    {
        private Shader currentShader;
        public SpriteFont FONT;

        private const int PS_SOLID = 0;

        public IntPtr hdc;
        public IntPtr hWnd;

        public Drawing(IntPtr hdc, IntPtr hWnd)
        {
            this.hdc = hdc;
            this.hWnd = hWnd;
        }

        public void DrawLine(int startX, int startY, int endX, int endY, Color color, int lineWidth)
        {
            IntPtr hPen = WindowsApi.CreatePen(PS_SOLID, lineWidth, ColorTranslator.ToWin32(color));
            IntPtr hOldPen = WindowsApi.SelectObject(hdc, hPen);

            WindowsApi.MoveToEx(hdc, startX, startY, IntPtr.Zero);
            WindowsApi.LineTo(hdc, endX, endY);

            WindowsApi.SelectObject(hdc, hOldPen);
            WindowsApi.DeleteObject(hPen);
            WindowsApi.DeleteObject(hOldPen);
        }

        public void DrawImage(Texture2D image, Vector2 pos)
        {
            int width = image.Width;
            int height = image.Height;

            IntPtr hCompatibleDC = WindowsApi.CreateCompatibleDC(hdc);
            IntPtr hBitmap = image.Image.GetHbitmap();
            IntPtr hOldBitmap = WindowsApi.SelectObject(hCompatibleDC, hBitmap);

            WindowsApi.BitBlt(hdc, (int)pos.X, (int)pos.Y, width, height, hCompatibleDC, 0, 0, TernaryRasterOperations.SRCCOPY);

            WindowsApi.SelectObject(hCompatibleDC, hOldBitmap);
            WindowsApi.DeleteObject(hBitmap);
            WindowsApi.DeleteDC(hCompatibleDC);
        }

        public void DrawString(string pathToFont, string text, Vector2 position, int fontSize, Color color)
        {
            FONT = new SpriteFont(pathToFont, fontSize);
            DrawImage(FONT.DrawString(text), position);
        }

        public void DrawString(byte[] memoryFileFont, string text, Vector2 position, int fontSize, Color color)
        {
            FONT = new SpriteFont(memoryFileFont, fontSize);
            DrawImage(FONT.DrawString(text), position);
        }

        public void FillScreen(Color color)
        {
            IntPtr hBrush = WindowsApi.CreatePen(PS_SOLID, 1, ColorTranslator.ToWin32(color));
            RECT rect;
            WindowsApi.GetClientRect(hWnd, out rect);

            WindowsApi.FillRect(hdc, ref rect, hBrush);

            WindowsApi.DeleteObject(hBrush);
        }

        public void DrawCross(Color color, int lineWidth)
        {
            RECT rect;
            WindowsApi.GetClientRect(hWnd, out rect);

            int centerX = rect.Right / 2;
            int centerY = rect.Bottom / 2;

            DrawLine(centerX - 50, centerY, centerX + 50, centerY, color, lineWidth);

            DrawLine(centerX, centerY - 50, centerX, centerY + 50, color, lineWidth);
        }

        public void DrawGrid(Color color, int lineWidth, int spacing)
        {
            RECT rect;
            WindowsApi.GetClientRect(hWnd, out rect);

            int numHorizontalLines = rect.Bottom / spacing;
            int numVerticalLines = rect.Right / spacing;

            for (int i = 1; i < numHorizontalLines; i++)
            {
                int y = i * spacing;
                DrawLine(0, y, rect.Right, y, color, lineWidth);
            }

            for (int j = 1; j < numVerticalLines; j++)
            {
                int x = j * spacing;
                DrawLine(x, 0, x, rect.Bottom, color, lineWidth);
            }
        }

        public void DrawFunction(Func<float, float> function, float startX, float endX, float step, Color color, int lineWidth)
        {
            RECT rect;
            WindowsApi.GetClientRect(hWnd, out rect);

            float x = startX;
            float y = function(x);

            while (x <= endX)
            {
                float nextX = x + step;
                float nextY = function(nextX);

                int screenX = (int)((x - startX) / (endX - startX) * rect.Right);
                int screenY = (int)((1 - (y / rect.Bottom)) * rect.Bottom);

                int nextScreenX = (int)((nextX - startX) / (endX - startX) * rect.Right);
                int nextScreenY = (int)((1 - (nextY / rect.Bottom)) * rect.Bottom);

                DrawLine(screenX, screenY, nextScreenX, nextScreenY, color, lineWidth);

                x = nextX;
                y = nextY;
            }
        }

        private float SquareFunction(float x)
        {
            return x * x;
        }

        public void DrawSquareFunction()
        {
            DrawFunction(SquareFunction, -10, 10, 0.1f, Color.Red, 2);
        }


        public void BeginShader(Shader shader)
        {
            currentShader = shader;
        }

        public void EndShader()
        {
            currentShader = null;
        }
    }
}