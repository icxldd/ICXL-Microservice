﻿using System;
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

        private readonly IAccountRepository accountR;


        public HomeController(IAccountRepository a)
        {

            this.accountR = a;
        }



        [HttpPost]
        public ActionResult Post([FromBody]CreateAccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Forbid();
            }
            else
            {
                this.accountR.a(dto);
                return Ok();
            }
        }

    }
}