using OpenClosed.Entities;

namespace OpenClosed.GoodDesign.Interfaces;

// 1) Stable abstraction / extension point
public interface IDiscountRule
{
    /// <summary>
    /// Return a fractional discount (e.g., 0.10m for 10%). Return 0 if not applicable.
    /// </summary>
    decimal CalculateDiscount(IOrder order);
}