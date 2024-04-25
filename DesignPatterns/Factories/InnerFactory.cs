namespace DesignPatterns.Factories
{
    internal class InnerFactory : IPatternDemo
    {
        private enum CoordinateSystem
        {
            Cartesian,
            Polar
        }

        private class Point
        {
            private readonly double x;
            private readonly double y;

            //can be "internal"
            private Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
            }

            public static Point Origin => new(0, 0); //initialized every time
            public static Point Origin2 = new(0, 0); //initialized once

            //if we dont want a static class for some reason
            //public static Factory Factory = new Factory();

            public static class Factory
            {
                public static Point NewCartesianPoint(double x, double y)
                {
                    return new Point(x, y);
                }

                public static Point NewPolarPoint(double rho, double theta)
                {
                    return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
                }
            }
        }
        
        public void Demo()
        {
            Console.WriteLine(Point.Factory.NewPolarPoint(1.0, Math.PI / 2));
        }
    }
}
