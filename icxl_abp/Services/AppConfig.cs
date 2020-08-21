using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icxl_abp.Services
{
    public class AppConfig
    {
        public string ConnectionString { get; set; }
        public string MessageMail { get; set; }
        public string APIGatewayServer { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public SMTPSettings SMTPSettings { get; set; }
        public Plugins Plugins { get; set; }
        public ConsulConfig ConsulConfig { get; set; }

        public NodeMember idsUrl { get; set; }
    }

    /// <summary>
    /// Jwt配置
    /// </summary>
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int ExpiresDay { get; set; }
    }

    public class SMTPSettings
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Hosts { get; set; }
        public int Port { get; set; }
    }

    public class Plugins
    {
        public string OrderViewer { get; set; }
        public string MediaShare { get; set; }
    }

    public class ConsulConfig
    {
        public NodeMember Server { get; set; }
        public NodeMember Client { get; set; }
    }

    public class NodeMember
    {
        public string IP { get; set; }
        public string Port { get; set; }
    }
}
