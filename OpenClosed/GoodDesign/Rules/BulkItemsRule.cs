using OpenClosed.GoodDesign.Entities;
using OpenClosed.GoodDesign.Interfaces;

namespace OpenClosed.GoodDesign.Rules
{
    // 2) Concrete policies (each lives in its own closed class)
    public sealed class BulkItemsRule : IDiscountRule
    {
        public decimal CalculateDiscount(IOrder order)
        {
            return ((Order)order).ItemsCount >= 10 ? 0.07m : 0m;
        }
    }
}
