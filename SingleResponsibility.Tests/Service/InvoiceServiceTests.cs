using Moq;
using SingleResponsibility.GoodDesign.Dto;
using SingleResponsibility.GoodDesign.Interfaces;
using SingleResponsibility.GoodDesign.Service;

namespace SingleResponsibility.Tests;

public class InvoiceServiceTests
{
    [Fact]
    public void ProcessAndNotify_CallsDependencies()
    {
        var repo = new Mock<IInvoiceRepository>();
        var formatter = new Mock<IInvoiceFormatter>();
        var email = new Mock<IEmailSender>();
        var invoice = new Invoice();

        formatter.Setup(f => f.Format(invoice)).Returns("csv");
        repo.Setup(r => r.Save("csv", It.IsAny<decimal>())).Returns(42);

        var service = new InvoiceService(repo.Object, formatter.Object, email.Object);

        service.ProcessAndNotify(invoice, "test@example.com");

        formatter.Verify(f => f.Format(invoice), Times.Once);
        repo.Verify(r => r.Save("csv", It.IsAny<decimal>()), Times.Once);
        email.Verify(e => e.SendEmail(It.IsAny<string>(), It.IsAny<string>(), "test@example.com"), Times.Once);
    }
}