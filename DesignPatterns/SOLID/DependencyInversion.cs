namespace DesignPatterns.SOLID
{
    /// <summary>
    /// High-level modules should not depend on low-level modules
    /// Use abstractions where possible
    /// </summary>
    internal class DependencyInversion : IPatternDemo
    {
        enum Relationship { Parent, Child, Sibling }

        class Person(string name)
        {
            public string Name { get; set; } = name;
        }

        interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        //low-level API
        class Relationships : IRelationshipBrowser
        {
            //As it is bad idea to change the type of Relations if it is exposed
            //we make it private and create the browser here
            //public List<(Person, Relationship, Person)> Relations => relations;
            private readonly List<(Person, Relationship, Person)> relations = [];

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }

            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                return relations
                    .Where(x => x.Item1.Name == name
                    && x.Item2 == Relationship.Parent)
                    .Select(x => x.Item3);
            }
        }

        //high-level module
        class Research
        {
            //public Research(Relationships relationships)
            //{
            //    var relations = relationships.Relations;

            //    foreach (var relation in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
            //    {
            //        Console.WriteLine($"John has a child called {relation.Item3.Name}");
            //    };
            //}

            public Research(IRelationshipBrowser browser)
            {
                foreach (var child in browser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine($"John has a child called {child.Name}");
                }
            }
        }

        public void Demo()
        {
            Person parent = new("John");
            Person child1 = new("Chris");
            Person child2 = new("Mary");

            Relationships relationships = new();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);
            _ = new Research(relationships);
        }
    }
}
