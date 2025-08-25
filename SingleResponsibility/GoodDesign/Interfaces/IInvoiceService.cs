using SingleResponsibility.GoodDesign.Dto;

namespace SingleResponsibility.GoodDesign.Interfaces
{
    // Orchestration/application service (coordinates responsibilities)
    public interface IInvoiceService
    {
        void ProcessAndNotify(Invoice invoice, string recipientEmail);
    }
}