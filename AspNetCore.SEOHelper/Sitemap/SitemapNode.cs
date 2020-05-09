using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace AspNetCore.SEOHelper.Sitemap
{
    public class SitemapNode
    {
        public SitemapFrequency? Frequency { get; set; }
        public DateTime? LastModified { get; set; }
        public double? Priority { get; set; }
        public string Url { get; set; }

     


    }


    internal class SitemapNodeSetter
    {
        public static SitemapFrequency? SetFrequency(string changefreq)
        {

            if (string.IsNullOrWhiteSpace(changefreq)) return null;


            SitemapFrequency frequency = (SitemapFrequency)Enum.Parse(typeof(SitemapFrequency), changefreq, true);
            return frequency;


        }

    }
    public enum SitemapFrequency
    {
        Never,
        Yearly,
        Monthly,
        Weekly,
        Daily,
        Hourly,
        Always
    }
}