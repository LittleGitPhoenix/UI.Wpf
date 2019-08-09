#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Phoenix.UI.Wpf.Base.Extensions
{
	/// <summary>
	/// Internally used by <see cref="Phoenix.UI.Wpf.Base.Converters.EnumToCollectionConverter"/> for building the <see cref="EnumDescription"/> collection.
	/// </summary>
	static class ExtendedEnumeration
	{
		/// <summary>
		/// Builds a collection containing all values from an enumeration as <see cref="EnumDescription"/>s from the a single value of the <see cref="Enum"/>.
		/// </summary>
		/// <param name="value"> The enum value. </param>
		/// <returns> A collection of <see cref="EnumDescription"/>s. </returns>
		/// <example> Color.Red.GetEnumDescriptions(); </example>
		internal static IEnumerable<EnumDescription> GetEnumDescriptions(this Enum value)
		{
			var type = value.GetType();
			var enumValues = Enum
				.GetValues(type)
				.Cast<Enum>()
				;

			foreach (var @enum in enumValues)
			{
				yield return new EnumDescription(@enum, @enum.GetDescription(), value.GetSelectionState(@enum));
			}
		}
		
		internal static bool GetSelectionState(this Enum originalEnum, Enum enumValue)
		{
			return originalEnum.HasFlag(enumValue);
		}

		internal static string GetDescription(this Enum value)
		{
			var descriptionAttribute = value
				.GetType()
				.GetField(value.ToString())
				.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
				.Cast<System.ComponentModel.DescriptionAttribute>()
				.FirstOrDefault()
				;

			if (descriptionAttribute?.Description != null) return descriptionAttribute.Description;

			// If no description could be found, replace all underscores with spaces and make the result TitleCase.
			var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
			return textInfo.ToTitleCase(textInfo.ToLower(value.ToString().Replace("_", " ")));
		}
	}
	
	/// <summary>
	/// Represents some properties of a single <see cref="Enum"/> value.
	/// </summary>
	/// <remarks> This is used by the <see cref="Phoenix.UI.Wpf.Base.Converters.EnumToCollectionConverter"/>. </remarks>
	public class EnumDescription : INotifyPropertyChanged
	{
		#region Delegates / Events
		
		/// <inheritdoc />
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		internal event EventHandler IsSelectedChanged;
		private void OnIsSelectedChanged()
		{
			this.IsSelectedChanged?.Invoke(this, EventArgs.Empty);
		}

		#endregion

		#region Constants
		#endregion

		#region Fields
		#endregion

		#region Properties

		/// <summary> The enumeration as its number representation. </summary>
		public long Number => Convert.ToInt64(this.Value);

		/// <summary> The <see cref="Enum"/> value. </summary>
		public Enum Value { get; }

		/// <summary> The description for the <see cref="Value"/>. </summary>
		public string Description { get; }

		/// <summary> Flag if the <see cref="Enum"/> is selected. </summary>
		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				if (value == _isSelected) return;
				_isSelected = value;
				this.OnIsSelectedChanged();
				this.OnPropertyChanged();
			}
		}
		private bool _isSelected;

		#endregion

		#region (De)Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value"> The <see cref="Enum"/> value itself. </param>
		/// <param name="description"> Its description. </param>
		/// <param name="isSelected"> Flag if the <see cref="Enum"/> is selected. </param>
		internal EnumDescription(Enum value, string description, bool isSelected)
		{
			this.Value = value;
			this.Description = description;
			this.IsSelected = isSelected;
		}

		#endregion

		#region Methods

		/// <summary> Returns a string representation of the object. </summary>
		public override string ToString() => $"[<{this.GetType().Name}> :: Value: {this.Value} | Description: {this.Description} | Selected: {this.IsSelected}]";

		#endregion
	}
}