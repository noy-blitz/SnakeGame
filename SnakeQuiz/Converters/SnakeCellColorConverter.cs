using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SnakeQuiz.Converters
{
    // This class implements IValueConverter and is used to convert a boolean value
    // to a Brush color based on whether the cell is part of the snake or not.
    public class SnakeCellColorConverter : IValueConverter
    {
        // The Convert method takes a value (bool) and returns a Brush (Green for snake cell, White for empty cell).
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Casts the input value to a boolean to check if it represents a snake cell.
            bool isSnakeCell = (bool)value;
            // Returns a green brush if the cell is part of the snake, otherwise returns a white brush.
            return isSnakeCell ? Brushes.Green : Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
