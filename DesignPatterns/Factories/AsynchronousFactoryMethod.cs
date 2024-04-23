namespace DesignPatterns.Factories
{
    internal class AsynchronousFactoryMethod : IPatternDemo
    {
        private class Foo
        {
            private Foo() { }

            private async Task<Foo> InitAsync()
            {
                await Task.Delay(1000);
                return this;
            }

            //you can put parameters
            public static Task<Foo> CreateAsync()
            {
                Foo result = new();
                return result.InitAsync();
            }
        }

        public async void Demo()
        {
            //Foo foo = new();
            //await foo.InitAsync(); //you have to remember to do it -> bad

            Foo f = await Foo.CreateAsync();
        }
    }
}
