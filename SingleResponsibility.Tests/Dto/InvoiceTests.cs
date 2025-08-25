using SingleResponsibility.GoodDesign.Dto;

namespace SingleResponsibility.Tests;

public class InvoiceTests
{
    [Fact]
    public void CalculateTotal_ReturnsSum()
    {
        var invoice = new Invoice();
        invoice.AddLine(new SingleResponsibility.Entities.InvoiceRecord("A", 2, 5m));
        invoice.AddLine(new SingleResponsibility.Entities.InvoiceRecord("B", 1, 10m));

        var total = invoice.CalculateTotal();

        Assert.Equal(20m, total);
    }
}