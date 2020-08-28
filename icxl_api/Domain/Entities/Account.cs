using icxl_api.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icxl_api.Entities
{
    public class Account
    {

        public string Id { get; set; }
        public string name { get; set; }
        public string password { get; set; }

    }

    public class CreateAccountDto
    {
        [Required(ErrorMessage = "商品ID不得为空")]
        public string Id { get; set; }

        [Required(ErrorMessage = "name不得为空")]
        [NoSpace(ErrorMessage ="不能包含空格")]
        public string name { get; set; }

        [Required(ErrorMessage = "用户password不得为空")]
        public string password { get; set; }
    }
}
