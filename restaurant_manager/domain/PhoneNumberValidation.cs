using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;
namespace restaurant_manager.domain
{
    public class PhoneNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = @"^\+\d{2}\(\d{3}\)\d{3}-\d{2}-\d{2}$";
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(value.ToString());
            return !(match.Success)? new ValidationResult(false, "Пример: «+38(044)555-55-55» ")
                    : ValidationResult.ValidResult;
        }
    }
}
