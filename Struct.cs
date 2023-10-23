using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Mister.Framework
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public POINT pt;
    }

    public enum TernaryRasterOperations : uint
    {
        SRCCOPY = 0x00CC0020
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int cx;
        public int cy;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct WNDCLASS
    {
        public uint style;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public WindowProcDelegate lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public string lpszMenuName;
        public string lpszClassName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFOHEADER
    {
        public uint biSize;
        public int biWidth;
        public int biHeight;
        public ushort biPlanes;
        public ushort biBitCount;
        public uint biCompression;
        public uint biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public uint biClrUsed;
        public uint biClrImportant;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        public uint bmiColors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        public uint cbSize;
        public IntPtr hwnd;
        public uint dwFlags;
        public uint uCount;
        public uint dwTimeout;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2 : IEquatable<Vector2>
    {
        [DataMember]
        public float X;

        [DataMember]
        public float Y;

        public Vector2(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Vector2(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Vector2(float value)
        {
            X = value;
            Y = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                return Equals((Vector2)obj);
            }
            return false;
        }

        public bool Equals(Vector2 other)
        {
            if (X == other.X)
            {
                return Y == other.Y;
            }
            return false;
        }
        public static bool operator ==(Vector2 value1, Vector2 value2)
        {
            if (value1.X == value2.X)
            {
                return value1.Y == value2.Y;
            }
            return false;
        }

        public static bool operator !=(Vector2 value1, Vector2 value2)
        {
            if (value1.X == value2.X)
            {
                return value1.Y != value2.Y;
            }
            return true;
        }

        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IEquatable<Vector3>
    {
        [DataMember]
        public float X;

        [DataMember]
        public float Y;

        [DataMember]
        public float Z;

        public Vector3(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public Vector3(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public Vector3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3)
            {
                return Equals((Vector3)obj);
            }
            return false;
        }

        public bool Equals(Vector3 other)
        {
            if (X == other.X)
            {
                if (Y == other.Y) 
                {
                    return Z == other.Z;
                }
            }
            return false;
        }
        public static bool operator ==(Vector3 value1, Vector3 value2)
        {
            if (value1.X == value2.X)
            {
                if (value1.Y == value2.Y) 
                {
                    return value1.Z == value2.Z;
                }
            }
            return false;
        }
        public static bool operator !=(Vector3 value1, Vector3 value2)
        {
            if (value1.X == value2.X)
            {
                if (value1.Y == value2.Y)
                {
                    return value1.Z != value2.Z;
                }
            }
            return false;
        }

        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Texture2D
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Bitmap Image { get; set; }

        public Texture2D(Bitmap bitmap)
        {
            Width = bitmap.Width;
            Height = bitmap.Height;
            Image = bitmap;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Texture3D
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
        public Bitmap[] Images { get; set; }

        public Texture3D(Bitmap[] bitmaps)
        {
            Width = bitmaps[0].Width;
            Height = bitmaps[0].Height;
            Depth = bitmaps.Length;
            Images = bitmaps;
        }
    }
}
