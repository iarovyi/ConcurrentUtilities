namespace ConcurrentUtilities.ConcurrentAverage
{
    using System.Collections.Concurrent;

    public class ConcurrentAveragesAccumulator
    {
        private readonly ConcurrentDictionary<string, AccumulatedAverage> _accumulativeAverages
            = new ConcurrentDictionary<string, AccumulatedAverage>();

        public void Add(string key, int value)
        {
            _accumulativeAverages.AddOrUpdate(key,
                (k) => new AccumulatedAverage(value, 1),
                (k, accumul) => accumul.Add(value));
        }

        public int? TryGetAverage(string key)
        {
            AccumulatedAverage value;
            return _accumulativeAverages.TryGetValue(key, out value) ? value.Average : null;
        }
    }
}
