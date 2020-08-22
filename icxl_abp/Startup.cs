using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icxl_abp.DBContext;
using icxl_abp.Domain;
using icxl_abp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Volo.Abp;

namespace icxl_abp
{
    public class Startup
    {


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddAuthorization().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(opts =>
            {
                // Force Camel Case to JSON
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //ignore Entity framework Navigation property back reference problem. Blog >> Posts. Post >> Blog. Blog.post.blog will been ignored.
                opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).AddJsonFormatters();


            services.AddEntityFrameworkNpgsql();
            var connStr = Configuration["ConnectionString"];
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connStr));



            services.Configure<AppConfig>(Configuration);

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));


            services.AddAuthentication("Bearer")
           .AddJwtBearer("Bearer", options =>
           {
               options.Authority = "http://" + Configuration["idsUrl:IP"] + ":" + Configuration["idsUrl:Port"];
               options.RequireHttpsMetadata = false;
               options.Audience = "api1";
           });


            services.AddApplication<AppModule>();

            return services.BuildServiceProviderFromFactory();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, IApplicationLifetime lifetime)
        {
            var settingsOptions = serviceProvider.GetService<IOptions<AppConfig>>();
            var appConfig = settingsOptions.Value;
            app.UseAuthentication();

            app.UseCors("AllowAll");
            app.RegisterConsul(lifetime, appConfig);



            #region Database Init
            {
                var dbContext = serviceProvider.GetService<AppDbContext>();
                dbContext.Database.Migrate();
                if (dbContext.Book.Count() == 0)
                {
                    Book a = new Book(Guid.NewGuid(),"1231",BookType.Adventure,DateTime.Now,123);
                    Book aa = new Book(Guid.NewGuid(), "4123123", BookType.Adventure, DateTime.Now, 123);
                    dbContext.Book.Add(a);
                    dbContext.Book.Add(aa);
                    dbContext.SaveChanges();
                }
            }
            #endregion

            app.InitializeApplication();


        }
    }
}
