namespace ConcurrentUtilities.Helpers
{
    using System.Collections.Generic;
    internal static class IntegerConverter
    {
        public static KeyValuePair<int, int> Split(long value) =>
            new KeyValuePair<int, int>((int)(value & uint.MaxValue), (int)(value >> 32));
        public static long Join(int left, int right) => ((long)right << 32) | (uint)left;
    }
}
