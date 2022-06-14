using System;
using System.Globalization;

namespace HoaDon.Helpers
{
	class GiaTienConverter : System.Windows.Data.IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null && !string.IsNullOrEmpty(value.ToString()))
			{
				return String.Format("{0:#,0}", int.Parse(value.ToString())) + ",000";
			}
			else
			{
				return value;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
