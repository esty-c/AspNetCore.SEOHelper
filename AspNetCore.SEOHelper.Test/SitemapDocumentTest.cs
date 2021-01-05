using AspNetCore.SEOHelper.Sitemap;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AspNetCore.SEOHelper.Test
{
    public class SitemapDocumentTest
    {
        [Fact]
        public void CreateSitemap()
        {
            //arrange
            List<SitemapNode> list = new List<SitemapNode>();

            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = null, Url = "https://codingwithesty.com/serilog-mongodb-in-asp-net-core", Frequency = SitemapFrequency.Daily });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://codingwithesty.com/serilog-mongodb-in-asp-net-core", Frequency = null });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = null, Url = "https://codingwithesty.com/serilog-mongodb-in-asp-net-core", Frequency = null });

            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://codingwithesty.com/serilog-mongodb-in-asp-net-core", Frequency = SitemapFrequency.Daily });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://codingwithesty.com/logging-in-asp-net-core", Frequency = SitemapFrequency.Yearly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://codingwithesty.com/logging-in-asp-net-core", Frequency = SitemapFrequency.Yearly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://codingwithesty.com/robots-txt-in-asp-net-core", Frequency = SitemapFrequency.Monthly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.5, Url = "https://codingwithesty.com/versioning-asp.net-core-apiIs-with-swagger", Frequency = SitemapFrequency.Weekly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.4, Url = "https://codingwithesty.com/configuring-swagger-asp-net-core-web-api", Frequency = SitemapFrequency.Never });
            var baseDirectroy = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));

            // act
            new SitemapDocument().CreateSitemapXML(list, baseDirectroy);
            var items = new SitemapDocument().LoadFromFile(baseDirectroy);

            //  assert
            items.Count.Should().Be(9);
        }
    }
}