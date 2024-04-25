using System.Reflection;

namespace DesignPatterns.Factories
{
    internal class AbstractFactory : IPatternDemo
    {
        //Make it more interactive because of Open-Closed Principle
        //private enum AvailableDrink { Coffee, Tea }

        private interface IHotDrink
        {
            public void Consume();
        }

        private class Tea : IHotDrink
        {
            public void Consume()
            {
                Console.WriteLine("This tea is nice.");
            }
        }

        private class Coffee : IHotDrink
        {
            public void Consume()
            {
                Console.WriteLine("This coffee is awesome.");
            }
        }

        private interface IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount);
        }

        private class TeaFactory : IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount)
            {
                Console.WriteLine($"Put in a tea bag, boil water, pour {amount} ml");
                return new Tea();
            }
        }

        private class CoffeeFactory : IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount)
            {
                Console.WriteLine($"Grind some beans, boil water, pour {amount} ml");
                return new Coffee();
            }
        }

        private class HotDrinkMachine
        {
            //Make it more interactive because of Open-Closed Principle
            //private readonly Dictionary<AvailableDrink, IHotDrinkFactory> factories = [];

            //public HotDrinkMachine()
            //{
            //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            //    {
            //        //nested class so we need a '+'
            //        string factoryName = "DesignPatterns.Factories.AbstractFactory+"
            //                + Enum.GetName(typeof(AvailableDrink), drink)
            //                + "Factory";

            //        var factory = (IHotDrinkFactory)Activator
            //            .CreateInstance(Type.GetType(factoryName));

            //        factories.Add(drink, factory);
            //    }
            //}

            //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
            //{
            //    return factories[drink].Prepare(amount);
            //}

            private List<Tuple<string, IHotDrinkFactory>> factories = [];

            public HotDrinkMachine()
            {
                foreach (var item in typeof(HotDrinkMachine).DeclaringType.GetNestedTypes(BindingFlags.NonPublic))
                {
                    if (typeof(IHotDrinkFactory).IsAssignableFrom(item)
                        && !item.IsInterface)
                    {
                        factories.Add(Tuple.Create(
                            item.Name.Replace("Factory", string.Empty),
                            (IHotDrinkFactory)Activator.CreateInstance(item)));
                    }
                }
            }

            public IHotDrink MakeDrink()
            {
                Console.WriteLine("Available drinks:");
                for (int i = 0; i < factories.Count; i++)
                {
                    var tuple = factories[i];
                    Console.WriteLine($"{i}: {tuple.Item1}");
                }

                while (true)
                {
                    string s;
                    if ((s = Console.ReadLine()) != null
                        && int.TryParse(s, out int i)
                        && i>= 0
                        && i< factories.Count)
                    {
                        Console.WriteLine("Specify amount: ");
                        ;
                        if ((s = Console.ReadLine()) != null
                            && int.TryParse(s, out int amount)
                            && amount > 0)
                        {
                            return factories[i].Item2.Prepare(amount);
                        }
                    }

                    Console.WriteLine("Incorect input. Try again!");
                }
            }
        }

        public void Demo()
        {
            HotDrinkMachine machine = new();
            machine.MakeDrink();
            //machine.MakeDrink(AvailableDrink.Tea, 100);
            //machine.MakeDrink(AvailableDrink.Coffee, 50);
        }
    }
}
