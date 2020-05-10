using FluentAssertions;
using Xunit;

namespace AspNetCore.SEOHelper.Test
{
    public class SEOFriendlyURLTest
    {
        [Fact]
        public void QueryStringTest()
        {
            string queryString = "Asp.Net MVC Tutorial Part-1";
            var text = queryString.ToSEOQueryString();
            text.Should().Be("asp-net-mvc-tutorial-part-1");
        }

        [Theory]
        [InlineData("The Lord of the Rings", "the-lord-of-the-rings")]
        [InlineData("Raúl González Blanco", "raul-gonzalez-blanco")]
        [InlineData("España", "espana")]
        [InlineData("Los 3 Mosqueteros", "los-3-mosqueteros")]
        // [InlineData("Real Madrid® C.F.", "real-madrid-cf")]

        public void QueryStringTests(string input, string output)
        {
            var text = input.ToSEOQueryString();
            text.Should().Be(output);
        }
    }
}