using InOut.Infrastructure.Repositories;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services;
using InOut.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InOut.IoC
{
    public abstract class Injector
    {
        public static void InjectIoCServices(IServiceCollection serviceColletion)
        {
            InjectRepositories(serviceColletion);
            InjectServices(serviceColletion);
        }

        public static void InjectRepositories(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<IAccountRepository, AccountRepository>();
            serviceColletion.AddScoped<IBranchRepository, BranchRepository>();
            serviceColletion.AddScoped<ILocationRepository, LocationRepository>();
            serviceColletion.AddScoped<IMovementRepository, MovementRepository>();
            serviceColletion.AddScoped<IProductRepository, ProductRepository>();
            serviceColletion.AddScoped<IProviderRepository, ProviderRepository>();
            serviceColletion.AddScoped<IUserRepository, UserRepository>();
        }

        public static void InjectServices(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<IAccountService, AccountService>();
            serviceColletion.AddScoped<IBranchService, BranchService>();
            serviceColletion.AddScoped<ILocationService, LocationService>();
            serviceColletion.AddScoped<IMovementService, MovementService>();
            serviceColletion.AddScoped<IProductService, ProductService>();
            serviceColletion.AddScoped<IProviderService, ProviderService>();
            serviceColletion.AddScoped<IUserService, UserService>();
        }
    }
}
