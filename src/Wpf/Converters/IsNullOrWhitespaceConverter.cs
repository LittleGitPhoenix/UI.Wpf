#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Globalization;
using System.Windows;

namespace Phoenix.UI.Wpf.Converters
{
	/// <summary>
	/// Checks if the bound <see cref="String"/> is null or whitespace.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(string), targetType: typeof(bool))]
	public class IsNullOrWhitespaceConverter : SourceValueConverter<IsNullOrWhitespaceConverter>
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null) return true;
			return String.IsNullOrWhiteSpace(value.ToString());
		}
	}

	/// <summary>
	/// Checks if the bound <see cref="String"/> is <c>NULL</c> or whitespace and converts it into configurable <see cref="Visibility"/>.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(string), targetType: typeof(Visibility))]
	public class IsNullOrWhitespaceToVisibilityConverter : InverseSourceValueToVisibilityConverter<IsNullOrWhitespaceToVisibilityConverter>
	{
		///// <summary> Evaluates <c>TRUE</c> to <see cref="Visibility.Collapsed"/> and <c>FALSE</c> to <see cref="Visibility.Visible"/>. </summary>
		//public new static readonly IValueConverter Instance = new IsNullOrWhitespaceToVisibilityConverter() { TrueVisibility = Visibility.Collapsed, FalseVisibility = Visibility.Visible };

		///// <summary> Evaluates <c>TRUE</c> to <see cref="Visibility.Visible"/> and <c>FALSE</c> to <see cref="Visibility.Collapsed"/>. </summary>
		//public new static readonly IValueConverter InverseInstance = new IsNullOrWhitespaceToVisibilityConverter() { TrueVisibility = Visibility.Visible, FalseVisibility = Visibility.Collapsed };

		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return String.IsNullOrWhiteSpace(value?.ToString()) ? this.TrueVisibility : this.FalseVisibility;
		}
	}
}