using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using icxl_api.AppContext;
using icxl_api.Infrastructure.StartupInit.IOC;
using icxl_api.IRepository;
using icxl_api.Repository;
using icxl_api.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icxl_api.Infrastructure.ServiceCollection
{
    public static class ServiceCollection
    {
        public static IServiceCollection _AddAutoMapper(this IServiceCollection services)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperConfigs>());
            return services.AddAutoMapper();
        }

        public static void _AddCap(this IServiceCollection services)
        {
            services.AddCap(x =>
            {
                x.UseEntityFramework<AppDbContext>();
                x.UseRabbitMQ(mq =>
                {
                    mq.HostName = "localhost";
                    mq.Port = 5672;
                    mq.UserName = "guest";
                    mq.Password = "guest";
                });
                x.UseDashboard();
            });
        }

        public static void _AddAuthentication(this IServiceCollection services, string IP, string Port)
        {
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://" + IP + ":" + Port;
                options.RequireHttpsMetadata = false;
                options.Audience = "api1";
            });
        }

        public static AutofacServiceProvider _AddAutoFac(this IServiceCollection services, IContainer ApplicationContainer)
        {
            //初始化容器
            var builder = new ContainerBuilder();
            //管道寄居
            builder.Populate(services);
            RegisterRepository.Register(builder);//注册仓储
            //构造
            ApplicationContainer = builder.Build();
            //将AutoFac反馈到管道中
            return new AutofacServiceProvider(ApplicationContainer);
        }


    }
}
