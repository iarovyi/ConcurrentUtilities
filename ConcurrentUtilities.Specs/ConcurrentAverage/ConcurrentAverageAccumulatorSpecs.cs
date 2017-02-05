namespace ConcurrentUtilities.Specs.ConcurrentAverage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Castle.Core.Internal;
    using ConcurrentUtilities.ConcurrentAverage;
    using FluentAssertions;
    using Xunit;

    public sealed class ConcurrentAverageAccumulatorSpecs
    {
        public static IEnumerable<object[]> IntegerCollections
        {
            get
            {
                yield return new object[] { new [] { 0, 0, 0 } };
                yield return new object[] { new[] { -100, -100, 1000 } };
                yield return new object[] { new[] { int.MinValue, int.MinValue } };
                yield return new object[] { new[] { int.MaxValue, int.MaxValue } };
                yield return new object[] { new[] { int.MinValue, int.MaxValue } };
            }
        }

        [Fact]
        public void Should_have_no_results_if_have_no_data()
        {
            var counter = new ConcurrentAverageAccumulator();

            int? average = counter.GetAverage();

            average.Should().BeNull();
        }

        [Theory]
        [MemberData("IntegerCollections")]
        public void Should_be_able_to_calculate_average(IEnumerable<int> numbers)
        {
            var counter = new ConcurrentAverageAccumulator();
            numbers.ForEach(n => counter.Add(n));
            int expectedAverage = (int)numbers.Average();
            
            int? average = counter.GetAverage();

            average.Should().Be(expectedAverage);
        }

        [Fact]
        //TODO: optimize resource consumption
        public void Should_add_values_thread_safely()
        {
            var counter = new ConcurrentAverageAccumulator();
            int[] range = Enumerable.Range(1, 10000).OrderBy(i=> Guid.NewGuid()).ToArray();
            int expectedAverage = range.Sum()/range.Count();

            Parallel.ForEach(range, new ParallelOptions { MaxDegreeOfParallelism = 20 }, counter.Add);
            var average = counter.GetAverage();

            average.Should().Be(expectedAverage);
        }
    }
}
