using Mister.ModSystem;
using Mister.Resource_Container;
using System;
using System.Reflection;

namespace Mister.Framework
{
    public class Main : IDisposable
    {
        private Assembly currentAssembly = Assembly.GetExecutingAssembly();
        private string projectName;

        private bool disposed;
        protected bool _isCursorVisible = true;
        protected int _FPS = 60;
        protected int _frame = 1;

        protected Graphics _graphics;
        protected Drawing _drawing;
        protected ModManager _modManager;
        protected ContainerManager _containerManager;

        public Main()
        {
            disposed = false;

            projectName = currentAssembly.GetName().Name;

            _graphics = new Graphics();
            _drawing = new Drawing(_graphics.hdc, _graphics.hWnd);

            _graphics.initialize = Initialize;
            _graphics.update = Update;
            _graphics.draw = Draw;
            _graphics.exit = Exit;

            _modManager = new ModManager();
            _containerManager = new ContainerManager();
        }

        public void Run()
        {
            _graphics.Run(projectName);
        }

        protected virtual void Initialize()
        {
            LoadContent();
        }

        protected virtual void LoadContent()
        {

        }

        protected virtual void UnloadContent()
        {

        }

        protected virtual void Update(GameTime gameTime)
        {
            _graphics.FPS_LIMIT = _FPS;
            _graphics.isCursorVisible = _isCursorVisible;
            _drawing.hdc = _graphics.hdc;
            _drawing.hWnd = _graphics.hWnd;

            if (_frame >= _FPS)
                _frame = 1;
            else
                _frame++;
        }

        protected virtual void Draw(GameTime gameTime)
        {
            _graphics.InvalidateRectWind();
            _graphics.ReleaseDCWind();
        }

        protected virtual void Exit()
        {
            _modManager.ClearManager();
            _containerManager.ClearManager();
            Dispose();
            Environment.Exit(0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing) 
            {
                _graphics.Exit();
            }

            disposed = true;
        }

        ~Main()
        {
            Dispose(false);
        }
    }
}