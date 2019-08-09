#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Phoenix.UI.Wpf.Base.Extensions;

namespace Phoenix.UI.Wpf.Base.Converters
{
	/// <summary>
	/// Converts the underlying <see cref="Enum"/> of the passed value into a collection of <see cref="EnumDescription"/>.
	/// </summary>
	/// <example>
	/// <para>ItemsSource="{Binding SomeEnumProperty, Converter={phoenix:EnumToCollectionConverter}, Mode=OneWay}"</para>
	/// <para>SelectedValue="{Binding SomeEnumProperty}"</para>
	/// <para>SelectedValuePath="Value"</para>
	/// <para>DisplayMemberPath="Description"</para>
	/// </example>
	[System.Windows.Data.ValueConversion(sourceType: typeof(Enum), targetType: typeof(System.Collections.Generic.IList<EnumDescription>))]
	public class EnumToCollectionConverter : SourceValueConverter<EnumToCollectionConverter>
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null) throw new ArgumentNullException(nameof(value));
			if (!(value is Enum originalEnum)) throw new ArgumentException($"{nameof(value)} must be of type {typeof(Enum)}");

			return new ReadOnlyObservableCollection<EnumDescription>
			(
				new ObservableCollection<EnumDescription>
				(
					originalEnum.GetEnumDescriptions()
				)
			);
		}

		/// <inheritdoc />
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!typeof(Enum).IsAssignableFrom(targetType))
			{
				throw new ArgumentException($"The target type must be '{nameof(Enum)}' but actually is '{targetType?.Name ?? "[UNKNOWN]"}'", nameof(targetType));
			}

			long number = 0;
			if (value is IList<EnumDescription> enumDescriptions)
			{
				number = enumDescriptions
					.Where(description => description.IsSelected)
					.Select(description => description.Number)
					.Aggregate(seed: (long) 0, (a, b) => a | b)
					;
			}
			return Enum.ToObject(targetType, number);
		}
	}
}