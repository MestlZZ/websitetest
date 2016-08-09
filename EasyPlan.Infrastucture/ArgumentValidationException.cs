using System;

namespace EasyPlan.Infrastructure
{
    public class ArgumentValidationException : Exception
    {
        public int StatusCode { get; private set; }

        public ArgumentValidationException(string message, int? statusCode = null)
            :base(message)
        {
            StatusCode = statusCode ?? 400;
        }
    }
}
