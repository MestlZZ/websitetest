﻿using System;

namespace EasyPlan.Infrastructure
{
    public static class ArgumentValidation
    {
        public static void ThrowIfNullOrWhiteSpace(string value, string argumentName = "")
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentValidationException($"{argumentName} can't be empty");
        }

        public static void ThrowIfNull(object value, string argumentName = "")
        {
            if (value == null)
                throw new ArgumentValidationException($"{argumentName} can't be null");
        }

        public static void ThrowIfLongerThan(string value, uint length, string argumentName = "")
        {
            if (value.Length > length)
                throw new ArgumentValidationException($"{argumentName} can't be longer than {length}");
        }

        public static void ThrowIfGreaterThan(int value, int max, string argumentName = "")
        {
            if (value > max)
                throw new ArgumentValidationException($"{argumentName} can't be greater than {max}");
        }

        public static void ThrowIfLessThan(int value, int min, string argumentName = "")
        {
            if (value < min)
                throw new ArgumentValidationException($"{argumentName} can't be less than {min}");
        }

        public static void ThrowIfOutOfRange(int value, int min, int max, string argumentName = "")
        {
            ThrowIfGreaterThan(value, max, argumentName);
            ThrowIfLessThan(value, min, argumentName);
        }
    }
}
