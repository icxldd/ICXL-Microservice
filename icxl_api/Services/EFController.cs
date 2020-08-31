using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DotNetCore.CAP;
using icxl_api.AppContext;
using icxl_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Npgsql;

namespace icxl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFController : ControllerBase
    {
        private readonly ICapPublisher _capBus;
        private readonly AppDbContext _db;

        public EFController(ICapPublisher capPublisher, AppDbContext db)
        {
            _capBus = capPublisher;
            _db = db;
        }
        //// GET: api/EF
        //[HttpGet]
        //public string Get()
        //{
        //    string str = JsonConvert.SerializeObject(_db.Account.ToList());
        //    return str;
        //}
        //[Route("icxl")]
        //public string icxl()
        //{
        //    _capBus.Publish("icxl", DateTime.Now);
        //    return JsonConvert.SerializeObject(_db.Account.ToList());
        //}
        //[NonAction]
        //[CapSubscribe("icxl")]
        //public void icxlcallback(DateTime datetime)
        //{
        //    System.Console.WriteLine("icxlcallback" + datetime.ToString());
        //}

        //[Route("cc")]
        //public string cc()
        //{
        //    _db.Account.Add(new Account()
        //    {
        //        Id = "cc" + DateTime.Now.ToString(),
        //        Name = "2",
        //        PassWord = "3"
        //    });
        //    _db.SaveChanges();
        //    _capBus.Publish("test.scope.ef.success.a.check", DateTime.Now);
        //    return JsonConvert.SerializeObject(_db.Account.ToList());
        //}



        //[Route("shiwu")]
        //public string shiwu()
        //{
        //    var transaction = _db.Database.BeginTransaction();
        //    _db.Account.Add(new Account()
        //    {
        //        Id = "shiwu" + DateTime.Now.ToString(),
        //        Name = "2",
        //        PassWord = "3"
        //    });
        //    _db.SaveChanges();
        //    _capBus.Publish("ShiWuReceivedMessage", transaction);

        //    return "";
        //}
        //[NonAction]
        //[CapSubscribe("ShiWuReceivedMessage")]
        //public void ShiWuReceivedMessage(IDbContextTransaction t)
        //{
        //    _db.Account.Add(new Account()
        //    {
        //        Id = "ShiWuReceivedMessage" + DateTime.Now.ToString(),
        //        Name = "2",
        //        PassWord = "3"
        //    });
        //    _db.SaveChanges();
        //    t.Commit();
        //}


        //[NonAction]
        //[CapSubscribe("test.scope.ef.success.a.check")]
        //public void CheckTestAReceivedMessage(DateTime datetime)
        //{
        //    _db.Account.Add(new Account()
        //    {
        //        Id = "CheckTestAReceivedMessage" + DateTime.Now.ToString(),
        //        Name = "2",
        //        PassWord = "3"
        //    });
        //    _db.SaveChanges();

        //}
        //// GET: api/EF/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/EF
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/EF/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
