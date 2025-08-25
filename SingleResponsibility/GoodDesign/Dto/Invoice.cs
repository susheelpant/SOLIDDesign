using SingleResponsibility.Entities;

namespace SingleResponsibility.GoodDesign.Dto;
public class Invoice
{
    public int Id { get; set; }
    private List<InvoiceRecord> Lines { get; } = new();

    public decimal CalculateTotal()
        => Lines.Sum(l => l.Total());

    public void AddLine(InvoiceRecord line)
    {
        Lines.Add(line);
    }

    public IEnumerable<InvoiceRecord> GetLines()
    {
        return Lines.AsReadOnly();
    }
}


