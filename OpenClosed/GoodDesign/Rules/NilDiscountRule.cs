using OpenClosed.GoodDesign.Entities;
using OpenClosed.GoodDesign.Interfaces;

namespace OpenClosed.GoodDesign.Rules
{
    // 2) Concrete policies (each lives in its own closed class)
    public sealed class NilDiscountRule : IDiscountRule
    {
        public decimal CalculateDiscount(IOrder order)
        {
            return 0m;
        }
    }
}
