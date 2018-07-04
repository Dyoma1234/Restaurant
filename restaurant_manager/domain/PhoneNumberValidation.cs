using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Data;
namespace restaurant_manager.domain
{
    public class PhoneNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = GetBoundValue(value) as string;

            string pattern = @"^\+\d{2}\(\d{3}\)\d{3}-\d{2}-\d{2}$";
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(stringValue.ToString());
            return !(match.Success)? new ValidationResult(false, "Пример: «+38(044)555-55-55» ")
                    : ValidationResult.ValidResult;
        }
        private object GetBoundValue(object value)
        {
            if (value is BindingExpression)
            {
                // ValidationStep was UpdatedValue or CommittedValue (Validate after setting)
                // Need to pull the value out of the BindingExpression.
                BindingExpression binding = (BindingExpression)value;

                // Get the bound object and name of the property
                object dataItem = binding.DataItem;
                string propertyName = binding.ParentBinding.Path.Path;

                // Extract the value of the property.
                object propertyValue = dataItem.GetType().GetProperty(propertyName).GetValue(dataItem, null);

                // This is what we want.
                return propertyValue;
            }
            else
            {
                // ValidationStep was RawProposedValue or ConvertedProposedValue
                // The argument is already what we want!
                return value;
            }
        }
    }
}
