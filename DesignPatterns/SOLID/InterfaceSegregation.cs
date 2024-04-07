namespace DesignPatterns.SOLID
{
    /// <summary>
    /// Interfaces should be segregated
    /// If some parts are not needed everywhere,
    /// we should break the interface in smaller pieces
    /// </summary>
    internal class InterfaceSegregation
    {
        private class Document { }

        private interface IMachine
        {
            void Print(Document d);
            void Scan(Document d);
            void Fax(Document d);
        }

        private class MultiFunctionalPrinter : IMachine
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

        private class OldFashionedPrinter : IMachine
        {
            public void Fax(Document d) => throw new NotImplementedException();
            public void Scan(Document d) => throw new NotImplementedException();
            public void Print(Document d)
            {
                //
            }
        }

        private interface IPrinter
        {
            void Print(Document d);
        }

        private interface IScanner
        {
            void Scan(Document d);
        }

        private class Photocopier : IPrinter, IScanner
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

        private interface IMultiFunctionDevice : IPrinter, IScanner { }

        private class MultiFunctionMachine : IMultiFunctionDevice
        {
            private IPrinter printer;
            private IScanner scanner;

            public MultiFunctionMachine(IPrinter printer, IScanner scanner)
            {
                this.printer = printer ?? throw new ArgumentNullException(paramName: (nameof(printer)));
                this.scanner = scanner ?? throw new ArgumentNullException(paramName: (nameof(scanner)));
            }

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
