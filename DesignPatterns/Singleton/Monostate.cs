namespace DesignPatterns.Singleton
{
    internal class Monostate : IPatternDemo
    {
        private class CEO
        {
            private static string name;
            private static int age;

            // Properties are not static, but can change static fields
            // In this case again all objects share same data
            public string Name{get => name; set => name = value; }
            public int Age { get => age; set => age = value; }

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
            }
        }

        public void Demo()
        {
            var ceo = new CEO();
            ceo.Name = "Adam Smith";
            ceo.Age = 55;

            var ceo2 = new CEO();
            Console.WriteLine(ceo2);
        }
    }
}