using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icxl_api.Filter
{
    public class logActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine(context.HttpContext.Request.Path);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine(context.HttpContext.Request.Path);
        }
    }
}
