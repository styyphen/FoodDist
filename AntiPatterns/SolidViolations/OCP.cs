using System;

namespace AntiPatterns.SolidViolations
{
    // Open/Closed Principle Violation:
    // The method must be modified whenever a new shipping type is added.
    public static class ShippingCalculator
    {
        public static decimal CalculateShipping(OrderDto order)
        {
            switch (order.ShippingType)
            {
                case "standard": return 5m;
                case "express": return 15m;
                case "drone": return 2m;
                // Adding new types requires editing this switch -> violates OCP
                default: return 10m;
            }
        }
    }

    public class OrderDto { public string ShippingType; }
}
