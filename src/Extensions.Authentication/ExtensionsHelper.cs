using Microsoft.Extensions.DependencyInjection;
using System;

namespace Extensions.Authentication
{
    /// <summary>
    /// Defines methods for the extensions library to add the token service and validation services.
    /// </summary>
    public static class ExtensionsHelper
    {
        /// <summary>
        /// Adds the token service to the specified service collection.
        /// </summary>
        /// <param name="services">The services to add the service to.</param>
        public static void AddTokenService(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<ITokenService, TokenService>();
        }

        /// <summary>
        /// Adds the validation service to the specified service collection.
        /// </summary>
        /// <param name="services">The services to add the service to.</param>
        /// <param name="requirements">The optional requirements to for the validator.</param>
        public static void AddValidationService(this IServiceCollection services, PasswordRequirements requirements = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IValidatorService, ValidatorService>(x => new ValidatorService(requirements ?? PasswordRequirements.Default));
        }
    }
}
