namespace ConcurrentUtilities.Specs.Helpers
{
    using System.Collections.Generic;
    using ConcurrentUtilities.Helpers;
    using FluentAssertions;
    using Xunit;

    public sealed class IntegerConverterSpecs
    {
        public static IEnumerable<object[]> IntegerPairs
        {
            get
            {
                yield return new object[] { 0,0 };
                yield return new object[] { -100, -100 };
                yield return new object[] { int.MinValue, int.MinValue };
                yield return new object[] { int.MaxValue, int.MaxValue };
                yield return new object[] { int.MinValue, int.MaxValue };
            }
        }

        [Theory]
        [MemberData("IntegerPairs")]
        public void Can_join_and_split_numbers(int number1, int number2)
        {
            long joined = IntegerConverter.Join(number1, number2);
            KeyValuePair<int,int> splitted = IntegerConverter.Split(joined);

            splitted.Key.Should().Be(number1);
            splitted.Value.Should().Be(number2);
        }
    }
} 