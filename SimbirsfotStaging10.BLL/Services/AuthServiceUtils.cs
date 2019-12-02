using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.BLL.VK;

namespace SimbirsfotStaging10.BLL.Services
{
    public delegate IAuthService AuthServiceResolver(AuthServices serviceType);
    public static class AuthServiceUtils
    {
        public static void AddAuthServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<VkAuth>();
            serviceCollection.AddTransient<AuthServiceResolver>(provider => (serviceType) =>
                {
                    switch (serviceType)
                    {
                        case (AuthServices.Vk):
                            return provider.GetRequiredService<VkAuth>();
                        default:
                            return null;
                    }
                }
            );
        }

        public static AuthServices StringToEnumElement(string name)
        {
            switch (name)
            {
                case ("Vk"):
                    return AuthServices.Vk;
                default:
                    throw new InvalidOperationException("Auth service with such name doesn't exist.");
            }
        }
    }

    public enum AuthServices
    {
        Vk
    }
}
