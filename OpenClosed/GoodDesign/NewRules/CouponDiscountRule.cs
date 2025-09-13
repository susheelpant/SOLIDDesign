using OpenClosed.GoodDesign.Interfaces;
using OpenClosed.GoodDesign.NewEntities;

namespace OpenClosed.GoodDesign.Rules
{
    // 2) Concrete policies (each lives in its own closed class)
    // Add Later
    public sealed class CouponDiscountRule : IDiscountRule
    {
        public decimal CalculateDiscount(IOrder order)
        {
            CouponOrder couponOrder = (CouponOrder)order;

            if (string.IsNullOrWhiteSpace(couponOrder.CouponCode))
            {
                return 0m;
            }
            else if (couponOrder.CouponCode.Equals("SAVE20", StringComparison.OrdinalIgnoreCase))
            {
                return 0.20m;
            }
            else if (couponOrder.CouponCode.Equals("SAVE10", StringComparison.OrdinalIgnoreCase))
            {
                return 0.10m;
            }
            else
            {
                return 0m;
            }
        }
    }
}
