using OpenClosed.Entities;

namespace OpenClosed.GoodDesign.Interfaces;

// 1) Stable abstraction / extension point
public interface IOrder
{
    decimal Subtotal { get; init; }
    DiscountType DiscountType { get; init; }
}