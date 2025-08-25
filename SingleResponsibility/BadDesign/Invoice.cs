using Microsoft.Data.SqlClient;
using SingleResponsibility.Entities;
using System.Net;
using System.Net.Mail; 

namespace SingleResponsibility.BadDesign;

public class Invoice
{
    public int Id { get; private set; }
    private List<InvoiceRecord> Lines { get; } = new();

    public void ProcessAndNotify(string connectionString, SmtpSettings smtpSettings, string recipientEmail)
    {
        // Bad Design example
        this.AddLine(new InvoiceRecord("Item 1", 2, 10.00m));
        this.AddLine(new InvoiceRecord("Item 2", 1, 20.00m));
        Console.WriteLine($"Total: {this.CalculateTotal()}");
        Console.WriteLine($"CSV: {this.ToCsv()}");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'MyDbConnection' is not configured.");
        }
        this.SaveToDatabase(connectionString);
        Console.WriteLine($"Saved to Database successfully. ID {this.Id}");

        if (smtpSettings == null)
        {
            throw new InvalidOperationException("SMTP settings are not configured.");
        }
        this.SendEmail(smtpSettings, recipientEmail);
        Console.WriteLine($"Email to {recipientEmail} sent successfully.");
    }

    private void AddLine(InvoiceRecord line)
    {
        Lines.Add(line);
    }

    // Responsibility 1: Business logic
    private decimal CalculateTotal() => Lines.Sum(l => l.Total());

    // Responsibility 2: Persistence
    private void SaveToDatabase(string connectionString)
    {
        // No changes to the rest of the file are needed for this specific error.
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO solid.Invoice (Details, Total)
                    VALUES (@details, @total);
                    SELECT CAST(SCOPE_IDENTITY() AS int);"; // Add this line to retrieve the new ID
                cmd.Parameters.AddWithValue("@details", ToCsv());
                cmd.Parameters.AddWithValue("@total", CalculateTotal());

                //cmd.ExecuteNonQuery(); // ExecuteNonQuery only inserts the record without returning the ID

                // ExecuteScalar returns the Identity value for the inserted row
                Id = (int)cmd.ExecuteScalar();
            }
        }
    }

    // Responsibility 3: Formatting
    private string ToCsv() 
        => string.Join(Environment.NewLine,
            Lines.Select(l => $"{l.Description},{l.Quantity},{l.UnitPrice}"));

    // Responsibility 4: Notifications/Email
    private void SendEmail(SmtpSettings smtpSettings, string to)
    {
        // Send Email
        var mail = new MailMessage(smtpSettings.From, to)
        {
            Subject = "Your invoice",
            Body = $"Date: {DateTime.Now} \n Total: {CalculateTotal()}\n\n{ToCsv()}"
        };

        using (var client = new SmtpClient(smtpSettings.Host, smtpSettings.Port))
        {
            client.EnableSsl = smtpSettings.EnableSsl;
            client.Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password);

            try
            {
                client.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
