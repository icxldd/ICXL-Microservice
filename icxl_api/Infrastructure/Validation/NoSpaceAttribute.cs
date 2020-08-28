using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace icxl_api.Validation
{
    public class NoSpaceAttribute : ValidationAttribute
    {
        private static readonly Regex _noSpaceRegex = new Regex(@"^[^\s]+$", RegexOptions.Compiled);

        public override bool IsValid(object value)
        {
            string stringValue = Convert.ToString(value, CultureInfo.CurrentCulture);

            if (string.IsNullOrEmpty(stringValue))
            {
                return true;
            }

            return _noSpaceRegex.IsMatch(stringValue);
        }
    }
}
