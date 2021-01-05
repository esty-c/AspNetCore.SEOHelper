using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace AspNetCore.SEOHelper.Sitemap
{
    public class SitemapDocument
    {
        public void CreateSitemapXML(IEnumerable<SitemapNode> sitemapNodes, string directoryPath)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            document.Save(Path.Combine(directoryPath, "sitemap.xml"));
            // return document.ToString();
        }

        public List<SitemapNode> LoadFromFile(string folderPath)
        {
            List<SitemapNode> list = new List<SitemapNode>();
            XmlDocument xml = new XmlDocument();
            xml.Load(Path.Combine(folderPath, "sitemap.xml"));
            XmlNamespaceManager manager = new XmlNamespaceManager(xml.NameTable);
            manager.AddNamespace("", "http://www.sitemaps.org/schemas/sitemap/0.9"); //Empty prefix
                                                                                     //  XmlNodeList xnList = xml.SelectNodes("/sitemapindex/sitemap", manager);
            XmlNodeList xnList = xml.GetElementsByTagName("url");
            string GetValue(XmlNode node, string fieldName)
            {
                try
                {
                    var result = node[fieldName].InnerText;
                    if (string.IsNullOrWhiteSpace(result)) return "";
                    return result;
                }
                catch (Exception ex)
                { return ""; }
            }
            foreach (XmlNode node in xnList)
            {
                var loc = GetValue(node, "loc");                          //   node["loc"].InnerText;
                var priority = GetValue(node, "priority");               //   node["priority"].InnerText;
                var changefreq = GetValue(node, "changefreq");          // node["changefreq"].InnerText;
                var lastmod = GetValue(node, "lastmod");                // node["lastmod"].InnerText;

                list.Add(new SitemapNode
                {
                    Url = loc,
                    Priority = SitemapNodeSetter.SetPriority(priority),
                    Frequency = SitemapNodeSetter.SetFrequency(changefreq),
                    LastModified = DateTime.Parse(lastmod)
                });
            }

            return list;
        }
    }
}