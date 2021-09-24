using System.Threading.Tasks;

using Hotel.CrossCutting.Extensions;
using Hotel.Shared.Interfaces;

using FluentValidation;

namespace Hotel.CrossCutting.Validators
{
    public abstract class Validator<T> : AbstractValidator<T> where T : class
    {
        public async Task<IResponse> ValidationAsync(T entity)
        {
            var validationResult = await ValidateAsync(entity);
            return validationResult.ValidationResponse();
        }
    }
}
