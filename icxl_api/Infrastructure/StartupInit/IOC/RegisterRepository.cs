using Autofac;
using icxl_api.Entities;
using icxl_api.IRepository;
using icxl_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icxl_api.Infrastructure.StartupInit.IOC
{
    public class RegisterRepository
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<AccountRepository>().As<IRepository<Account, AccountDto>>();
        }
    }
}
