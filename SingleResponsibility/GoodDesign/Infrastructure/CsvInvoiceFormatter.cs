using SingleResponsibility.GoodDesign.Dto;
using SingleResponsibility.GoodDesign.Interfaces;

namespace SingleResponsibility.GoodDesign.Infrastructure
{
    public class CsvInvoiceFormatter : IInvoiceFormatter
    {
        // Responsibility 3: Formatting
        public string Format(Invoice invoice)
            => string.Join(Environment.NewLine,
                invoice.GetLines().Select(l => $"{l.Description},{l.Quantity},{l.UnitPrice}"));
    }
}
