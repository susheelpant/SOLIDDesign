using OpenClosed.Entities;

namespace OpenClosed.BadDesign;

public sealed class DiscountService
{
    // Adding a new discounttype => edit the enum, change Order properties and switch logic (and probably tests). That violates OCP
    public decimal CalculateTotal(Order order)
    {
        decimal discount = 0m;

        switch (order.DiscountType)
        {
            case DiscountType.Loyalty:
                if (order.CustomerLoyaltyYears >= 2)
                {
                    discount = 0.05m;
                }
                break;

            case DiscountType.BlackFriday:
                if (order.MondaySale)
                {
                    discount = 0.35m;
                }
                else
                {
                    discount = 0.20m;
                }
                break;

            case DiscountType.BulkItems:
                if (order.ItemsCount >= 10)
                {
                    discount = 0.07m;
                }
                break;

            default:
                break;
        }

        return order.Subtotal * (1 - discount);
    }
}
