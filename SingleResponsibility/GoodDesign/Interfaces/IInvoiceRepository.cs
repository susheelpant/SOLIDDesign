using SingleResponsibility.GoodDesign.Dto;

namespace SingleResponsibility.GoodDesign.Interfaces;

// Persistence responsibility
public interface IInvoiceRepository
{
    int Save(string details, decimal total);
}