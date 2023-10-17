using Xunit;
using FluentAssertions;

namespace LCD_Digits.Tests
{
    public class LcdConverterTest
    {

        private const string Zero = """
                                    ._.
                                    |.|
                                    |_|
                                    """;

        private const string One = """
                                    ...
                                    ..|
                                    ..|
                                    """;

        private const string Two = """
                                   ._.
                                   ._|
                                   |_.
                                   """;

        private const string Three = """
                                   ._.
                                   ._|
                                   ._|
                                   """;

        private const string Four = """
                                   ...
                                   |_|
                                   ..|
                                   """;

        private const string Five = """
                                   ._.
                                   |_.
                                   ._|
                                   """;

        private const string Six = """
                                    ._.
                                    |_.
                                    |_|
                                    """;

        private const string Seven = """
                                    ._.
                                    ..|
                                    ..|
                                    """;

        private const string Eight = """
                                    ._.
                                    |_|
                                    |_|
                                    """;

        private const string Nine = """
                                     ._.
                                     |_|
                                     ..|
                                     """;

        [Theory]
        [InlineData( 0, Zero)]
        [InlineData( 1, One)]
        [InlineData( 2, Two)]
        [InlineData( 3, Three)]
        [InlineData( 4, Four)]
        [InlineData( 5, Five)]
        [InlineData( 6, Six)]
        [InlineData( 7, Seven)]
        [InlineData( 8, Eight)]
        [InlineData( 9, Nine)]
        public void Each_Int_Should_Match_Its_String_Representation(int digitToConvert, string representation)
        {
            var converter = new LcdConverter();

            var result = converter.Convert(digitToConvert);

            Assert.Equal(representation, result);
        }

        [Fact]
        public void Should_Correctly_Represent_Two_DigitNumbers()
        {
            var converter = new LcdConverter();
            var numberToConvert = 23;
            var representation = """
                                 ._. ._.
                                 ._| ._|
                                 |_. ._|
                                 """;

            var result = converter.Convert(numberToConvert);

            Assert.Equal(representation, result);
        }

        [Fact]
        public void Should_Correctly_Represent_Three_DigitNumbers()
        {
            var converter = new LcdConverter();
            var numberToConvert = 910;
            var representation = """
                                 ._. ... ._.
                                 |_| ..| |.|
                                 ..| ..| |_|
                                 """;

            var result = converter.Convert(numberToConvert);

            Assert.Equal(representation, result);
        }

        [Fact]
        public void Should_Correctly_Represent_Four_DigitNumbers()
        {
            var converter = new LcdConverter();
            var numberToConvert = 9180;
            var representation = """
                                 ._. ... ._. ._.
                                 |_| ..| |_| |.|
                                 ..| ..| |_| |_|
                                 """;

            var result = converter.Convert(numberToConvert);

            Assert.Equal(representation, result);
        }

        [Fact]
        public void Should_Throw_ArgumentOutOfRangeException_If_Integer_Is_Negative()
        {
            var converter = new LcdConverter();
            var digitToConvert = -1;

            Action action = () => converter.Convert(digitToConvert);

            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
    }
}