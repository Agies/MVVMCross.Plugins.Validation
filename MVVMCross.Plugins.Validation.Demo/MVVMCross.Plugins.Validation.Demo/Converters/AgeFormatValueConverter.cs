using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCross.Plugins.Validation.Demo.Converters
{
    public class AgeFormatValueConverter : MvxValueConverter<int, string>
    {
        protected override string Convert(int value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == 0)
                return "";

            return value.ToString();
        }

        protected override int ConvertBack(string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var num = 0;
            int.TryParse(value, out num);
            return num;
        }
    }
}
