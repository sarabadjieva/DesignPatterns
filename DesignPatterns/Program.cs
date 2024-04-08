using DesignPatterns.Builder;
using DesignPatterns.SOLID;

internal class Program
{
    private static void Main(string[] args)
    {
        //SOLID
        SingleResponsibility.Demo();
        OpenClosed.Demo();
        Liskov.Demo();
        //InterfaceSegregation.Demo();
        DependencyInversion.Demo();

        //Builder
        Builder.Demo();
    }
}