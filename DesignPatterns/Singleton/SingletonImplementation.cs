using Autofac;
using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Singleton
{
    internal class SingletonImplementation : IPatternDemo
    {
        private interface IDatabase
        {
            int GetPopulation(string name);
        }


        private class SingletonDatabase : IDatabase
        {
            private static int instanceCount;
            public static int Count => instanceCount;

            private Dictionary<string, int> capitals = [];

            private static Lazy<SingletonDatabase> instance =
                new(()=> new SingletonDatabase());
            public static SingletonDatabase Instance => instance.Value;

            //private constructor
            private SingletonDatabase()
            {
                Console.WriteLine("Initializing database");

                capitals = File.ReadAllLines(Path.Combine("Singleton","Capitals.txt"))
                    .Batch(2)
                    .ToDictionary(
                        list => list.ElementAt(0).Trim(),
                        list => int.Parse(list.ElementAt(1)));

                instanceCount++;
            }

            public int GetPopulation(string name) => capitals[name];
        }

        private class OrdinaryDatabase : IDatabase
        {
            private Dictionary<string, int> capitals = [];

            //private constructor
            private OrdinaryDatabase()
            {
                Console.WriteLine("Initializing database");

                capitals = File.ReadAllLines(Path.Combine("Singleton", "Capitals.txt"))
                    .Batch(2)
                    .ToDictionary(
                        list => list.ElementAt(0).Trim(),
                        list => int.Parse(list.ElementAt(1)));
            }

            public int GetPopulation(string name)
            {
                return capitals[name];
            }
        }

        private class SingletonRecordFinder
        {
            public int GetTotalPopulation(IEnumerable<string> names)
            {
                int result = 0;
                foreach (var name in names)
                    result += SingletonDatabase.Instance.GetPopulation(name);
                
                return result;
            }
        }

        private class ConfigurableRecordFinder
        {
            private IDatabase database;

            public ConfigurableRecordFinder(IDatabase database)
            {
                this.database = database;
            }

            public int GetTotalPopulation(IEnumerable<string> names)
            {
                int result = 0;
                foreach (var name in names)
                    result += database.GetPopulation(name);

                return result;
            }
        }

        public class DummyDatabase : IDatabase
        {
            public int GetPopulation(string name)
            {
                return new Dictionary<string, int>
                {
                    ["alpha"] = 1,
                    ["beta"] = 2,
                    ["gamma"] = 3
                }[name];
            }
        }

        //not working
        [TestFixture]
        private class SingletonTests
        {
            [Test]
            public void IsSingletonTest()
            {
                var db = SingletonDatabase.Instance;
                var db2 = SingletonDatabase.Instance;
                Assert.That(db, Is.SameAs(db2));
                Assert.That(SingletonDatabase.Count, Is.EqualTo(2));
            }

            [Test]
            public void SingletonTotalPopulationTest()
            {
                var rf = new SingletonRecordFinder();
                var names = new[] { "Tokyo", "Beijing"};
                int tp = rf.GetTotalPopulation(names);
                Assert.That(tp, Is.EqualTo(14094034 + 21542000));
            }

            [Test]
            public void ConfigurablePopulationTest()
            {
                var rf = new ConfigurableRecordFinder(new DummyDatabase());
                var names = new[] { "alpha", "gamma" };
                int tp = rf.GetTotalPopulation(names);
                Assert.That(tp, Is.EqualTo(4));
            }

            [Test]
            public void DIPopulationTest() //Dependency Injection
            {
                var cb = new ContainerBuilder();
                cb.RegisterType<OrdinaryDatabase>()
                    .As<IDatabase>()
                    .SingleInstance();
                cb.RegisterType<ConfigurableRecordFinder>();

                using (var c = cb.Build())
                {
                    var rf = c.Resolve<ConfigurableRecordFinder>();
                }
            }
        }

        public void Demo()
        {
            var city = "Tokyo";
            var pop = SingletonDatabase.Instance.GetPopulation(city);
            Console.WriteLine($"{city} has population {pop}");
        }
    }
}
