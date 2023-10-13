using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            ArgumentNullException.ThrowIfNull(stringValue);
            
            var trimmedString = stringValue.Trim();
            
            if (trimmedString.Equals(string.Empty))
            {
                throw new FormatException();
            }

            bool isPositiveSign = GetSign(trimmedString);
            trimmedString = TrimSign(trimmedString);

            int result = 0;
            foreach (char c in trimmedString)
            {
                if (IsDigit(c)) 
                {
                    try
                    {
                        var digit = CharToDigit(c);
                        result = checked(result * 10 + (isPositiveSign ? digit : -digit));
                    }
                    catch (OverflowException)
                    {
                        throw;
                    }
                }
                else
                {
                    throw new FormatException();
                }
            }

            return result;
        }

        private bool IsDigit(char c)
        {
            var shiftedCharUnicode = c - '0';
            return shiftedCharUnicode >= 0 && shiftedCharUnicode <= 9;
        }

        private int CharToDigit(char c)
        {
            return c - '0';
        }

        private bool GetSign(string stringNumber)
        {
            var firstChar = stringNumber[0];
            if (firstChar != '+' && firstChar != '-' && !IsDigit(firstChar))
            {
                throw new FormatException("Passed string argument must have '+', '-' or digit as the first char.");
            }

            return !stringNumber.StartsWith('-');
        }

        private string TrimSign(string stringNumber)
        {
            if (stringNumber.StartsWith('-') || stringNumber.StartsWith('+'))
            {
                return stringNumber.Substring(1);
            }

            return stringNumber;
        }
    }
}