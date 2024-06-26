﻿namespace DesignPatterns.Prototype
{
    /// <summary>
    /// We want to get away from the copying in every class
    /// Implement serialization + deserialization -> deep copy
    /// Public because of extensions and serialization
    /// </summary>
    public class CopyThroughSerialization : IPatternDemo
    {
        public class Address
        {
            public string StreetAddress, City, Country;

            public Address() { }

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
        }

        public class Employee
        {
            public string Name;
            public Address Address;

            public Employee() { }

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
        }

        public void Demo()
        {
            Employee john = new("John", new Address("123 London Road", "London", "UK"));
            Employee? chris = john.DeepCopyXml();

            chris.Name = "Chris";
            chris.Address.StreetAddress = "321 London Road";
            Console.WriteLine(john);
            Console.WriteLine(chris);
        }
    }
}
