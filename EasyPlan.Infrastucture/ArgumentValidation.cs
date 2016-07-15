using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPlan.Infrastructure
{
    public static class ArgumentValidation
    {
        public static void ThrowIfNullOrEmpty(string value, string argumentName = "")
        {
            ThrowIfNull(value, argumentName);

            if (value.Trim() == String.Empty)
                throw new ArgumentException($"The value {argumentName} can't be empty");
        }

        public static void ThrowIfNull(object value, string argumentName = "")
        {
            if (value == null)
                throw new ArgumentNullException($"The value {argumentName} can't be null");
        }

        public static void ThrowIfLongerThan(string value, uint length, string argumentName = "")
        {
            if (value.Length > length)
                throw new ArgumentException($"The value {argumentName} can't be longer than {length}");
        }

        public static void ThrowIfGreaterThan(int value, int max, string argumentName = "")
        {
            if (value > max)
                throw new ArgumentOutOfRangeException($"The value {argumentName} can't be greater than {max}");
        }

        public static void ThrowIfLessThan(int value, int min, string argumentName = "")
        {
            if (value < min)
                throw new ArgumentOutOfRangeException($"The value {argumentName} can't be less than {min}");
        }

        public static void ThrowIfOutOfRange(int value, int min, int max, string argumentName = "")
        {
            ThrowIfGreaterThan(value, max, argumentName);
            ThrowIfLessThan(value, min, argumentName);
        }
    }
}
