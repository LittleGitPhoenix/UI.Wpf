﻿#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Phoenix.UI.Wpf.Converters
{
	/// <summary>
	/// Base class for custom converters.
	/// </summary>
	/// <remarks> Since this class inherits from <see cref="MarkupExtension"/> it can addressed directly in XAML. </remarks>
	/// <example>
	/// 
	/// Reference the assembly:
	/// -----------------------
	/// xmlns:phoenix="http://programming.little-phoenix.de/wpf/"
	/// 
	/// Use converter in binding:
	/// -------------------------
	/// <!-- Usage as MarkupExtension: -->
	/// <DataTrigger Binding="{Binding [BINDING_PROPERTY], Converter={phoenix:IsGreaterThanConverter}, ConverterParameter=0}" Value="true">
	///		<Setter Property = "[PROPERTY]" Value="[VALUE]"/>
	/// </DataTrigger>
	/// 
	/// </example>
	public abstract class SourceValueConverter : MarkupExtension, IValueConverter
	{
		/// <inheritdoc />
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}

		/// <summary>
		/// Converts a <paramref name="value"/>.
		/// </summary>
		/// <param name="value"> The value produced by the binding source. </param>
		/// <param name="targetType"> The type of the binding target property. </param>
		/// <param name="parameter"> The converter parameter to use. </param>
		/// <param name="culture"> The culture to use in the converter. </param>
		/// <returns> The converted <paramref name="value"/>. </returns>
		public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

		/// <summary>
		/// Converts a <paramref name="value"/> back.
		/// </summary>
		/// <param name="value"> The value that is produced by the binding target. </param>
		/// <param name="targetType"> The type to convert to. </param>
		/// <param name="parameter"> The converter parameter to use. </param>
		/// <param name="culture"> The culture to use in the converter. </param>
		/// <returns> The back converted <paramref name="value"/>. </returns>
		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new InvalidOperationException($"{this.GetType().Name} can only be used one way.");
		}
	}

	/// <summary>
	/// Base class for custom converters.
	/// </summary>
	/// <remarks> Since this class inherits from <see cref="MarkupExtension"/> using it doesn't require any static resources to be defined. </remarks>
	/// <example>
	/// 
	/// Reference the assembly:
	/// -----------------------
	/// xmlns:phoenix="http://programming.little-phoenix.de/wpf/"
	/// 
	/// Use converter in binding:
	/// -------------------------
	/// <!-- Usage via Instance property: -->
	/// <DataTrigger Binding="{Binding [BINDING_PROPERTY], Converter={x:Static phoenix:IsGreaterThanConverter.Instance}, ConverterParameter=0}" Value="true">
	///		<Setter Property = "[PROPERTY]" Value="[VALUE]"/>
	/// </DataTrigger>
	/// 
	/// </example>
	[Obsolete("Use the normal and not-generic version instead. This one allows access via its static 'Instance' property, which is superfluous because of the inheritance from 'MarkupExtension'.")]
	public abstract class SourceValueConverter<T> : SourceValueConverter
		where T : SourceValueConverter<T>, new()
	{
		public static readonly IValueConverter Instance = new T();
	}

	/// <summary>
	/// Checks if the bound <c>value</c> for a condition and converts it into configurable <see cref="Visibility"/>.
	/// </summary>
	public abstract class InverseSourceValueToVisibilityConverter<T> : SourceValueToVisibilityConverter<T>
		where T : SourceValueToVisibilityConverter<T>, new()
	{
		/// <summary> Evaluates <c>TRUE</c> to <see cref="Visibility.Visible"/> and <c>FALSE</c> to <see cref="Visibility.Collapsed"/>. </summary>
		public new static readonly IValueConverter InverseInstance = new T() { TrueVisibility = Visibility.Visible, FalseVisibility = Visibility.Collapsed };

		/// <inheritdoc />
		public override Visibility TrueVisibility { get; set; } = Visibility.Collapsed;

		/// <inheritdoc />
		public override Visibility FalseVisibility { get; set; } = Visibility.Visible;
	}
}