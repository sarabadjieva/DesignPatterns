namespace DesignPatterns.Builder
{
    /// <summary>
    /// Chain, which is enforced through a set of interfaces
    /// Acts as a kind of wizard
    /// In a specific order
    /// </summary>
    internal class StepwiseBuilder : IPatternDemo
    {
        enum CarType { Sedan, Crossover }

        class Car
        {
            public CarType Type { get; set; }
            public int WheelSize { get; set; }

            public override string ToString()
            {
                return $"Car {Type} with wheel size {WheelSize}";
            }
        }

        interface ISpecifyCarType
        {
            public ISpecifyWheelSize OfType(CarType type);
        }

        interface ISpecifyWheelSize
        {
            public IBuildCar WithWheels(int size);
        }

        interface IBuildCar
        {
            public Car Build();
        }

        class CarBuilder
        {
            private class Impl :
                ISpecifyCarType,
                ISpecifyWheelSize,
                IBuildCar
            {
                private Car car = new();

                public ISpecifyWheelSize OfType(CarType type)
                {
                    car.Type = type;
                    return this;
                }

                public IBuildCar WithWheels(int size)
                {
                    switch (car.Type)
                    {
                        case CarType.Crossover when size < 17 || size > 20:
                        case CarType.Sedan when size < 15 || size > 17:
                            throw new ArgumentException($"Wrong size of wheel for {car.Type}");
                    }

                    car.WheelSize = size;
                    return this;
                }

                public Car Build() => car;
            }

            public static ISpecifyCarType Create()
            {
                return new Impl();
            }
        }

        public void Demo()
        {
            Car car = CarBuilder.Create()   //ISpecifyCarType
                .OfType(CarType.Crossover)  //ISpecifyWheelSize
                .WithWheels(18)             //IBuildCar
                .Build();                   //Car

            Console.WriteLine(car);
        }
    }
}
