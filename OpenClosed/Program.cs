using Microsoft.Extensions.DependencyInjection;
using OpenClosed.BadDesign;
using OpenClosed.Entities;
using OpenClosed.GoodDesign;
using OpenClosed.GoodDesign.Entities;
using OpenClosed.GoodDesign.Interfaces;
using OpenClosed.GoodDesign.NewEntities;
using System;


///  Bad Design setup
Console.WriteLine("Execution starts, under BAD Design");
BadDesignMain();

static void BadDesignMain()
{
    // Bad Design example
    var orders = new List<OpenClosed.BadDesign.Order>
    {
        new() { Subtotal = 100m, CustomerLoyaltyYears = 3, ItemsCount = 5, MondaySale = false, DiscountType = DiscountType.Loyalty },
        new() { Subtotal = 200m, CustomerLoyaltyYears = 1, ItemsCount = 3, MondaySale = true, DiscountType = DiscountType.BlackFriday },
        new() { Subtotal = 150m, CustomerLoyaltyYears = 0, ItemsCount = 11, MondaySale = false, DiscountType = DiscountType.BulkItems },
        new() { Subtotal = 50m, CustomerLoyaltyYears = 5, ItemsCount = 2, MondaySale = true, DiscountType = DiscountType.None }
    };

    var discountService = new OpenClosed.BadDesign.DiscountService();
    foreach (var order in orders)
    {
        var total = discountService.CalculateTotal(order);
        Console.WriteLine($"Order Subtotal: {order.Subtotal:C}, Discount Type: {order.DiscountType}, Total after Discount: {total:C}");
    }
}


///  Good Design setup
Console.WriteLine("Execution starts, under GOOD Design");
var services = new ServiceCollection().AddDiscountServices();
GoodDesignMain(services.BuildServiceProvider());


static void GoodDesignMain(IServiceProvider serviceProvider)
{
    // Good Design example
    var orders = new List<IOrder>
    {
        new LoyaltyOrder() { Subtotal = 100m, CustomerLoyaltyYears = 3, ItemsCount = 5, DiscountType = DiscountType.Loyalty },
        new BlackFridayOrder() { Subtotal = 200m, ItemsCount = 3, MondaySale = true, DiscountType = DiscountType.BlackFriday },
        new OpenClosed.GoodDesign.Entities.Order() { Subtotal = 150m, ItemsCount = 11, DiscountType = DiscountType.BulkItems },
        new OpenClosed.GoodDesign.Entities.Order() { Subtotal = 50m, ItemsCount = 2, DiscountType = DiscountType.None },

        // new rule added later without any change to existing code
        new CouponOrder() { Subtotal = 50m, ItemsCount = 5, CouponCode = "SAVE20", DiscountType = DiscountType.Coupons },
        new CouponOrder() { Subtotal = 50m, ItemsCount = 5, CouponCode = "SAVE10", DiscountType = DiscountType.Coupons },
        new CouponOrder() { Subtotal = 50m, ItemsCount = 5, CouponCode = "SAVE50", DiscountType = DiscountType.Coupons } // SAVE50 is not defined, should apply no discount
    };

    using var scope = serviceProvider.CreateScope();
    var scopedServices = scope.ServiceProvider;
    var discountService = scopedServices.GetRequiredService<IDiscountService>();
    foreach (var order in orders)
    {
        var total = discountService.CalculateTotal(order);
        Console.WriteLine($"Order Subtotal: {order.Subtotal:C}, Discount Type: {order.DiscountType}, Total after Discount: {total:C}");
    }
}