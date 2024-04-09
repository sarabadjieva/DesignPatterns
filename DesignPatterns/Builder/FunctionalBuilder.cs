namespace DesignPatterns.Builder
{
    /// <summary>
    /// Attempting to do in a function way and keeing the Open-Closed principle
    /// The classes here are not private as we are using extensions
    /// Extensions cannot be nested
    /// </summary>
    internal class FunctionalBuilder : IPatternDemo
    {
        internal class Person
        {
            public string Name { get; set; }
            public string Position { get; set; }

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

        internal abstract class GenericFunctionalBuilder<TSubject, TSelf>
            where TSubject : new()
            where TSelf : GenericFunctionalBuilder<TSubject, TSelf>
        {
            private readonly List<Func<Person, Person>> actions = [];

            public TSelf? Do(Action<Person> action) => AddAction(action);

            public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));

            private TSelf? AddAction(Action<Person> action)
            {
                actions.Add(p => { action(p); return p; });
                return this as TSelf;
            }
        }

        internal sealed class PersonBuilder
            : GenericFunctionalBuilder<Person, PersonBuilder>
        {
            public PersonBuilder? Called(string name) => Do(p => p.Name = name);
        }

        //The class should not be private
        //internal sealed class PersonBuilder
        //{
        //    private readonly List<Func<Person, Person>> actions = [];

        //    public PersonBuilder Called(string name) => Do(p => p.Name = name);

        //    public PersonBuilder Do(Action<Person> action) => AddAction(action);

        //    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));

        //    private PersonBuilder AddAction(Action<Person> action)
        //    {
        //        actions.Add(p => { action(p); return p; });
        //        return this;
        //    }

        public void Demo()
        {
            var person = new PersonBuilder()
                .Called("Sarah")?
                .WorksAs("Developer")?
                .Build();

            Console.WriteLine(person);
        }
    }

    static class PersonBuilderExtensions
    {
        public static FunctionalBuilder.PersonBuilder? WorksAs
            (this FunctionalBuilder.PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);
    }
}
