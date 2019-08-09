#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Globalization;

namespace Phoenix.UI.Wpf.Base.Converters
{
	/// <summary> 
	/// Inverses the bound <see cref="Boolean"/>.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(bool), targetType: typeof(bool))]
	public class InverseBooleanConverter : SourceValueConverter<HasElementsConverter>
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

			return !boolean;
		}

		/// <inheritdoc />
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> this.Convert(value, targetType, parameter, culture);
	}
}