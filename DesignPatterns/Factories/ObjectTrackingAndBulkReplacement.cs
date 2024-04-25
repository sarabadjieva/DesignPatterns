using System.Text;

namespace DesignPatterns.Factories
{
    internal class ObjectTrackingAndBulkReplacement : IPatternDemo
    {
        private interface ITheme
        {
            string TextColor { get; }
            string BgrColor { get; }
        }

        private class LightTheme : ITheme
        {
            public string TextColor => "black";
            public string BgrColor => "white";
        }

        private class DarkTheme : ITheme
        {
            public string TextColor => "white";
            public string BgrColor => "black";
        }

        private class TrackingThemeFactory
        {
            private readonly List<WeakReference<ITheme>> themes = [];

            public string Info
            {
                get
                {
                    StringBuilder sb = new();
                    foreach (var reference in themes)
                    {
                        if (reference.TryGetTarget(out var theme))
                        {
                            bool dark = theme is DarkTheme;
                            sb.Append(dark ? "Dark" : "Light")
                                .AppendLine(" theme");
                        }
                    }

                    return sb.ToString();
                }
            }
         
            public ITheme CreateTheme(bool dark)
            {
                ITheme theme = dark ? new DarkTheme() : new LightTheme();
                themes.Add(new WeakReference<ITheme>(theme));
                return theme;
            }
        }

        private class ReplacableThemeFactory
        {
            private readonly List<WeakReference<Ref<ITheme>>> themes = [];

            private ITheme CreateThemeImpl(bool dark)
            {
                return dark ? new DarkTheme() : new LightTheme();
            }

            public Ref<ITheme> CreateTheme(bool dark)
            {
                Ref<ITheme> r = new(CreateThemeImpl(dark));
                themes.Add(new(r));
                return r;
            }

            /// <summary>
            /// Replace every theme
            /// </summary>
            public void ReplaceTheme(bool dark)
            {
                foreach (var wr in themes)
                {
                    if (wr.TryGetTarget(out var reference))
                    {
                        reference.Value = CreateThemeImpl(dark);
                    }
                }
            }
        }

        private class Ref<T>(T value) where T : class
        {
            public T Value { get; set; } = value;
        }

        public void Demo()
        {
            TrackingThemeFactory factory = new();
            factory.CreateTheme(false);
            factory.CreateTheme(true);
            Console.Write(factory.Info);

            ReplacableThemeFactory factory2 = new();
            var magicTheme = factory2.CreateTheme(true);
            Console.WriteLine(magicTheme.Value.BgrColor);
            factory2.ReplaceTheme(false);
            Console.WriteLine(magicTheme.Value.BgrColor);
        }
    }
}
