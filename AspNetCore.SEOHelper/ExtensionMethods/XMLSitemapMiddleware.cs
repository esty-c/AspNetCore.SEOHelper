using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCore.SEOHelper
{
    public static class XMLSitemapMiddlewareExtensions
    {
        public static IApplicationBuilder UseXMLSitemap(this IApplicationBuilder builder, string rootPath)
        {
            return builder.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/sitemap.xml"), b =>
                b.UseMiddleware<XMLSitemapMiddleware>(rootPath));
        }
    }

    public class XMLSitemapMiddleware
    {
        private const string Default = "";

        private readonly RequestDelegate next;

        private readonly string rootPath;

        public XMLSitemapMiddleware(RequestDelegate next, string rootPath)
        {
            this.next = next;

            this.rootPath = rootPath;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/sitemap.xml"))
            {
                var generalRobotsTxt = Path.Combine(rootPath, "sitemap.xml");

                string output = String.Empty;

                if (File.Exists(generalRobotsTxt))
                {
                    output = await File.ReadAllTextAsync(generalRobotsTxt);
                }
                else
                {
                    output = Default;
                }

                context.Response.ContentType = "text/xml";
                await context.Response.WriteAsync(output);
            }
            else
            {
                await next(context);
            }
        }
    }
}