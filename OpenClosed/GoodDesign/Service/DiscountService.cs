using OpenClosed.GoodDesign.Interfaces;
using System.Data;

namespace OpenClosed.GoodDesign.Service
{
    // 3) The calculator is "closed": it doesn't change when we add policies
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRuleFactory _ruleFactory;

        public DiscountService(IDiscountRuleFactory ruleFactory)
        {
            _ruleFactory = ruleFactory;
        }

        public decimal CalculateTotal(IOrder order)
        {
            if(order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if(order.Subtotal < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(order.Subtotal), "Subtotal cannot be negative.");
            }

            var rule = _ruleFactory.GetRule(order.DiscountType);
            var discount = rule is null ? 0m : rule.CalculateDiscount(order);
            return order.Subtotal * (1 - discount);
        }
    }
}