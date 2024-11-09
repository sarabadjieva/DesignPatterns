using DesignPatterns;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();

        var patternDemos = types.Where(t => typeof(IPatternDemo).IsAssignableFrom(t) && !t.IsInterface);

        foreach (var group in patternDemos.GroupBy(t => t.Namespace))
        {
            Console.WriteLine($"-----NAMESPACE {group.Key}-----");

            foreach (var type in group)
            {
                Console.WriteLine($"-----{type.Name}-----");
                var instance = Activator.CreateInstance(type) as IPatternDemo;
                instance?.Demo();
                Console.Write(Environment.NewLine);
            }

            Console.Write(Environment.NewLine + Environment.NewLine);
        }

        //Without demos: InterfaceSegregation, SingletonImplementation + DI (most is in tests)
        //AsyncFactoryMethod (have to make some adjustments to reflection/interface)
    }
}