namespace DesignPatterns.Singleton
{
    /// <summary>
    /// Could help to create one object per thread
    /// </summary>
    internal sealed class PerThreadSingleton : IPatternDemo
    {
        private static ThreadLocal<PerThreadSingleton> threadInstance
        = new(() => new PerThreadSingleton());

        private static PerThreadSingleton Instance => threadInstance.Value;

        private int Id;

        public PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;    
        }

        public void Demo()
        {
            var t1 = Task.Factory.StartNew (() =>
            {
                Console.WriteLine("t1: " + PerThreadSingleton.Instance.Id);
            });

            var t2 = Task.Factory.StartNew (() =>
            {
                Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
                Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
            });

            Task.WaitAll(t1, t2);
        }
    }
}