#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Globalization;
using System.Windows;

namespace Phoenix.UI.Wpf.Converters
{
	/// <summary>
	/// Inverses the bound <see cref="Visibility"/>. Can be used to toggle controls dependent on another one.
	/// </summary>
	/// <remarks> ATTENTION: Directly binding another <see cref="UIElement"/> and using its <see cref="UIElement.Visibility"/> property later on will always fail, as the bound control itself does not raise change notifications. </remarks>
	[System.Windows.Data.ValueConversion(sourceType: typeof(Visibility), targetType: typeof(Visibility))]
	public class InverseVisibilityConverter : SourceValueToVisibilityConverter<InverseVisibilityConverter>
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//! Directly binding a UIElement will work only once, as the element doesn't raise change notifications for itself.
			//if (!(value is Visibility visibility))
			//{
			//	if (!(value is UIElement element)) return this.TrueVisibility;
			//	visibility = element.Visibility;
			//}

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