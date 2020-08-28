using AutoMapper;
using icxl_api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icxl_api.Services
{
    public class AutoMapperConfigs:Profile
    {
        public AutoMapperConfigs() {

            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
        }


    }
}
