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
                throw new ArgumentException(String.Format("The value {0} can't be empty", argumentName));
        }

        public static void ThrowIfNull(object value, string argumentName = "")
        {
            if (value == null)
                throw new ArgumentNullException(String.Format("The value {0} can't be null", argumentName));
        }

        public static void ThrowIfLongerThan(string value, uint length, string argumentName = "")
        {
            if (value.Length > length)
                throw new ArgumentException(String.Format("The value {0} can't be longer than {1}", argumentName, length));
        }

        public static void ThrowIfGreaterThan(int value, int max, string argumentName = "")
        {
            if (value > max)
                throw new ArgumentOutOfRangeException(String.Format("The value {0} can't be greater than {1}", argumentName, max));
        }

        public static void ThrowIfLessThan(int value, int min, string argumentName = "")
        {
            if (value < min)
                throw new ArgumentOutOfRangeException(String.Format("The value {0} can't be less than {1}", argumentName, min));
        }

        public static void ThrowIfOutOfRange(int value, int min, int max, string argumentName = "")
        {
            ThrowIfGreaterThan(value, max, argumentName);
            ThrowIfLessThan(value, min, argumentName);
        }
    }
}
