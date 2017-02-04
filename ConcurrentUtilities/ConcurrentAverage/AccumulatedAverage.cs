namespace ConcurrentUtilities.ConcurrentAverage
{
    using System;
    using System.Collections.Generic;
    using Helpers;

    internal struct AccumulatedAverage
    {
        private readonly int _sum;
        private readonly uint _count;

        public AccumulatedAverage(int sum, uint count)
        {
            if (count == 0 && sum != default(int)) { throw new ArgumentOutOfRangeException(nameof(count)); }

            _sum = sum;
            _count = count;
        }
        public int? Average => _count == 0 ? new int?() : _sum/(int)_count;

        public AccumulatedAverage Add(int value)
        {
            long count = (long)_count + 1;
            long sum = (long)_sum + value;
            bool hasOverflow = sum > int.MaxValue || sum < int.MinValue 
                          || count < uint.MinValue || count > uint.MaxValue;

            if (hasOverflow)
            {
                sum = sum/count;
                count = 1;
            }

            return new AccumulatedAverage((int)sum, (uint)count);
        }

        public long ToLong() => IntegerConverter.Join(_sum, (int)_count);

        public static AccumulatedAverage FromLong(long value)
        {
            KeyValuePair<int,int> vals = IntegerConverter.Split(value);
            return new AccumulatedAverage(vals.Key, (uint)vals.Value);
        }
    }
}
