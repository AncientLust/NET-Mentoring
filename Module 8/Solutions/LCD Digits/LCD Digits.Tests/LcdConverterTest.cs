using Xunit;
using FluentAssertions;

namespace LCD_Digits.Tests
{
    public class LcdConverterTest
    {
        [Theory]
        [InlineData( 0, "._.\n|.|\n|_|")]
        [InlineData( 1, "...\n..|\n..|")]
        [InlineData( 2, "._.\n._|\n|_.")]
        [InlineData( 3, "._.\n._|\n._|")]
        [InlineData( 4, "...\n|_|\n..|")]
        [InlineData( 5, "._.\n|_.\n._|")]
        [InlineData( 6, "._.\n|_.\n|_|")]
        [InlineData( 7, "._.\n..|\n..|")]
        [InlineData( 8, "._.\n|_|\n|_|")]
        [InlineData( 9, "._.\n|_|\n..|")]
        public void Each_Int_Should_Match_Its_String_Representation(int digitToConvert, string representation)
        {
            var converter = new LcdConverter();

            var result = converter.Convert(digitToConvert);

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

        [Fact]
        public void Should_Throw_ArgumentOutOfRangeException_If_Integer_Is_Non_Single_Digit()
        {
            var converter = new LcdConverter();
            var digitToConvert = 10;

            Action action = () => converter.Convert(digitToConvert);

            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
    }
}