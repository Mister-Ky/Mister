using Mister.Framework;
using System;

namespace Mister.ModSystem
{
    public interface IMod
    {
        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

        void Load();
        void Unload();
    }

    public interface IModFramework : IMod
    {
        void Init();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void Exit();
        void Dispose();
    }
}
