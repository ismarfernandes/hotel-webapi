using System;
using System.Collections.Generic;

namespace Hotel.Shared.Extensions
{
    public static class ExceptionExtension
    {
        public static IEnumerable<string> InnerExceptionsMessages(this Exception ex)
        {
            var exception = ex;

            do
            {
                yield return exception?.Message;
                exception = exception?.InnerException;
            } while (exception != null);
        }
    }
}
