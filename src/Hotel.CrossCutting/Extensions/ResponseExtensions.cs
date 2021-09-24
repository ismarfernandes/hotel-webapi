using System.Linq;

using Hotel.Shared.Enumerators;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Responses;

using FluentValidation.Results;

namespace Hotel.CrossCutting.Extensions
{
    public static class ResponseExtensions
    {
        #region Attributes
        private const string validationSuccessedMessage = "Validations succeed";
        private const string validationFailureMessage = "Validations failed";
        #endregion

        public static IResponse ValidationResponse(this ValidationResult validationResult)
        {
            return new Response
            {
                Result = validationResult?.Errors?.Select(s => s.ErrorMessage),
                Message = validationResult.IsValid ? validationSuccessedMessage : validationFailureMessage, 
                Success = validationResult.IsValid,
                Type = ResponseType.Validation
            };
        }
    }
}
