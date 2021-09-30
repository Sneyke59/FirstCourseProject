namespace Framework.Extensions
{
    using System;
    using UnityEngine;

    public static class StringExtension
    {
        public static string ToHexColor(this string str)
        {
            str = str.Replace(" ", string.Empty);

            System.Random random = new System.Random(str.GetHashCode());

            string randStr = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                randStr += str[(int)(random.NextDouble() * str.Length)];
            }

            int hash = 0;

            for (int i = 0; i < randStr.Length; i++)
            {
                hash = randStr.ToCharArray()[i] + ((hash << 5) - hash);
            }

            string colour = "#";

            for (int i = 0; i < 3; i++)
            {
                int value = (hash >> (i * 8)) & 0xFF;
                string tempHex = "00" + Convert.ToString(value, 16);
                colour += tempHex.Substring(tempHex.Length - 2);
            }

            return colour;
        }

        public static Color ToColor(this string str)
        {
            string hexColor = str.ToHexColor();

            ColorUtility.TryParseHtmlString(hexColor, out Color color);

            return color;
        }
    }
}
