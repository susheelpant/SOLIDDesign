using SingleResponsibility.Entities;
using SingleResponsibility.GoodDesign.Dto;
using SingleResponsibility.GoodDesign.Interfaces;

namespace SingleResponsibility.GoodDesign.Service;

public class InvoiceService: IInvoiceService
{
    private readonly IInvoiceRepository _repo;
    private readonly IInvoiceFormatter _formatter;
    private readonly IEmailSender _client;

    public InvoiceService(IInvoiceRepository repo, IInvoiceFormatter formatter, IEmailSender client)
    {
        _repo = repo;
        _formatter = formatter;
        _client = client;
    }

    // Orchestration/application service (coordinates responsibilities)
    public void ProcessAndNotify(Invoice invoice, string recipientEmail)
    {
        GenerateInvoice(invoice); // business logic responsibility

        var csv = _formatter.Format(invoice); // formatting responsibility
        var total = invoice.CalculateTotal(); // domain responsibility (kept in domain)
        invoice.Id = _repo.Save(csv, total); // persistence responsibility

        var body = $"Date: {DateTime.Now} \n Total: {total}\n\n{csv}";
        _client.SendEmail("Your invoice", body, recipientEmail); // notification responsibility
    }

    private void GenerateInvoice(Invoice invoice)
    {
        // Business logic responsibility
        invoice.AddLine(new InvoiceRecord("Item 1", 2, 10.00m));// domain responsibility (kept in domain)
        invoice.AddLine(new InvoiceRecord("Item 2", 1, 20.00m));// domain responsibility (kept in domain)
    }
}