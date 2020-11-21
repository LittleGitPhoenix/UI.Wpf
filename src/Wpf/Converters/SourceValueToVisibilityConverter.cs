#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System.Windows;
using System.Windows.Data;

namespace Phoenix.UI.Wpf.Converters
{
	/// <summary>
	/// Checks if the bound <c>value</c> for a condition and converts it into configurable <see cref="Visibility"/>. By default <c>True</c> is <see cref="Visibility.Visible"/> and <c>False</c> is <see cref="Visibility.Collapsed"/>.
	/// </summary>
	public abstract class SourceValueToVisibilityConverter<T> : SourceValueConverter<T>
		where T : SourceValueToVisibilityConverter<T>, new()
	{
		/// <summary> Evaluates <c>TRUE</c> to <see cref="Visibility.Collapsed"/> and <c>FALSE</c> to <see cref="Visibility.Visible"/>. </summary>
		public static readonly IValueConverter InverseInstance = new T() { TrueVisibility = Visibility.Collapsed, FalseVisibility = Visibility.Visible };

		public virtual Visibility TrueVisibility { get; set; } = Visibility.Visible;

		public virtual Visibility FalseVisibility { get; set; } = Visibility.Collapsed;
	}
}