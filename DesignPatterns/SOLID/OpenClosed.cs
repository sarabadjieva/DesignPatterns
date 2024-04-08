namespace DesignPatterns.SOLID
{
    /// <summary>
    /// Classes should be open for extension (extend the functionality)
    /// But should be closed to modifications (shouldn't have to go back and start adding things)
    /// Do not change the main shipped/tested module, but add new classes
    /// </summary>
    internal class OpenClosed : IPatternDemo
    {
        enum Color { Red, Green, Blue }
        enum Size { Small, Medium, Large, Huge }

        class Product
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public Size Size { get; set; }

            public Product(string name, Color color, Size size)
            {
                if (name == null)
                    throw new ArgumentNullException(paramName: nameof(name));    
            
                Name = name;
                Color = color;
                Size = size;
            }
        }

        static class ProductFilter
        {
            public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
            {
                foreach (Product product in products)
                    if (product.Size == size)
                        yield return product;
            }

            public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (Product product in products)
                    if (product.Color == color)
                        yield return product;
            }

            //For example we want to add filtering by size and color
            //Instead of brute force, we could implement a pattern (specification pattern)
        }

        interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }

        interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        class ColorSpecification : ISpecification<Product>
        {
            private Color color;

            public ColorSpecification(Color color)
            {
                this.color = color;
            }

            public bool IsSatisfied(Product t)
            {
                return color == t.Color;
            }
        }

        class SizeSpecification : ISpecification<Product>
        {
            private Size size;

            public SizeSpecification(Size size)
            {
                this.size = size;
            }

            public bool IsSatisfied(Product t)
            {
                return size == t.Size;
            }
        }

        class AndSpecification<T> : ISpecification<T>
        {
            private ISpecification<T> first;
            private ISpecification<T> second;

            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
                this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
            }

            public bool IsSatisfied(T t)
            {
                return first.IsSatisfied(t) && second.IsSatisfied(t);
            }
        }

        class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach (var i in items)
                    if (spec.IsSatisfied(i))
                        yield return i;
            }
        }

        public void Demo()
        {
            Product[] products = {
                new Product("Apple", Color.Green, Size.Small),
                new Product("Tree", Color.Green, Size.Large),
                new Product("House", Color.Blue, Size.Large)
            };

            Console.WriteLine("Green products (old):");
            foreach (var product in ProductFilter.FilterByColor(products, Color.Green))
                Console.WriteLine($" - {product.Name} is green.");

            BetterFilter bf = new();
            Console.WriteLine("Green products (new): ");
            foreach (var product in bf.Filter(products, new ColorSpecification(Color.Green)))
                Console.WriteLine($" - {product.Name} is green.");

            Console.WriteLine("Large blue items");
            AndSpecification<Product> andSpecification = new(
                new ColorSpecification(Color.Blue),
                new SizeSpecification(Size.Large));
            foreach(var product in bf.Filter(products ,andSpecification))
                Console.WriteLine($" - {product.Name} is large and blue.");
        }
    }
}
