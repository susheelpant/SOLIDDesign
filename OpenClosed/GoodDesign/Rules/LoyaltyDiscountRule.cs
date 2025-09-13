using OpenClosed.GoodDesign.Entities;
using OpenClosed.GoodDesign.Interfaces;

namespace OpenClosed.GoodDesign.Rules
{
    // 2) Concrete policies (each lives in its own closed class)
    public sealed class LoyaltyDiscountRule : IDiscountRule
    {
        public decimal CalculateDiscount(IOrder order)
        {
            if(order is not LoyaltyOrder)
            {
                return 0m;
            }

            return ((LoyaltyOrder)order).CustomerLoyaltyYears >= 2 ? 0.05m : 0m;
        }
    }
}
