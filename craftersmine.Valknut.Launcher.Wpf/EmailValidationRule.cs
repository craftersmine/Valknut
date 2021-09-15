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
    public class EmailValidationRule
    {
        public static bool Validate(string value)
        {
            
            string val = Convert.ToString(value);

            if (string.IsNullOrWhiteSpace(val))
                return false;

            bool isEmailValid = Regex.IsMatch(val, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

            if (isEmailValid)
                return true;
            else return false;
        }
    }
}
