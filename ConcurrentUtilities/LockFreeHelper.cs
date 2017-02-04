namespace ConcurrentUtilities
{
    using System;
    using System.Threading;

    internal static class LockFreeHelper
    {
        public static void Update(ref long field, Func<long, long> updateFn)
        {
            var spinner = new SpinWait();

            while (true)
            {
                long snapshot = field;
                if (Interlocked.CompareExchange(ref field, updateFn(snapshot), snapshot) == snapshot)
                {
                    return;
                }

                spinner.SpinOnce();
            }
        }
    }
}
