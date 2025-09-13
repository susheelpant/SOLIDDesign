using OpenClosed.Entities;
using OpenClosed.GoodDesign.Interfaces;

public interface IDiscountRuleFactory
{
    IDiscountRule GetRule(DiscountType discountType);
}
