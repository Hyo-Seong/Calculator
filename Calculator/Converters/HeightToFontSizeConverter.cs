using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Calculator.Converters
{
    public class HeightToFontSizeConverter : IMultiValueConverter
    {
        // Height말고 Width도 받아와서 처리하자
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double height = System.Convert.ToDouble(values[0]);
            double width = System.Convert.ToDouble(values[1]);
            double denominator = System.Convert.ToDouble(parameter);
            double result = height >= width ? width / denominator : height / denominator;
            if (result < 15)
            {
                return result*1.1;
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
