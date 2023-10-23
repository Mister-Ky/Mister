using System;
using System.IO;

namespace Mister.Utils
{
    public sealed class UtilsFolder
    {
        public static void ReFolder(string folder)
        {
            if (!Directory.Exists(folder) && !string.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }
}
