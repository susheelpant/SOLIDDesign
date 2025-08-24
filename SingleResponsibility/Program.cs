using Microsoft.Extensions.Configuration;
using SingleResponsibility.BadDesign;

Invoice invoice = new();
invoice.AddLine(new InvoiceRecord("Item 1", 2, 10.00m));
invoice.AddLine(new InvoiceRecord("Item 2", 1, 20.00m));
Console.WriteLine($"Total: {invoice.CalculateTotal()}");
Console.WriteLine($"CSV: {invoice.ToCsv()}");

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

// Build configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .Build();

// Updated code to handle potential null value for connection string
string? connectionString = configuration.GetConnectionString("MyDbConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'MyDbConnection' is not configured.");
}

invoice.SaveToDatabase(connectionString);
Console.WriteLine($"Saved to Database successfully. ID {invoice.Id}");

var smtpSettings = configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

// Ensure smtpSettings is not null before calling SendEmail
if (smtpSettings == null)
{
    throw new InvalidOperationException("SMTP settings are not configured.");
}

invoice.SendEmail(smtpSettings, "customeremail@inbox.com");
