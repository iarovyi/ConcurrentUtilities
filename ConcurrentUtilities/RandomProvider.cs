namespace ConcurrentUtilities
{
    using System;
    using System.Threading;

    internal static class RandomProvider
    {
        private static int _seed = Environment.TickCount;
        private static readonly ThreadLocal<Random> ThreadLocalRandom = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));
        public static Random GetThreadRandom() => ThreadLocalRandom.Value;

        public static int GetRandom(int minValue, int maxValue) 
            => GetThreadRandom().Next(minValue, maxValue);
    }
}
