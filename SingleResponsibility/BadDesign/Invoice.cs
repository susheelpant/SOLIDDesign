using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Mail; 

namespace SingleResponsibility.BadDesign;

public class Invoice
{
    public int Id { get; private set; }
    private List<InvoiceRecord> Lines { get; } = new();

    public void AddLine(InvoiceRecord line)
    {
        Lines.Add(line);
    }

    // Responsibility 1: Business logic
    public decimal CalculateTotal() => Lines.Sum(l => l.Total());

    // Responsibility 2: Persistence
    public void SaveToDatabase(string connectionString)
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
    public string ToCsv() 
        => string.Join(Environment.NewLine,
            Lines.Select(l => $"{l.Description},{l.Quantity},{l.UnitPrice}"));

    // Responsibility 4: Notifications/Email
    public void SendEmail(SmtpSettings smtpSettings, string to)
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
