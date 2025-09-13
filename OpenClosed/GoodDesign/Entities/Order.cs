using OpenClosed.Entities;
using OpenClosed.GoodDesign.Interfaces;

namespace OpenClosed.GoodDesign.Entities;

public class Order : IOrder
{
    public decimal Subtotal { get; init; }
    public int ItemsCount { get; init; }
    public DiscountType DiscountType { get; init; }
}
