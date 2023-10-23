using Mister.Utils;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mister.Framework
{
    public sealed class Graphics
    {
        private WindowProcDelegate windowProcDelegate;

        public Initialize initialize;
        public Update update;
        public Draw draw;
        public Exit exit;


        private const uint SWP_NOMOVE = 0x0002;
        private const int PS_SOLID = 0;

        // HWND_MESSAGE constant
        private static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);

        // Window styles
        private const int WS_OVERLAPPED = 0x00000000;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_SYSMENU = 0x00080000;
        private const int WS_MINIMIZEBOX = 0x00020000;
        private const int WS_MAXIMIZEBOX = 0x00010000;
        private const int WS_THICKFRAME = 0x00040000;
        private const int WS_VISIBLE = 0x10000000;

        // Window messages
        private const int WM_PAINT = 0x000F;
        private const int WM_CLOSE = 0x0010;
        private const int WM_QUIT = 0x0012;

        private const int WM_CREATE = 0x0001;
        private const int WM_DESTROY = 0x0002;

        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_COMMAND = 0x0111;
        private const int WM_TIMER = 0x0113;

        private const int SWP_NOSIZE = 0x0001;

        public IntPtr hWnd;
        public IntPtr hdc;

        public int FPS_LIMIT = 60;

        public WNDCLASS wc = new WNDCLASS();
        public bool isCursorVisible = true;
        private GameTime gameTime;
        public int width { get; private set; }
        public int height { get; private set; }
        public int x { get; private set; }
        public int y { get; private set; }

        public void Run(string WindowsName)
        {
            width = 800;
            height = 600;
            x = 100;
            y = 100;
            
            gameTime = new GameTime();
            initialize();
            CreateMainWindow(WindowsName);
            MessageLoop();
        }

        private void CreateMainWindow(string WindowsName)
        {
            string className = "MisterGraphics";

            // Регистрируем класс окна
            wc.style = 0;
            windowProcDelegate = new WindowProcDelegate(WindowProc);
            wc.lpfnWndProc = windowProcDelegate;
            wc.cbClsExtra = 0;
            wc.cbWndExtra = 0;
            wc.hInstance = Marshal.GetHINSTANCE(GetType().Module);
            wc.hIcon = IntPtr.Zero;
            wc.hCursor = IntPtr.Zero;
            wc.hbrBackground = IntPtr.Zero;
            wc.lpszMenuName = null;
            wc.lpszClassName = className;
            WindowsApi.RegisterClass(ref wc);

            // Создаем окно
            hWnd = WindowsApi.CreateWindowEx(0, className, $"{WindowsName}", WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX | WS_VISIBLE, x, y, width, height, IntPtr.Zero, IntPtr.Zero, wc.hInstance, IntPtr.Zero);
            WindowsApi.ShowWindow(hWnd, 1);
            WindowsApi.UpdateWindow(hWnd);

            // Получаем контекст устройства (Device Context)
            hdc = WindowsApi.GetDC(hWnd);
        }

        private void MessageLoop()
        {
            MSG msg;
            gameTime.Start();

            while (WindowsApi.GetMessage(out msg, IntPtr.Zero, 0, 0))
            {
                gameTime.Update();
                WindowsApi.TranslateMessage(ref msg);
                WindowsApi.DispatchMessage(ref msg);
                WindowsApi.ShowCursor(isCursorVisible ? 1 : 0);
                update(gameTime);
                UpdateHdc();
                draw(gameTime);
                Thread.Sleep(1000 / FPS_LIMIT);
            }
        }

        private IntPtr WindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case WM_PAINT:
                    // Рисуем на окне
                    update(gameTime);
                    UpdateHdc();
                    draw(gameTime);

                    // Инвалидируем окно для повторной отрисовки
                    InvalidateRectWind();
                    break;

                case WM_CLOSE:
                case WM_QUIT:
                    // Завершаем программу при закрытии окна
                    exit();
                    Environment.Exit(0);
                    break;
            }

            return WindowsApi.DefWindowProc(hWnd, msg, wParam, lParam);
        }

        public void SetWindowPosition(int x, int y)
        {
            WindowsApi.SetWindowPos(hWnd, IntPtr.Zero, x, y, 0, 0, SWP_NOSIZE);
            this.x = x;
            this.y = y;
            UpdateHdc();
        }

        public void WindowsTitle(string windowsTitle)
        {
            WindowsApi.SetWindowText(hWnd, windowsTitle);
            UpdateHdc();
        }

        public void ResizeWindow(int width, int height)
        {
            WindowsApi.SetWindowPos(hWnd, IntPtr.Zero, 0, 0, width, height, SWP_NOMOVE);
            this.width = width;
            this.height = height;
            UpdateHdc();
        }

        public void SetWindowPositionInit(int x, int y)
        {
            WindowsApi.SetWindowPos(hWnd, IntPtr.Zero, x, y, 0, 0, SWP_NOSIZE);
            this.x = x;
            this.y = y;
        }

        public void WindowsTitleInit(string windowsTitle)
        {
            WindowsApi.SetWindowText(hWnd, windowsTitle);
        }

        public void ResizeWindowInit(int width, int height)
        {
            WindowsApi.SetWindowPos(hWnd, IntPtr.Zero, 0, 0, width, height, SWP_NOMOVE);
            this.width = width;
            this.height = height;
        }

        public void ReleaseDCWind()
        {
            WindowsApi.ReleaseDC(hWnd, hdc);
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public void UpdateHdc()
        {
            hdc = WindowsApi.GetDC(hWnd);
        }

        public void InvalidateRectWind()
        {
            WindowsApi.InvalidateRect(hWnd, IntPtr.Zero, false);
        }
    }

    // Делегат для обработчика оконных сообщений
    public delegate IntPtr WindowProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);


    public delegate void Initialize();
    public delegate void Update(GameTime gameTime);
    public delegate void Draw(GameTime gameTime);

    public delegate void Exit();
}
