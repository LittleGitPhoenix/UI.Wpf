#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Windows;

namespace Phoenix.UI.Wpf.Helper
{
	/// <summary>
	/// Helper class that provides property binding for ui elements that are not part of the logical tree (e.g. data grid columns).
	/// </summary>
	/// <remarks> https://thomaslevesque.com/2011/03/21/wpf-how-to-bind-to-data-when-the-datacontext-is-not-inherited/ </remarks>
	/// <example>
	/// &lt;DataGrid.Resources&gt;
	///     &lt;local:BindingProxy x:Key="proxy" Data="{Binding}" /&gt;
	/// &lt;/DataGrid.Resources&gt;
	/// 
	/// &lt;DataGridTextColumn
	/// 	Header="Price"
	/// 	Binding="{Binding Price}"
	/// 	Visibility="{Binding Source={StaticResource proxy}, Path=Data.ShowPrice, Converter={StaticResource visibilityConverter}}"
	/// 	/&gt;
	/// </example>
	public class BindingProxy : Freezable
	{
		#region Overrides of Freezable

		protected override Freezable CreateInstanceCore()
		{
			return new BindingProxy();
		}

		#endregion

		public object Data
		{
			get => base.GetValue(DataProperty);
			set => base.SetValue(DataProperty, value);
		}

		// Using a DependencyProperty as the backing store for Data. This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
	}
}