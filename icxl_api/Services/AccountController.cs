using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icxl_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace icxl_api.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private AppContext.AppDbContext _db;
        public AccountController(AppContext.AppDbContext db)
        {
            this._db = db;
        }
        // GET: api/Account
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return _db.Account.ToList();
        }


        // POST: api/Account
        [HttpPost]
        public ActionResult Post([FromBody] AccountDto value)
        {
            value.Id = Guid.NewGuid().ToString();
            var dtod = AutoMapper.Mapper.Map<Account>(value);
            _db.Account.Add(dtod);
            _db.SaveChanges();
            return Ok();
        }

    }
}
