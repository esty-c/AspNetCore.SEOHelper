# AspNetCore.SEOHelper
Helps to create routing  robots.txt  and sitemap.xml for asp.net core project.

  //routing for robots.txt
  ```csharp
  app.UseRobotsTxt(env.ContentRootPath);
  ```
   //routing for sitemap.txt
```csharp
  app.UseXMLSitemap(env.ContentRootPath);
 ```
