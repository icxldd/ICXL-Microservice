using icxl_api.AppContext;
using icxl_api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icxl_api.IRepository
{
    public interface IRepository<T, DTO>
        where T : class, new()
        where DTO : class, new()
    {

    }
}
