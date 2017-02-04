namespace ConcurrentUtilities.Specs.ConcurrentAverage
{
    using ConcurrentUtilities.ConcurrentAverage;
    using FluentAssertions;
    using Xunit;

    public sealed class AccumulatedAverageSpecs
    {
        [Fact]
        public void Should_have_no_results_if_have_no_data()
        {
            var counter = new AccumulatedAverage();

            int? average = counter.Average;

            average.Should().BeNull();
        }

        [Fact]
        public void Should_be_able_to_average_with_overfow()
        {
            var accumulated = new AccumulatedAverage()
                .Add(int.MaxValue)
                .Add(int.MaxValue)
                .Add(int.MaxValue);

            int? average = accumulated.Average;

            average.Should().Be(int.MaxValue);
        }
    }
}
