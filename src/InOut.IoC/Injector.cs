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
            serviceColletion.AddScoped<IAccountRepository, AccountRepository>();
            serviceColletion.AddScoped<IBranchRepository, BranchRepository>();
            serviceColletion.AddScoped<ILocationRepository, LocationRepository>();
            serviceColletion.AddScoped<IMovementRepository, MovementRepository>();
            serviceColletion.AddScoped<IProductRepository, ProductRepository>();
            serviceColletion.AddScoped<IProviderRepository, ProviderRepository>();
            serviceColletion.AddScoped<IUserRepository, UserRepository>();
        }

        private static void InjectServices(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<IAccountService, AccountService>();
            serviceColletion.AddScoped<IBranchService, BranchService>();
            serviceColletion.AddScoped<ILocationService, LocationService>();
            serviceColletion.AddScoped<IMovementService, MovementService>();
            serviceColletion.AddScoped<IProductService, ProductService>();
            serviceColletion.AddScoped<IProviderService, ProviderService>();
            serviceColletion.AddScoped<IUserService, UserService>();
        }

        private static void InjectGenerics(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<ITokenGenerator, TokenGenerator>();
            serviceColletion.AddSingleton(MappingProfile.CreateMappingProfile().CreateMapper());
        }
    }
}
