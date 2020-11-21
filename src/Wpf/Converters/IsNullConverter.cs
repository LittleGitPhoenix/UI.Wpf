#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Globalization;
using System.Windows;

namespace Phoenix.UI.Wpf.Converters
{
	/// <summary>
	/// Checks if the bound property is <c>NULL</c>.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(object), targetType: typeof(bool))]
	public class IsNullConverter : SourceValueConverter<IsNullConverter>
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value == null);
		}
	}
	
	/// <summary>
	/// Checks if the bound property is NOT <c>NULL</c>.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(object), targetType: typeof(bool))]
	public class IsNotNullConverter : SourceValueConverter<IsNotNullConverter>
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value != null);
		}
	}

	/// <summary>
	/// Checks if the bound <see cref="Object"/> is <c>NULL</c> and converts it into configurable <see cref="Visibility"/>.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(object), targetType: typeof(Visibility))]
	public class IsNullToVisibilityConverter : InverseSourceValueToVisibilityConverter<IsNullToVisibilityConverter>
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is null ? this.TrueVisibility : this.FalseVisibility;
		}
	}
}