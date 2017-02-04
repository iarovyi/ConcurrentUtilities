namespace ConcurrentUtilities.Specs.ConcurrentAverage
{
    using System.Collections.Generic;
    using System.Linq;
    using Castle.Core.Internal;
    using ConcurrentUtilities.ConcurrentAverage;
    using FluentAssertions;
    using Xunit;

    public sealed class ConcurrentAveragesAccumulatorSpecs
    {
        public static IEnumerable<object[]> IntegerCollections
        {
            get
            {
                yield return new object[] { new[] { 0, 0, 0 } };
                yield return new object[] { new[] { -100, -100, 1000 } };
                yield return new object[] { new[] { int.MinValue, int.MinValue } };
                yield return new object[] { new[] { int.MaxValue, int.MaxValue } };
                yield return new object[] { new[] { int.MinValue, int.MaxValue } };
            }
        }

        [Fact]
        public void Should_have_no_results_if_have_no_data()
        {
            var counter = new ConcurrentAveragesAccumulator();

            int? average = counter.TryGetAverage("foo");

            average.Should().BeNull();
        }

        [Theory]
        [MemberData("IntegerCollections")]
        public void Should_be_able_to_calculate_average(IEnumerable<int> numbers)
        {
            var counter = new ConcurrentAveragesAccumulator();
            numbers.ForEach(n => counter.Add("foo", n));
            int expectedAverage = (int)numbers.Average();

            int? average = counter.TryGetAverage("foo");

            average.Should().Be(expectedAverage);
        }
    }
}
