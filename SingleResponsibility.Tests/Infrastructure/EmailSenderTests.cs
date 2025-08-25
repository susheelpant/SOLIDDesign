using SingleResponsibility.GoodDesign.Infrastructure;

namespace SingleResponsibility.Tests;

public class EmailSenderTests
{
    [Fact]
    public void SendEmail_ThrowsOnInvalidHost()
    {
        var settings = new SmtpSettings
        {
            Host = "",
            Port = 25,
            EnableSsl = false,
            Username = "user",
            Password = "pass",
            From = "Sender"
        };
        var sender = new EmailSender(settings);

        Assert.ThrowsAny<Exception>(() =>
            sender.SendEmail("subject", "body", "to@example.com"));
    }
}