using icxl_api.AppContext;
using icxl_api.Entities;
using icxl_api.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icxl_api.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;
        public AccountRepository(AppDbContext db)
        {
            _db = db;
        }


        public int a(CreateAccountDto dto)
        {
            var dtod = AutoMapper.Mapper.Map<Account>(dto);
            _db.Account.Add(dtod);
            _db.SaveChanges();
            return 2000;
        }

    }
}
