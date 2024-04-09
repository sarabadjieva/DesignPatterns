namespace DesignPatterns.SOLID
{
    /// <summary>
    /// Derived classes must be substitutable for their base classes
    /// Ex. Using virtual variables
    /// </summary>
    internal class Liskov : IPatternDemo
    {
        private class Rectangle
        {
            //public int Width { get; set; }
            //public int Height { get; set; }
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            public Rectangle() {}

            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
        }

        private class Square : Rectangle
        {
            //public new int Width
            public override int Width
            {
                set { base.Width = value; base.Height = value; }
            }

            //public new int Height
            public override int Height
            {
                set { base.Width = value; base.Height = value; }
            }
        }

        private static int Area(Rectangle r) => r.Width * r.Height;

        public void Demo()
        {
            Rectangle r = new(2, 3);
            Console.WriteLine($"{r} has Area {Area(r)}");

            //If we have left the rectangle's properties non-virtual
            //And declare the square's as new
            //this would have changed only the width
            Rectangle s = new() { Width = 4 };
            Console.WriteLine($"{s} has Area {Area(s)}");
        }
    }
}
