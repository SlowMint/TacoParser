using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething()
        {
            //Arrange
            var tacoParser = new TacoParser();
            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");
            //Assert
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        [InlineData("34.035985,-84.683302,Taco Bell Acworth...", -84.683302)]
        [InlineData("34.901154,-85.136345,Taco Bell Ringgol...", -85.136345)]
        [InlineData("33.926654,-87.757477,Taco Bell Winfiel...", -87.757477)]
        public void ShouldParseLongitude(string line, double expected)
        {
            //Arrange
            var longitudeTester = new TacoParser();
            //Act
            var actual = longitudeTester.Parse(line);
            //Assert
            Assert.Equal(expected, actual.Location.Longitude);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        [InlineData("34.035985,-84.683302,Taco Bell Acworth...", 34.035985)]
        [InlineData("34.901154,-85.136345,Taco Bell Ringgol...", 34.901154)]
        [InlineData("33.926654,-87.757477,Taco Bell Winfiel...", 33.926654)]
        public void ShouldParseLatitude(string line, double expected)
        {
            //Arrange
            var latitudeTester = new TacoParser();
            //Act
            var actual = latitudeTester.Parse(line);
            //Assert
            Assert.Equal(expected, actual.Location.Latitude);
        }
    }
}
