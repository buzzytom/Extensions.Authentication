using Extensions.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

        /// <summary>
        /// Adds authentication and JWT authentication to the services collection with the specified configuration.
        /// This automatically sets up key private key for issuing.
        /// </summary>
        /// <param name="services">The services to add JWT to.</param>
        /// <param name="configuration">The JWT configuration to use.</param>
        public static void AddJwt(this IServiceCollection services, JwtConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            // Register the configuration as a service
            services.AddSingleton(configuration);

            // Create the symmetric key used in JWT validation
            SymmetricKeyProvider key = new SymmetricKeyProvider();
            services.AddSingleton<ISymmetricKeyProvider>(key);

            // Add authentication services
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.Issuer,
                        ValidAudience = configuration.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key.Key)
                    };
                });
        }
    }
}
