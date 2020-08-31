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
        public string Name { get; set; }
        public string PassWord { get; set; }
    }

    public class AccountDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "name不得为空")]
        [NoSpace(ErrorMessage = "不能包含空格")]
        public string Name { get; set; }
        [Required(ErrorMessage = "用户password不得为空")]
        public string PassWord { get; set; }
    }
}
