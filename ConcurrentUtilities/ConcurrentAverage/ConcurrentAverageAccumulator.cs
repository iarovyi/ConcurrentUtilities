namespace ConcurrentUtilities.ConcurrentAverage
{
    public class ConcurrentAverageAccumulator
    {
        private long _value;
        public void Add(int value)
        {
            LockFreeHelper.Update(ref _value,
                (old) => AccumulatedAverage.FromLong(old).Add(value).ToLong());
        }

        public int? GetAverage() => AccumulatedAverage.FromLong(_value).Average;
    }
}
