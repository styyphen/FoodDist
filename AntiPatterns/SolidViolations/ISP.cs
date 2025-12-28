using System;

namespace AntiPatterns.SolidViolations
{
    // Interface Segregation Principle Violation:
    // Fat interface forces classes to implement methods they don't need.
    public interface IKitchenDevice
    {
        void Fry();
        void Bake();
        void Steam();
        void Blend();
    }

    public class Toaster : IKitchenDevice
    {
        public void Fry() { /* noop - toaster can't fry */ }
        public void Bake() { Console.WriteLine("Toasting..."); }
        public void Steam() { /* noop */ }
        public void Blend() { /* noop */ }
    }
}
