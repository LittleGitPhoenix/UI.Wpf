#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Globalization;
using System.Windows;

namespace Phoenix.UI.Wpf.Base.Converters
{
	/// <summary>
	/// Inverses the bound <see cref="Visibility"/>. Can be used to toggle controls dependent n one another.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(Visibility), targetType: typeof(Visibility))]
	public class InverseVisibilityConverter : SourceValueToVisibilityConverter<InverseVisibilityConverter>
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is Visibility visibility)) return this.TrueVisibility;

			if (visibility == this.TrueVisibility)
			{
				return this.FalseVisibility;
			}
			else
			{
				return this.TrueVisibility;
			}
		}
	}
}