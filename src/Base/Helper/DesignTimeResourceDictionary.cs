#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Windows;

namespace Phoenix.UI.Wpf.Base.Helper
{
	/// <summary>
	/// Special <see cref="ResourceDictionary"/> that is only applied during design time.
	/// </summary>
	/// <remarks> Can be used to specify resources for example in a user control so that the designer shows a better preview. Normally resources that change styles are specified at application level and therefor won't be reflected during design time. </remarks>
	/// <example>
	/// &lt;UserControl.Resources&gt;
	/// 	&lt;ResourceDictionary&gt;
	/// 		&lt;ResourceDictionary.MergedDictionaries&gt;
	/// 			&lt;phoenix:DesignTimeResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml" /&gt;
	/// 			&lt;phoenix:DesignTimeResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml" /&gt;
	/// 			&lt;phoenix:DesignTimeResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml" /&gt;
	/// 		&lt;/ResourceDictionary.MergedDictionaries&gt;
	/// 	&lt;/ResourceDictionary&gt;
	/// &lt;/UserControl.Resources&gt;
	/// </example>
	public class DesignTimeResourceDictionary : ResourceDictionary
	{
		/// <summary> Flag signaling if the application is currently in design mode. </summary>
		private static readonly bool IsInDesignMode = (bool)System.ComponentModel.DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;

		/// <summary> Special <see cref="Source"/> property whose value is only applied set in design mode. </summary>
		public new Uri Source
		{
			get => base.Source;
			set
			{
				// Only set the real source if in design mode.
				if (DesignTimeResourceDictionary.IsInDesignMode) base.Source = value;
			}
		}
	}
}