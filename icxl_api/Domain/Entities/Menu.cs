using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icxl_api.Domain.Entities
{
    public class Menu
    {
        public string id { get; set; }

        public string url { get; set; }

        public string name { get; set; }

        public string parentId { get; set; }

    }


    public class MenuDTO
    {
        public Menu Parent { get; set; }


        public List<Menu> Childs { get; set; }
    }

    
}
