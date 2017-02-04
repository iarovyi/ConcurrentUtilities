namespace ConcurrentUtilities.Specs.ConcurrentAverage
{
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

        /*TODO
        [Fact]
        public void Should_add_values_thread_safely()
        {
            var counter = new ConcurrentAverageAccumulator();
            var range = Enumerable.Range(1, 100000);
            var expectedValue = (int)range.Average();

            range.AsParallel().ForEach(counter.Add);

            counter.GetAverage().Should().Be(expectedValue);
        }*/
    }
}
