using OpenClosed.Entities;

namespace OpenClosed.BadDesign;

public sealed class Order
{
    public decimal Subtotal { get; init; }
    public int CustomerLoyaltyYears { get; init; }
    public int ItemsCount { get; init; }
    public bool MondaySale { get; init; }
    public DiscountType DiscountType { get; init; }
}
