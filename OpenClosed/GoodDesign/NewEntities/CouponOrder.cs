using OpenClosed.GoodDesign.Entities;

namespace OpenClosed.GoodDesign.NewEntities;

public class CouponOrder :Order
{
    public string CouponCode { get; init; }
}
