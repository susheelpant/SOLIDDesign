using OpenClosed.GoodDesign.Entities;
using OpenClosed.GoodDesign.Interfaces;

namespace OpenClosed.GoodDesign.Rules
{
    // 2) Concrete policies (each lives in its own closed class)
    public sealed class BlackFridayRule : IDiscountRule
    {
        public decimal CalculateDiscount(IOrder order)
        {
            if (order is not BlackFridayOrder)
            {
                return 0m;
            }

            return ((BlackFridayOrder)order).MondaySale ? 0.35m : 0.20m;
        }
    }
}
