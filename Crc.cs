using System;

namespace Mister.Utils.Cryptography
{
    public sealed class Crc
    {
        public static byte[] AddCrc(byte[] data)
        {
            uint crc32Result = CalculateCrc32(data);
            byte[] crcBytes = BitConverter.GetBytes(crc32Result);
            byte[] resultBytes = new byte[data.Length + crcBytes.Length];
            data.CopyTo(resultBytes, 0);
            crcBytes.CopyTo(resultBytes, data.Length);
            return resultBytes;
        }

        public static byte[] RemoveCrc(byte[] data)
        {
            byte[] resultBytes = new byte[data.Length - 4];
            Array.Copy(data, 0, resultBytes, 0, resultBytes.Length);
            return resultBytes;
        }

        private static uint[] crcTable = GenerateCrcTable();

        private static uint[] GenerateCrcTable()
        {
            uint[] crcTable = new uint[256];
            const uint poly = 0xedb88320u;
            for (uint i = 0; i < crcTable.Length; ++i)
            {
                uint crc = i;
                for (int j = 8; j > 0; --j)
                {
                    if ((crc & 1) == 1)
                    {
                        crc = (crc >> 1) ^ poly;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
                crcTable[i] = crc;
            }
            return crcTable;
        }

        public static uint CalculateCrc32(byte[] bytes)
        {
            uint crc = 0xffffffffu;
            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(((crc) & 0xff) ^ bytes[i]);
                crc = (crc >> 8) ^ crcTable[index];
            }
            return crc ^ 0xffffffffu;
        }
    }
}