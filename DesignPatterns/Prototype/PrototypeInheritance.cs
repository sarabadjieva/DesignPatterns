namespace DesignPatterns.Prototype
{
    /// <summary>
    /// Deep copying with inheritance
    /// We are reusing base class functionallity
    /// We don't need multiple ctors, just empty
    /// The classes here are not private as we are using extensions
    /// </summary>
    internal class PrototypeInheritance : IPatternDemo
    {
        internal interface IDeepCopyable<T>
            where T : new() //implements an empty ctor
        {
            public void CopyTo(T target);
            
            //default interface member
            public T DeepCopy()
            {
                T t = new();
                CopyTo(t);
                return t;
            }
        }

        internal class Address : IDeepCopyable<Address>
        {
            public string StreetName;
            public int HouseNumber;

            public Address() { }

            //public Address(string streetName, int houseNumber)
            //{
            //    StreetName = streetName;
            //    HouseNumber = houseNumber;
            //}

            public void CopyTo(Address target)
            {
                target.StreetName = StreetName;
                target.HouseNumber = HouseNumber;
            }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }
        }

        internal class Person : IDeepCopyable<Person>
        {
            public string[] Names;
            public Address Address;

            public Person() { }

            //public Person(string[] names, Address address)
            //{
            //    Names = names;
            //    Address = address;
            //}

            public void CopyTo(Person target)
            {
                target.Names = (string[])Names.Clone();
                target.Address = Address.DeepCopy();

            }

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
            }
        }

        private class Employee : Person, IDeepCopyable<Employee>
        {
            public int Salary;

            public Employee() { }

            //public Employee(string[] names, Address address, int salary) : base(names, address)
            //{
            //    Salary = salary;
            //}

            public override string ToString()
            {
                return base.ToString() + $" {nameof(Salary)}: {Salary}";
            }

            public void CopyTo(Employee target)
            {
                base.CopyTo(target);
                target.Salary = Salary;
            }
        }

        public void Demo()
        {
            Employee john = new()
            {
                Names = ["John", "Doe"],
                Address = new()
                {
                    HouseNumber = 123,
                    StreetName = "London Road"
                },
                Salary = 321000
            };

            Employee copy = john.DeepCopy();
            Person copyPerson = john.DeepCopy<Person>();

            copy.Names[1] = "Smith";
            copy.Address.HouseNumber++;
            copy.Salary = 123000;

            copyPerson.Names[0] = "Jack";

            Console.WriteLine(john);
            Console.WriteLine(copy);
            Console.WriteLine(copyPerson);
        }
    }
}
