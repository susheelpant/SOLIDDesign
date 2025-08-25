using SingleResponsibility.GoodDesign.Dto;

namespace SingleResponsibility.GoodDesign.Interfaces;

public interface IInvoiceFormatter
{
    string Format(Invoice invoice);
}
