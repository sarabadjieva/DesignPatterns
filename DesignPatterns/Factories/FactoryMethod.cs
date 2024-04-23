namespace DesignPatterns.Factories
{
    internal class FactoryMethod : IPatternDemo
    {
        private enum CoordinateSystem
        {
            Cartesian,
            Polar
        }

        private class Point
        {
            private double x, y;

            //NB private constructor -> mandatory to use factory method
            private Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            //factory method
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            //factory method
            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho*Math.Cos(theta), rho*Math.Sin(theta));
            }

            //This is the example of what NOT to do
            //The parameters are confusing, optional parameters are not preferred
            ///// <summary>
            ///// Initializes a point from EITHER cartesian or polar
            ///// </summary>
            ///// <param name="a">x if cartesian, rho if polar</param>
            ///// <param name="b"></param>
            ///// <param name="system"></param>
            //public Point(double a, double b,
            //    CoordinateSystem system = CoordinateSystem.Cartesian)
            //{
            //    switch (system)
            //    {
            //        case CoordinateSystem.Cartesian:
            //            this.x = a; //confusion because of parameters' name
            //            this.y = b;
            //            break;
            //        case CoordinateSystem.Polar:
            //            this.x = a * Math.Cos(b);
            //            this.y = a * Math.Sin(b);
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //cannot overload for ex. public Point(double rho, double theta)

            public override string ToString()
            {
                return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
            }
        }

        public void Demo()
        {
            Console.WriteLine(Point.NewPolarPoint(1.0, Math.PI / 2));
        }
    }
}
