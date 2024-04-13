namespace DesignPatterns.Builder
{
    /// <summary>
    /// If you want several builders to build several aspects of an object
    /// Facade is another concept
    /// </summary>
    internal class FacetedBuilder : IPatternDemo
    {
        private class Person
        {
            //Address info
            public string StreetAddress { get; set; } = "";
            public string Postcode { get; set; } = "";
            public string City { get; set; } = "";

            //Employment info
            public string Company { get; set; } = "";
            public string Position { get; set; } = "";
            public int AnnualIncome { get; set; }

            public override string ToString()
            {
                return $"{nameof(StreetAddress)}: {StreetAddress}, " +
                    $"{nameof(Postcode)}: {Postcode}, " +
                    $"{nameof(City)}: {City}, " +
                    $"{nameof(Company)}: {Company}, " +
                    $"{nameof(Position)}: {Position}, " +
                    $"{nameof(AnnualIncome)}: {AnnualIncome}";
            }
        }

        //Facade - keeps a reference to the person and access to the builders
        private class PersonBuilder
        {
            //Reference!!
            protected Person person = new();

            public PersonJobBuilder Works => new PersonJobBuilder(person);
            public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

            public static implicit operator Person(PersonBuilder pb) => pb.person;
        }

        private class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person)
            {
                this.person = person;
            }

            public PersonJobBuilder At(string companyName)
            {
                person.Company = companyName;
                return this;
            }

            public PersonJobBuilder AsA(string position)
            {
                person.Position = position;
                return this;
            }

            public PersonJobBuilder Earning(int amount)
            {
                person.AnnualIncome = amount;
                return this;
            }
        }

        private class PersonAddressBuilder : PersonBuilder
        {
            public PersonAddressBuilder(Person person)
            {
                this.person = person;
            }

            public PersonAddressBuilder At(string streetAddress)
            { 
                person.StreetAddress = streetAddress;
                return this;
            }

            public PersonAddressBuilder WithPostcode(string postcode)
            {
                person.Postcode = postcode;
                return this;
            }

            public PersonAddressBuilder In(string city)
            {
                person.City = city;
                return this;
            }
        }

        public void Demo()
        {
            PersonBuilder pb = new();
            Person person = pb
                .Lives
                .At("123 London Road")
                .In("London")
                .WithPostcode("SW12AC")
                .Works
                .At("Company123")
                .AsA("Engineer")
                .Earning(100000);

            Console.WriteLine(person);
        }
    }
}
