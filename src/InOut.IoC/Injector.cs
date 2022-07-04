using InOut.Domain.Entities;
using InOut.Infrastructure.Repositories;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services;
using InOut.Service.Services.Interfaces;
using InOut.Service.Token;
using InOut.Service.Token.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InOut.IoC
{
    public static class Injector
    {
        public static void InjectIoCServices(IServiceCollection serviceColletion)
        {
            InjectRepositories(serviceColletion);
            InjectServices(serviceColletion);
            InjectGenerics(serviceColletion);
        }

        private static void InjectRepositories(IServiceCollection serviceColletion)
        {
            serviceColletion.AddTransient<IAccountRepository, AccountRepository>();
            serviceColletion.AddTransient<IBranchRepository, BranchRepository>();
            serviceColletion.AddTransient<ILocationRepository, LocationRepository>();
            serviceColletion.AddTransient<IMovementRepository, MovementRepository>();
            serviceColletion.AddTransient<IProductRepository, ProductRepository>();
            serviceColletion.AddTransient<IProviderRepository, ProviderRepository>();
            serviceColletion.AddTransient<IUserRepository, UserRepository>();
        }

        private static void InjectServices(IServiceCollection serviceColletion)
        {
            serviceColletion.AddTransient<IAccountService, AccountService>();
            serviceColletion.AddTransient<IBranchService, BranchService>();
            serviceColletion.AddTransient<ILocationService, LocationService>();
            serviceColletion.AddTransient<IMovementService, MovementService>();
            serviceColletion.AddTransient<IProductService, ProductService>();
            serviceColletion.AddTransient<IProviderService, ProviderService>();
            serviceColletion.AddTransient<IUserService, UserService>();
        }

        private static void InjectGenerics(IServiceCollection serviceColletion)
        {
            serviceColletion.AddTransient<ITokenGenerator, TokenGenerator>();
        }
    }
}
