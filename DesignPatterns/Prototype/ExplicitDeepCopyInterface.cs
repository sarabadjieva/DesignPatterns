namespace DesignPatterns.Prototype
{
    /// <summary>
    /// 
    /// </summary>
    internal class ExplicitDeepCopyInterface : IPatternDemo
    {
        private interface IPrototype<T>
        {
            T DeepCopy();
        }

        private class Address : IPrototype<Address>
        {
            public string StreetAddress, City, Country;

            public Address(string streetAddress, string city, string country)
            {
                StreetAddress = streetAddress ?? throw new ArgumentNullException(paramName: nameof(streetAddress));
                City = city ?? throw new ArgumentNullException(paramName: nameof(city));
                Country = country ?? throw new ArgumentNullException(paramName: nameof(country));
            }

            public Address(Address other)
            {
                StreetAddress = other.StreetAddress;
                City = other.City;
                Country = other.Country;
            }

            public override string ToString()
            {
                return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(City)}: {City}, {nameof(Country)}: {Country}";
            }

            public Address DeepCopy()
            {
                return new Address(StreetAddress, City, Country);
            }
        }

        private class Employee : IPrototype<Employee>
        {
            public string Name;
            public Address Address;

            public Employee(string name, Address address)
            {
                Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
                Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
            }

            public Employee(Employee other)
            {
                Name = other.Name;
                Address = new Address(other.Address);
            }

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
            }

            public Employee DeepCopy()
            {
                return new Employee(Name, Address.DeepCopy());
            }
        }

        public void Demo()
        {
            Employee john = new("John", new Address("123 London Road", "London", "UK"));

            //var chris = john;
            Employee chris = john.DeepCopy();

            chris.Name = "Chris";
            chris.Address.StreetAddress = "321 London Road";
            Console.WriteLine(john); // if reference -> john is called chris
            Console.WriteLine(chris);
        }
    }
}
