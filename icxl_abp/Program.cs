using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.InProcess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace icxl_abp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
                https://github.com/aspnet/AspNetCore/issues/4206#issuecomment-445612167
                CurrentDirectoryHelpers 文件位于: \framework\src\Volo.Abp.AspNetCore.Mvc\Microsoft\AspNetCore\InProcess\CurrentDirectoryHelpers.cs
                当升级到ASP.NET Core 3.0的时候将会删除这个类.
            */
            CurrentDirectoryHelpers.SetCurrentDirectory();

            BuildWebHostInternal(args).Run();
        }
        public static IWebHost BuildWebHostInternal(string[] args) =>
           new WebHostBuilder()
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIIS()
               .UseIISIntegration()
               .UseStartup<Startup>()
               .Build();
    }
}
