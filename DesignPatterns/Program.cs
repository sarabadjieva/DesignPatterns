using DesignPatterns;
using DesignPatterns.Builder;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();

        var patternDemos = types.Where(t => typeof(IPatternDemo).IsAssignableFrom(t) && !t.IsInterface);

        foreach (var type in patternDemos)
        {
            Console.WriteLine($"-----{type.Name}-----");
            var instance = Activator.CreateInstance(type) as IPatternDemo;
            instance?.Demo();
            Console.WriteLine(Environment.NewLine);
        }

        //Without demos: InterfaceSegregation
    }
}