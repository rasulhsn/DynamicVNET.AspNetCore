using Microsoft.Extensions.DependencyInjection;
using System;

namespace DynamicVNET.Lib.AspNetCore
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddProfileValidation(this IServiceCollection services,Action<ProfileValidator> setup)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (setup == null)
                throw new ArgumentNullException(nameof(ProfileValidator));

            ProfileValidator validator = new ProfileValidator();
            setup(validator);
            services.AddSingleton(validator);
            return services;
        }
    }
}
