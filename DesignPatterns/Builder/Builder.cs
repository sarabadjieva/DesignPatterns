using System.Text;

namespace DesignPatterns.Builder
{
    internal class Builder : IPatternDemo
    {
        class HtmlElement
        {
            private const int indentSize = 2;

            public string Name { get; set; }
            public string Text { get;set; }
            public List<HtmlElement> Elements { get; set; } = [];

            public HtmlElement() { }

            public HtmlElement(string name, string text)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Text = text ?? throw new ArgumentNullException(nameof(text));
            }

            private string ToStringImpl(int indent)
            {
                StringBuilder sb = new();
                string i = new(' ', indentSize * indent);
                sb.Append($"{i}<{Name}>\n");

                if (!string.IsNullOrWhiteSpace(Text))
                {
                    sb.Append(new string(' ', indentSize * (indent + 1)));
                    sb.AppendLine(Text);
                }

                foreach (HtmlElement e in Elements)
                {
                    sb.Append(e.ToStringImpl(indent + 1));
                }

                sb.AppendLine($"{i}</{Name}>");
                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }
        }

        class HtmlBuilder
        {
            private readonly string rootName;
            private HtmlElement root = new();

            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                this.root.Name = rootName;
            }

            public HtmlBuilder AddChild(string childName, string childText)
            {
                HtmlElement e = new(childName, childText);
                root.Elements.Add(e);
                return this;
            }

            public void Clear()
            {
                root = new() { Name = rootName };
            }

            public override string ToString()
            {
                return root.ToString();
            }
        }

        public void Demo()
        {
            // if you want to build a simple HTML paragraph using StringBuilder
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            Console.WriteLine(sb);

            // now I want an HTML list with 2 words in it
            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");
            Console.WriteLine(sb);

            HtmlBuilder builder = new("ul");

            //if we use void AddChild
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");

            //using fluent builder
            builder.AddChild("li", "fluent").AddChild("li", "builder");
            Console.WriteLine(builder);
        }
    }
}
