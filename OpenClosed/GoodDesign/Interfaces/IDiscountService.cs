namespace OpenClosed.GoodDesign.Interfaces;

public interface IDiscountService
{
    decimal CalculateTotal(IOrder order);
}