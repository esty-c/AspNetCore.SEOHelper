using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCore.SEOHelper
{
    public static class RobotsTxtMiddlewareExtensions
    {
        public static IApplicationBuilder UseRobotsTxt(this IApplicationBuilder builder, string rootPath)
        {
            return builder.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/robots.txt"), b =>
                b.UseMiddleware<RobotsTxtMiddleware>(rootPath));
        }
    }

    public class RobotsTxtMiddleware
    {
        private const string Default = "User-agent: *  \nDisallow: /";

        private readonly RequestDelegate next;
        private readonly string environmentName;
        private readonly string rootPath;

        public RobotsTxtMiddleware(RequestDelegate next, string rootPath)
        {
            this.next = next;

            this.rootPath = rootPath;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/robots.txt"))
            {
                var generalRobotsTxt = Path.Combine(rootPath, "robots.txt");

                string output = String.Empty;

                if (File.Exists(generalRobotsTxt))
                {
                    output = await File.ReadAllTextAsync(generalRobotsTxt);
                }
                else
                {
                    output = Default;
                }

                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(output);
            }
            else
            {
                await next(context);
            }
        }
    }
}