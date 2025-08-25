using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SingleResponsibility.GoodDesign;
using SingleResponsibility.GoodDesign.Interfaces;
using BadDesignInvoice = SingleResponsibility.BadDesign.Invoice;
using GoodDesignInvoice = SingleResponsibility.GoodDesign.Dto.Invoice;

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

// Build configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .Build();

// Updated code to handle potential null value for connection string
string connectionString = configuration.GetConnectionString("MyDbConnection");
var smtpSettings = configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
var recipientEmail = configuration["AppSettings:ToEmail"];

///  Bad Design setup
Console.WriteLine("Execution starts, under BAD Design");
BadDesignMain(connectionString, smtpSettings, recipientEmail);

static void BadDesignMain(string connectionString, SmtpSettings smtpSettings, string recipientEmail)
{
    // Bad Design example

    BadDesignInvoice invoice = new();
    invoice.ProcessAndNotify(connectionString, smtpSettings, recipientEmail);
}

/// Good Design setup
Console.WriteLine("Execution starts, under GOOD Design");
var services = new ServiceCollection().AddInvoiceServices(connectionString, smtpSettings);

GoodDesignMain(services.BuildServiceProvider(), recipientEmail);

static void GoodDesignMain(IServiceProvider serviceProvider, string recipientEmail)
{
    // Good Design example
    using var scope = serviceProvider.CreateScope();
    var scopedServices = scope.ServiceProvider;
    var invoiceService = scopedServices.GetRequiredService<IInvoiceService>();
    invoiceService.ProcessAndNotify(new GoodDesignInvoice(), recipientEmail);
}


