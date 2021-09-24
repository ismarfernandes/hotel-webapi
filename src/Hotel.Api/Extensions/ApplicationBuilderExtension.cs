using Hotel.Api.Middlewares;

using Microsoft.AspNetCore.Builder;

namespace Hotel.Api.Extensions
{
    public static class ApplicationBuilderExtension
    {
        #region Public Methods
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            return app;
        }
        #endregion
    }
}
