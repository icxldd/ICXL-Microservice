using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icxl_api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icxl_api.Services
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private AppContext.AppDbContext _db;
        public MenuController(AppContext.AppDbContext db)
        {
            this._db = db;
        }


        // GET: api/Menu
        [HttpGet]
        public IEnumerable<MenuDTO> Get()
        {
            List<MenuDTO> objs = new List<MenuDTO>();

            IQueryable<Menu> items = _db.Menu.Where(x => x.parentId.Equals(""));
            foreach (Menu item in items)
            {
                MenuDTO dto = new MenuDTO();
                dto.Parent = item;
                dto.Childs = new List<Menu>();
                IQueryable<Menu> _items = _db.Menu.Where(x => x.parentId.Equals(item.id));
                dto.Childs = _items.ToList();
                objs.Add(dto);
            }

            return objs;
        }




    }
}
