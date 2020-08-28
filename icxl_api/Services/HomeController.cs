using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icxl_api.Entities;
using icxl_api.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace icxl_api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IRepository<Account, AccountDto> accountR;


        public HomeController(IRepository<Account, AccountDto> a)
        {

            this.accountR = a;
        }
        [HttpGet]
        public string Get()
        {
            return "123";
        }

        [HttpPost]
        public ActionResult Post([FromBody]AccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Forbid();
            }
            else
            {
                //this.accountR.a(dto);
                return Ok();
            }
        }

    }
}