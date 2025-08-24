namespace SingleResponsibility.BadDesign;

public class InvoiceRecord
{
    public string Description { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }

    public InvoiceRecord(string description, int quantity, decimal unitPrice)
    {
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public decimal Total() => Quantity * UnitPrice;
}
