﻿using System.Text;

namespace TECHNOVA.Helpers
{
    public class Util
    {
        public static string GenerateRandomKey(int length = 5)
        {
            var pattern = @"qẵzedcrfvtgbyhnujmikolpQAZWSXEDCRFVTGBYHNUJMIKOLP!";

            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }
    }
}
