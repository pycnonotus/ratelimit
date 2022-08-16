using Application.PrimeNumbers;
using FakeCompany.RateLimit.Factories;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class RegisterApplicationExtensions
{
    public static IServiceCollection AddIsPrimeService(this IServiceCollection serviceCollection)
    {

        serviceCollection.AddSingleton<IErrorMessageFactory, ErrorMessageFactory>();
        
        return serviceCollection
            .AddSingleton<IPrimeNumberCalculator, SimplePrimesCalculator>()
            .AddSingleton< IPrimeNumberService,PrimeNumberService>();
    }
}
