using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Ibrahim.Wpf.CustomControls.Converters
{
    public class HeightDivideTwoToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null)
            {
                double height = System.Convert.ToDouble(value);
                return new Thickness(height / 2, 0, 0, 0);
            }
            return new Thickness(15, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
