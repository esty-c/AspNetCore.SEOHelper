using FluentAssertions;
using Xunit;

namespace AspNetCore.SEOHelper.Test
{
    public class SEOFriendlyURLTest
    {
        [Fact]
        public void ToSeoQueryString()
        {
            string queryString = "Asp.Net MVC Tutorial Part-1";
            var text = queryString.ToSEOQueryString();
            text.Should().Be("asp-net-mvc-tutorial-part-1");
        }
    }
}