using System;
using System.Collections.Generic;

namespace Mister.ModSystem
{
    public sealed class ModManager
    {
        private List<Tuple<string, IMod>> mods = new List<Tuple<string, IMod>>();

        public void Add(string modName, IMod mod)
        {
            mods.Add(new Tuple<string, IMod>(modName, mod));
        }

        public void Delete(string modName)
        {
            Tuple<string, IMod> mod = mods.Find(m => m.Item1 == modName);
            if (mod != null)
            {
                mod.Item2.Unload();
                mods.Remove(mod);
            }
        }

        public void Load(string modName)
        {
            Tuple<string, IMod> mod = mods.Find(m => m.Item1 == modName);
            if (mod != null)
            {
                mod.Item2.Load();
            }
        }

        public void Unload(string modName)
        {
            Tuple<string, IMod> mod = mods.Find(m => m.Item1 == modName);
            if (mod != null)
            {
                mod.Item2.Unload();
            }
        }

        public void UnloadAll()
        {
            foreach (Tuple<string, IMod> mod in mods)
            {
                mod.Item2.Unload();
            }
        }

        public void ClearManager()
        {
            UnloadAll();
            mods.Clear();
        }
    }
}