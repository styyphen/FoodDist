using System;

namespace AntiPatterns.SolidViolations
{
    // Dependency Inversion Principle Violation:
    // High-level module depends on a concrete low-level implementation.
    public class MySqlOrderRepository
    {
        public void Save(Order order)
        {
            // MySQL-specific save logic mixed into domain usage
            Console.WriteLine("Saving order to MySQL");
        }
    }

    public class OrderService
    {
        // Direct dependency on concrete repo (no abstraction)
        private readonly MySqlOrderRepository _repo = new MySqlOrderRepository();

        public void PlaceOrder(Order order)
        {
            // business logic
            _repo.Save(order);
        }
    }

    public class Order { public int Id; }
}
