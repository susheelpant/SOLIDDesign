using SingleResponsibility.GoodDesign.Infrastructure;
using SingleResponsibility.GoodDesign.Dto;

namespace SingleResponsibility.Tests;

public class CsvInvoiceFormatterTests
{
    [Fact]
    public void Format_ReturnsCsvString()
    {
        var invoice = new Invoice();
        invoice.AddLine(new SingleResponsibility.Entities.InvoiceRecord("Item1", 2, 10.5m));
        invoice.AddLine(new SingleResponsibility.Entities.InvoiceRecord("Item2", 1, 5m));

        var formatter = new CsvInvoiceFormatter();
        var result = formatter.Format(invoice);

        Assert.Contains("Item1,2,10.5", result);
        Assert.Contains("Item2,1,5", result);
    }
}