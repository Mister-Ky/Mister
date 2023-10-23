using Mister.ModSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Mister.Utils
{
    public sealed class ModReaderUtils
    {
        public static List<IMod> ModAllReader(string folder, string ex)
        {
            var modList = new List<IMod>();
            var files = Directory.GetFiles(folder, ex);
            //var files = Directory.GetFiles("mods", "*.dll");

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(Path.Combine(Directory.GetCurrentDirectory(), file));
                ReadModAssembly(modList, assembly);
            }
            return modList;
        }

        public static List<IMod> ModAllReader(byte[] assemblyBytes)
        {
            var modList = new List<IMod>();
            var assembly = Assembly.Load(assemblyBytes);
            ReadModAssembly(modList, assembly);
            return modList;
        }

        public static IMod ModReader(string folder, string ex)
        {
            var assembly = Assembly.LoadFrom(Path.Combine(Directory.GetCurrentDirectory(), $"{folder}{ex}"));
            //var assembly = Assembly.LoadFrom(Path.Combine(Directory.GetCurrentDirectory(), $"{Mods/}{mod.dll}"));
            //In the example, the path to fashion - Mods/mod.dll, in the folder = "Mods/", in the ex = "mod.dll"
            return ReadModAssembly(assembly);
        }

        public static IMod ModReader(byte[] assemblyBytes)
        {
            var assembly = Assembly.Load(assemblyBytes);
            return ReadModAssembly(assembly);
        }

        private static void ReadModAssembly(List<IMod> modList, Assembly assembly)
        {
            var modTypes = assembly.GetTypes().Where(t => typeof(IMod).IsAssignableFrom(t) && !t.IsInterface).ToArray();

            foreach (var modType in modTypes)
            {
                var modInstance = Activator.CreateInstance(modType) as IMod;
                modList.Add(modInstance);
            }
        }

        private static IMod ReadModAssembly(Assembly assembly)
        {
            var modType = assembly.GetTypes().FirstOrDefault(t => typeof(IMod).IsAssignableFrom(t) && !t.IsInterface);
            return modType != null ? Activator.CreateInstance(modType) as IMod : null;
        }
    }
}