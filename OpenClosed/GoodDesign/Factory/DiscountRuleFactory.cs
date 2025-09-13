using OpenClosed.Entities;
using OpenClosed.GoodDesign.Entities;
using OpenClosed.GoodDesign.Interfaces;
using OpenClosed.GoodDesign.Rules;

namespace OpenClosed.GoodDesign.Factory
{
    public class DiscountRuleFactory : IDiscountRuleFactory
    {
        public IDiscountRule GetRule(DiscountType discountType) // Mark return type as nullable
        {
            return discountType switch
            {
                DiscountType.None => new NilDiscountRule(),
                DiscountType.Loyalty => new LoyaltyDiscountRule(),
                DiscountType.BlackFriday => new BlackFridayRule(),
                DiscountType.BulkItems => new BulkItemsRule(),

                // New rule added later
                DiscountType.Coupons => new CouponDiscountRule(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
