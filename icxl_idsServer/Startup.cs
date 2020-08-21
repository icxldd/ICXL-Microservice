using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icxl_idsServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SqlSugar;

namespace icxl_idsServer
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(opts =>
            {
                 opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                 opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.Configure<AppConfig>(Configuration);

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()));

            services.AddIdentityServer().AddDeveloperSigningCredential().AddInMemoryIdentityResources(Config.GetIdentityResourceResources())
          .AddInMemoryApiResources(Config.GetApiResources())
          .AddInMemoryClients(Config.GetClients()).AddResourceOwnerValidator<ResourceOwnerPasswordValidator>().AddProfileService<ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IServiceProvider serviceProvider, IApplicationLifetime lifetime)
        {
            var settingsOptions = serviceProvider.GetService<IOptions<AppConfig>>();
            var appConfig = settingsOptions.Value;

            app.UseCors("AllowAll");

            app.UseDeveloperExceptionPage();
            app.UseIdentityServer();
            app.RegisterConsul(lifetime, appConfig);


            DB.dao = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = appConfig.ConnectionString,//必填, 数据库连接字符串
                DbType = DbType.PostgreSQL,         //必填, 数据库类型
                IsAutoCloseConnection = true,       //默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
                InitKeyType = InitKeyType.SystemTable    //默认SystemTable, 字段信息读取, 如：该属性是不是主键，是不是标识列等等信息
            });

            app.UseMvc();
        }
    }
}
