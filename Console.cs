using System;

namespace Mister.Utils
{
    public sealed class SysCons
    {
        public static void WL(string? text)
        {
            Console.WriteLine(text);
        }
        public static void W(string? text)
        {
            Console.Write(text);
        }

        public static void ErrorWL(string? text)
        {
            Console.Error.WriteLine(text);
        }
        public static void ErrorW(string? text)
        {
            Console.Error.Write(text);
        }

        public static void WL(int? text)
        {
            Console.WriteLine(text);
        }
        public static void W(int? text)
        {
            Console.Write(text);
        }

        public static void ErrorWL(int? text)
        {
            Console.Error.WriteLine(text);
        }
        public static void ErrorW(int? text)
        {
            Console.Error.Write(text);
        }

        public static void Clear()
        {
            Console.Clear();
        }
    }
}
