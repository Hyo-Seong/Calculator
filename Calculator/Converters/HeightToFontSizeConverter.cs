using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Calculator.Converters
{
    public class HeightToFontSizeConverter : IValueConverter
    {
        // Height말고 Width도 받아와서 처리하자
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = System.Convert.ToInt32(value);
            if (System.Convert.ToInt32(value) != 0)
            {
                return result / 2;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
