using System;
using System.Linq;
using System.Text;

namespace Mister.Utils.Generator
{
    public sealed class PasswordGenerator
    {
        private static Random random = new Random();

        public static string Generate(int lowercaseCount, int uppercaseCount,
                                              int digitCount, int specialCharCount, int length)
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*()";

            // Проверяем, достаточно ли символов для генерации пароля
            if (length < lowercaseCount + uppercaseCount + digitCount + specialCharCount)
            {
                throw new ArgumentException("Недостаточная длина для указанных параметров.");
            }

            StringBuilder password = new StringBuilder(length);

            // Генерируем символы в нижнем регистре
            for (int i = 0; i < lowercaseCount; i++)
            {
                password.Append(lowercaseChars[random.Next(lowercaseChars.Length)]);
            }

            // Генерируем символы в верхнем регистре
            for (int i = 0; i < uppercaseCount; i++)
            {
                password.Append(uppercaseChars[random.Next(uppercaseChars.Length)]);
            }

            // Генерируем цифры
            for (int i = 0; i < digitCount; i++)
            {
                password.Append(digitChars[random.Next(digitChars.Length)]);
            }

            // Генерируем специальные символы
            for (int i = 0; i < specialCharCount; i++)
            {
                password.Append(specialChars[random.Next(specialChars.Length)]);
            }

            // Заполняем оставшуюся часть пароля случайными символами
            for (int i = password.Length; i < length; i++)
            {
                string availableChars = lowercaseChars + uppercaseChars + digitChars + specialChars;
                password.Append(availableChars[random.Next(availableChars.Length)]);
            }

            // Перемешиваем символы пароля
            string generatedPassword = new string(password.ToString().OrderBy(x => random.Next()).ToArray());

            return generatedPassword;
        }
    }
}
