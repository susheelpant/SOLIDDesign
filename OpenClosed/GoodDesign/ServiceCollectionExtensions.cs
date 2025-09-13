using Microsoft.Extensions.DependencyInjection;
using OpenClosed.GoodDesign.Entities;
using OpenClosed.GoodDesign.Factory;
using OpenClosed.GoodDesign.Interfaces;
using OpenClosed.GoodDesign.Rules;
using OpenClosed.GoodDesign.Service;


namespace OpenClosed.GoodDesign;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscountServices(
        this IServiceCollection services)
    {
        services.AddSingleton<IDiscountRuleFactory, DiscountRuleFactory>();
        services.AddSingleton<IDiscountService, DiscountService>();

        return services;
    }
}