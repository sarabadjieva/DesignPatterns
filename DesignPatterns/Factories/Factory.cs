namespace DesignPatterns.Factories
{
    internal class Factory : IPatternDemo
    {
        //Instead of having factory methods for Point from FactoryMethod.cs
        private static class PointFactory
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

        private enum CoordinateSystem
        {
            Cartesian,
            Polar
        }

        private class Point
        {
            private double x, y;

            //had to make it public; change later in course
            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
            }
        }

        public void Demo()
        {
            Console.WriteLine(PointFactory.NewPolarPoint(1.0, Math.PI / 2));
        }
    }
}
