using craftersmine.Valknut.Launcher.Wpf.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace craftersmine.Valknut.Launcher.Wpf
{
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            string val = Convert.ToString(value);

            if (string.IsNullOrWhiteSpace(val))
                return new ValidationResult(false, Resources.Validation_Email_FieldCannotBeEmpty);

            bool isEmailValid = Regex.IsMatch(val, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

            if (isEmailValid)
                return new ValidationResult(true, null);
            else return new ValidationResult(false, Resources.Validation_Email_FieldContentsIsNotEmail);
        }
    }
}
