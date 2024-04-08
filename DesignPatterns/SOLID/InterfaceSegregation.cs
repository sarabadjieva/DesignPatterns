namespace DesignPatterns.SOLID
{
    /// <summary>
    /// Interfaces should be segregated
    /// If some parts are not needed everywhere,
    /// we should break the interface in smaller pieces
    /// </summary>
    internal class InterfaceSegregation
    {
        class Document { }

        interface IMachine
        {
            void Print(Document d);
            void Scan(Document d);
            void Fax(Document d);
        }

        class MultiFunctionalPrinter : IMachine
        {
            public void Fax(Document d)
            {
                //
            }

            public void Print(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
                //
            }
        }

        class OldFashionedPrinter : IMachine
        {
            public void Fax(Document d) => throw new NotImplementedException();
            public void Scan(Document d) => throw new NotImplementedException();
            public void Print(Document d)
            {
                //
            }
        }

        interface IPrinter
        {
            void Print(Document d);
        }

        interface IScanner
        {
            void Scan(Document d);
        }

        class Photocopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
                //
            }
        }

        interface IMultiFunctionDevice : IPrinter, IScanner { }

        class MultiFunctionMachine(IPrinter printer, IScanner scanner) : IMultiFunctionDevice
        {
            private readonly IPrinter printer = printer ?? throw new ArgumentNullException(paramName: (nameof(printer)));
            private readonly IScanner scanner = scanner ?? throw new ArgumentNullException(paramName: (nameof(scanner)));

            public void Print(Document d)
            {
                printer.Print(d);
            }

            public void Scan(Document d)
            {
                scanner.Scan(d);
            }
        }
    }
}
