using System.Globalization;
using System.Windows.Controls;

namespace restaurant_manager.domain

{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                return string.IsNullOrWhiteSpace((value ?? "").ToString())
                    ? new ValidationResult(false, "Заполните поле.")
                    : ValidationResult.ValidResult;
            }
            return new ValidationResult(true, null);
        }
    }
}
