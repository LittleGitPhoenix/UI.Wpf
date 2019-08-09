#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Globalization;
using System.Windows;

namespace Phoenix.UI.Wpf.Base.Converters
{
	/// <summary>
	/// Checks if the bound <see cref="Boolean"/> is <c>True</c> or <c>False</c> and converts it into configurable <see cref="Visibility"/>.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(bool), targetType: typeof(Visibility))]
	public class BoolToVisibilityConverter : SourceValueToVisibilityConverter<BoolToVisibilityConverter>
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var boolean = false;
			try
			{
				boolean = System.Convert.ToBoolean(value);
			}
			catch { /*ignore*/ }
			
			return boolean ? this.TrueVisibility : this.FalseVisibility;
		}

		/// <inheritdoc />
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility visibility)
			{
				switch (visibility)
				{
					case Visibility.Visible: return true;
					default: return false;
				}
			}
			return false;
		}
	}
}