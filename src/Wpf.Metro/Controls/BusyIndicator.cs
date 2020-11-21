#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Windows;
using System.Windows.Controls;

namespace Phoenix.UI.Wpf.Metro.Controls
{
	/// <summary>
	/// A control to provide a visual indicator when an application is busy.
	/// </summary>
	/// <remarks>
	/// http://10rem.net/blog/2010/02/05/creating-customized-usercontrols-deriving-from-contentcontrol-in-wpf-4
	/// https://github.com/dotnetprojects/WpfExtendedToolkit/tree/Extended/Src/Xceed.Wpf.Toolkit/BusyIndicator
	/// https://wpftutorial.net/HowToCreateACustomControl.html
	/// </remarks>
	public class BusyIndicator : ContentControl
	{
		#region Delegates / Events
		#endregion

		#region Constants
		#endregion

		#region Fields
		#endregion

		#region (Dependency)Properties

		///// <inheritdoc cref="MessageFontSizeProperty"/>
		//public double MessageFontSize
		//{
		//	get => (double)GetValue(MessageFontSizeProperty);
		//	set => SetValue(MessageFontSizeProperty, value);
		//}
		///// <summary> The font size of the busy message. </summary>
		//public static readonly DependencyProperty MessageFontSizeProperty = DependencyProperty.Register(nameof(MessageFontSize), typeof(double), typeof(BusyIndicator), new PropertyMetadata(default(double)));

		///// <inheritdoc cref="OverlayBackgroundProperty"/>
		//public System.Windows.Media.Brush OverlayBackground
		//{
		//	get => (System.Windows.Media.Brush)GetValue(OverlayBackgroundProperty);
		//	set => SetValue(OverlayBackgroundProperty, value);
		//}
		///// <summary> The background brush of the whole busy overlay. </summary>
		//public static readonly DependencyProperty OverlayBackgroundProperty = DependencyProperty.Register(nameof(OverlayBackground), typeof(System.Windows.Media.Brush), typeof(BusyIndicator), new PropertyMetadata(default(System.Windows.Media.Brush)));

		///// <inheritdoc cref="IndicatorForegroundProperty"/>
		//public System.Windows.Media.Brush IndicatorForeground
		//{
		//	get => (System.Windows.Media.Brush)GetValue(IndicatorForegroundProperty);
		//	set => SetValue(IndicatorForegroundProperty, value);
		//}
		///// <summary> The foreground brush of the busy indicator. Will be applied to the waiting animation and the busy message. </summary>
		//public static readonly DependencyProperty IndicatorForegroundProperty = DependencyProperty.Register(nameof(IndicatorForeground), typeof(System.Windows.Media.Brush), typeof(BusyIndicator), new PropertyMetadata(default(System.Windows.Media.Brush)));

		/// <inheritdoc cref="OverlayOpacityProperty"/>
		public double OverlayOpacity
		{
			get => (double)GetValue(OverlayOpacityProperty);
			set => SetValue(OverlayOpacityProperty, value);
		}
		/// <summary> The opacity of the background of the whole busy overlay. Default is 0.6 (60%). </summary>
		public static readonly DependencyProperty OverlayOpacityProperty = DependencyProperty.Register(nameof(OverlayOpacity), typeof(double), typeof(BusyIndicator), new PropertyMetadata(0.60D));

		/// <inheritdoc cref="IsBusyProperty"/>
		public bool IsBusy
		{
			get => (bool)GetValue(IsBusyProperty);
			set => SetValue(IsBusyProperty, value);
		}
		/// <summary> Flag if the busy indicator should be shown. </summary>
		public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(BusyIndicator), new PropertyMetadata(default(bool)));

		/// <inheritdoc cref="MessageProperty"/>
		public string Message
		{
			get => (string)GetValue(MessageProperty);
			set => SetValue(MessageProperty, value);
		}
		/// <summary> An optional message that is shown if the indicator is visible. </summary>
		public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof(Message), typeof(string), typeof(BusyIndicator), new PropertyMetadata(default(string)));

		#endregion

		#region Enumerations
		#endregion

		#region (De)Constructors

		static BusyIndicator()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public BusyIndicator() { }

		#endregion

		#region Methods
		#endregion
	}
}