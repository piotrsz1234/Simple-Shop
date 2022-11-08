using System;
using System.Text.RegularExpressions;

namespace Common.Bll.Helpers
{
    public static class Extensions
    {
        public static bool IsMoneyFormat(string? text)
        {
            if (string.IsNullOrEmpty(text)) return true;

            if (decimal.TryParse(text, out var price))
            {
                if ((price * 1000) % 10 != 0)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public static bool IsInteger(string? text)
        {
            if (string.IsNullOrEmpty(text)) return true;
            
            return int.TryParse(text, out _);
        }
    }
}