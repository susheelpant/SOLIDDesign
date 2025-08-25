using Microsoft.Extensions.DependencyInjection;
using SingleResponsibility.GoodDesign.Service;
using SingleResponsibility.GoodDesign.Interfaces;
using SingleResponsibility.GoodDesign.Persistence;
using SingleResponsibility.GoodDesign.Infrastructure;

namespace SingleResponsibility.GoodDesign;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInvoiceServices(
        this IServiceCollection services,
        string connectionString,
        SmtpSettings smtpSettings)
    {
        services.AddSingleton<IInvoiceRepository>(provider =>
            new SqlInvoiceRepository(connectionString));
        services.AddSingleton<IInvoiceFormatter, CsvInvoiceFormatter>();
        services.AddSingleton<IEmailSender>(provider =>
            new SmtpEmailSender(smtpSettings));
        services.AddSingleton<IInvoiceService, InvoiceService>();

        return services;
    }
}