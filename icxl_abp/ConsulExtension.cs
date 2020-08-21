using Consul;
using icxl_abp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
namespace icxl_abp
{
    public static class ConsulExtension
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IApplicationLifetime lifetime, AppConfig config)
        {
            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{config.ConsulConfig.Server.IP}:{config.ConsulConfig.Server.Port}"));//请求注册的 Consul 地址
            var httpCheck = new AgentServiceCheck()
            {

                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册

                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔

                HTTP = $"http://{config.ConsulConfig.Client.IP}:{config.ConsulConfig.Client.Port}/api/health",//健康检查地址

                Timeout = TimeSpan.FromSeconds(5)
            };

            var registration = new AgentServiceRegistration()
            {

                Checks = new[] { httpCheck },

                ID = $"icxlabpservice-{config.ConsulConfig.Client.IP}:{config.ConsulConfig.Client.Port}",

                Name = "icxlabpservice",

                Address = $"{config.ConsulConfig.Client.IP}",

                Port = Convert.ToInt32(config.ConsulConfig.Client.Port),

                Tags = new[] { $"urlprefix-/icxlabpservice" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）

            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });

            return app;

        }

    }
}
