#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Phoenix.UI.Wpf.Base.Extensions;

//namespace Phoenix.UI.Wpf.Base.Helper
//{
//	public sealed class BindableFlagsEnum<TEnum>
//		where TEnum : Enum
//	{
//		#region Delegates / Events

//		public event EventHandler FlagsChanged;
//		private void OnFlagsChanged()
//		{
//			this.UnderlyingEnum = BindableFlagsEnum<TEnum>.GetEnum(this.EnumValues);
//			this.FlagsChanged?.Invoke(this, EventArgs.Empty);
//		}

//		#endregion

//		#region Constants
//		#endregion

//		#region Fields
//		#endregion

//		#region Properties

//		public TEnum UnderlyingEnum { get; private set; }

//		public IReadOnlyCollection<EnumDescription> EnumValues { get; }

//		#endregion

//		#region (De)Constructors

//		public BindableFlagsEnum() : this((TEnum) Enum.ToObject(typeof(TEnum), 0)) { }

//		public BindableFlagsEnum(TEnum value)
//		{
//			// Save parameters.
//			this.UnderlyingEnum = value;

//			// Initialize fields.
//			var enumDescriptions = value.GetEnumDescriptions().ToList();
//			foreach (var enumDescription in enumDescriptions)
//			{
//				enumDescription.IsSelectedChanged += (sender, args) => this.OnFlagsChanged();
//			}
//			this.EnumValues = enumDescriptions.AsReadOnly();
//		}

//		#endregion

//		#region Methods

//		private static TEnum GetEnum(IEnumerable<EnumDescription> enumDescriptions)
//		{
//			var selection = enumDescriptions
//				.Where(description => description.IsSelected)
//				.Sum(description => description.Number)
//				;

//			return (TEnum) Enum.ToObject(typeof(TEnum), selection);
//		}

//		#endregion
//	}
//}