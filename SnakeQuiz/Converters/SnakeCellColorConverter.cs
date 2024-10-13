using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SnakeQuiz.Converters
{
    public class SnakeCellColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSnakeCell = (bool)value;
            return isSnakeCell ? Brushes.Green : Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
