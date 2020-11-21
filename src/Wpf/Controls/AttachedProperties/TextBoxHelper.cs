#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Windows;
using System.Windows.Controls;

namespace Phoenix.UI.Wpf.Controls.AttachedProperties
{
	/// <summary>
	/// Helper class for <see cref="TextBox"/>.
	/// </summary>
	public class TextBoxHelper
	{
		#region Receive keyboard focus after load

		public static bool GetReceiveKeyboardFocusAfterLoad(TextBox textBox)
		{
			if (textBox is null) throw new ArgumentNullException(nameof(textBox));
			return (bool)textBox.GetValue(ReceiveKeyboardFocusAfterLoadProperty);
		}

		public static void SetReceiveKeyboardFocusAfterLoad(TextBox textBox, bool value)
		{
			if (textBox is null) throw new ArgumentNullException(nameof(textBox));
			textBox.SetValue(ReceiveKeyboardFocusAfterLoadProperty, value);
		}
		
		/// <summary>
		/// Lets the <see cref="TextBox"/> receive keyboard focus as soon as it is loaded.
		/// </summary>
		public static readonly DependencyProperty ReceiveKeyboardFocusAfterLoadProperty = DependencyProperty.RegisterAttached
		(
			"ReceiveKeyboardFocusAfterLoad",
			typeof(bool),
			typeof(TextBoxHelper),
			new FrameworkPropertyMetadata(false, TextBoxHelper.AttachToLoadedEvent)
		);
		
		
		private static void AttachToLoadedEvent(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			if (!(dependencyObject is TextBox textBox)) return;

			var enabled = args.NewValue != null && (bool) args.NewValue;
			if (enabled)
			{
				if (textBox.IsLoaded)
				{
					// Directly set the focus.
					TextBoxHelper.SetFocus(textBox, new RoutedEventArgs());
				}
				else
				{
					// Apply a handler to the loaded event.
					TextBoxHelper.AddHandlers(textBox);
				}
			}
			else
			{
				TextBoxHelper.RemoveHandlers(textBox);
			}
		}

		private static void AddHandlers(FrameworkElement frameworkElement)
		{
			frameworkElement.Loaded += TextBoxHelper.SetFocus;
			frameworkElement.Unloaded += TextBoxHelper.RemoveHandlers;
		}
		private static void RemoveHandlers(object sender)
			=> TextBoxHelper.RemoveHandlers(sender, null);
		
		private static void RemoveHandlers(object sender, RoutedEventArgs _)
		{
			if (!(sender is FrameworkElement frameworkElement)) return;
			frameworkElement.Loaded -= TextBoxHelper.SetFocus;
			frameworkElement.Unloaded -= TextBoxHelper.RemoveHandlers;
		}
		
		private static void SetFocus(object sender, RoutedEventArgs args)
		{
			// Remove this handler.
			TextBoxHelper.RemoveHandlers(sender);

			// Cast the framework element to an input element.
			if (!(sender is IInputElement inputElement)) return;

			// Set the focus.
			Application.Current.Dispatcher.BeginInvoke
			(
				(Action) delegate
				{
					System.Windows.Input.Keyboard.Focus(inputElement);
				}, System.Windows.Threading.DispatcherPriority.Render
			);
		}
		
		#endregion
	}
}