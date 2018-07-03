using System.Globalization;
using System.Windows.Controls;

namespace restaurant_manager.domain

{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
           
                return string.IsNullOrWhiteSpace((value ?? "").ToString())
                    ? new ValidationResult(false, "Это поле должно быть заполнено.")
                    : ValidationResult.ValidResult;
            
           
        }
    }
}
