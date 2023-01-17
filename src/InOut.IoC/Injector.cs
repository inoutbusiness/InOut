﻿using InOut.Domain.Queues.Publishers;
using InOut.Domain.Queues.Publishers.Interfaces;
using InOut.Infrastructure.Repositories;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Cache;
using InOut.Service.Cache.Interfaces;
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
            serviceColletion.AddScoped<IBrandRepository, BrandRepository>();
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
            serviceColletion.AddScoped<IUserAccountService, UserAccountService>();
            serviceColletion.AddScoped<IPasswordRecoveryService, PasswordRecoveryService>();
            serviceColletion.AddScoped<IBrandService, BrandService>();
        }

        private static void InjectGenerics(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<ITokenGenerator, TokenGenerator>();
            serviceColletion.AddScoped<ICacheManager, CacheManager>();
            serviceColletion.AddScoped<IEmailSenderPublisher, EmailSenderPublisher>();
            serviceColletion.AddSingleton(MappingProfile.CreateMappingProfile().CreateMapper());
        }
    }
}