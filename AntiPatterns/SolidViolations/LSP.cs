using System;

namespace AntiPatterns.SolidViolations
{
    // Liskov Substitution Principle Violation:
    // Square changes the expected behavior of Rectangle setters.
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public int Area() => Width * Height;
    }

    public class Square : Rectangle
    {
        // Overrides to keep sides equal — this changes the contract expected from Rectangle
        public override int Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                base.Height = value;
            }
        }

        public override int Height
        {
            get => base.Height;
            set
            {
                base.Width = value;
                base.Height = value;
            }
        }
    }

    // Example that breaks client expectations:
    public static class LspDemo
    {
        public static void ResizeAndExpectIndependentSides(Rectangle r)
        {
            r.Width = 5;
            r.Height = 10;
            // If r is a Square, Area will be 100 not 50 -> surprising behavior
            Console.WriteLine($"Area: {r.Area()}");
        }
    }
}
