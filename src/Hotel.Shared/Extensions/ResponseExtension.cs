using System.Collections.Generic;

using Hotel.Shared.Enumerators;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Responses;

namespace Hotel.Shared.Extensions
{
    public static class ResponseExtension
    {
        #region Attributes
        private const string failureMessage = "A failure occurred";
        private const string successMessage = "Completed successfully";
        private const string validationSuccessedMessage = "Validations succeed";
        private const string validationFailureMessage = "Validations failed";
        #endregion

        public static IResponse FailureResponse(this IEnumerable<string> errors)
        {
            return new Response
            {
                Result = errors,
                Message = failureMessage,
                Success = false,
                Type = ResponseType.Error
            };
        }

        public static IResponse SuccessResponse(this object result, string message = null)
        {
            return new Response
            {
                Result = result,
                Message = message ?? successMessage,
                Success = true,
                Type = ResponseType.Success
            };
        }

        public static IResponse ValidationFailureResponse(this string validationMessage)
        {
            return new Response
            {
                Result = new string[] { validationMessage },
                Message = validationFailureMessage,
                Success = false,
                Type = ResponseType.Validation
            };
        }

        public static IResponse ValidationFailureResponse(this object result)
        {
            return new Response
            {
                Result = result,
                Message = validationFailureMessage,
                Success = false,
                Type = ResponseType.Validation
            };
        }
    }
}
