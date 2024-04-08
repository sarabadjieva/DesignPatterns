namespace DesignPatterns.Builder
{
    /// <summary>
    /// Problem with inheritance of fluent builders
    /// Avoid them with interface and generics
    /// </summary>
    internal class FluentBuilderInheritanceWithRecursiveGenerics : IPatternDemo
    {
        class Person
        {
            public string Name { get; set; }
            public string Position { get; set; }

            public class Builder : PersonJobBuilder<Builder> { }

            public static Builder New => new Builder();

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

        abstract class PersonBuilder
        {
            protected Person person = new();

            public Person Build() => person;
        }

        class PersonInfoBuilder<T>
            : PersonBuilder
            where T : PersonInfoBuilder<T>
        {
            //protected Person person = new();

            //public PersonInfoBuilder Called(string name)
            public T? Called(string name)
            {
                person.Name = name;
                return this as T;
            }
        }

        //Because of Open-Closed principle, we decide to create a new class and expand it
        class PersonJobBuilder<T>
            : PersonInfoBuilder<PersonJobBuilder<T>>
            where T : PersonJobBuilder<T>
        {
            //public PersonJobBuilder WorkAsA(string position)
            public T? WorkAsA(string position)
            {
                person.Position = position;
                return this as T;
            }
        }

        public void Demo()
        {
            //PersonJobBuilder builder = new();
            //builder.Called("Dimitar"); //cannot call WorkAsA

            var person = Person.New.Called("Dimitar")?.WorkAsA("Programmer")?.Build();
            Console.WriteLine(person);

            PersonJobBuilder<Person.Builder>? builder = Person.New.Called("Ivan");
        }
    }
}
