#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Phoenix.UI.Wpf.Controls.AttachedProperties
{
	/// <summary>
	/// Helper methods for <see cref="ScrollViewer"/>.
	/// </summary>
	public class ScrollViewerHelper
	{
		#region Handle mouse wheel

		// https://serialseb.com/blog/2007/09/03/wpf-tips-6-preventing-scrollviewer-from/

		private static readonly List<MouseWheelEventArgs> ReEntrantList = new List<MouseWheelEventArgs>();

		public static bool GetHandleMouseWheelFromContent(ScrollViewer scrollViewer)
		{
			if (scrollViewer is null) throw new ArgumentNullException(nameof(scrollViewer));
			return (bool)scrollViewer.GetValue(HandleMouseWheelFromContentProperty);
		}

		public static void SetHandleMouseWheelFromContent(ScrollViewer scrollViewer, bool value)
		{
			if (scrollViewer is null) throw new ArgumentNullException(nameof(scrollViewer));
			scrollViewer.SetValue(HandleMouseWheelFromContentProperty, value);
		}

		/// <summary>
		/// Lets the <see cref="ScrollViewer"/> handle mouse wheel events from the content so that scrolling is possible.
		/// </summary>
		/// <remarks> This is useful if a child (e.g. <see cref="DataGrid"/>) blocks mouse events from reaching the <see cref="ScrollViewer"/>. </remarks>
		public static readonly DependencyProperty HandleMouseWheelFromContentProperty = DependencyProperty.RegisterAttached
		(
			"HandleMouseWheelFromContent",
			typeof(bool),
			typeof(ScrollViewerHelper),
			new FrameworkPropertyMetadata(false, ScrollViewerHelper.OnHandleMouseWheelFromContentPropertyChanged)
		);

		public static void OnHandleMouseWheelFromContentPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
		{
			if (!(sender is ScrollViewer scrollViewer)) throw new ArgumentException(@"The dependency property can only be attached to a ScrollViewer", nameof(sender));

			var enabled = args.NewValue != null && (bool)args.NewValue;
			if (enabled)
			{
				scrollViewer.PreviewMouseWheel += HandlePreviewMouseWheel;
			}
			else
			{
				scrollViewer.PreviewMouseWheel -= HandlePreviewMouseWheel;
			}
		}

		private static void HandlePreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (!(sender is ScrollViewer scrollViewer)) return;

			if (!e.Handled && !ReEntrantList.Contains(e))
			{
				var previewEventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
				{
					RoutedEvent = UIElement.PreviewMouseWheelEvent,
					Source = sender
				};

				var originalSource = e.OriginalSource as UIElement;

				ReEntrantList.Add(previewEventArg);
				originalSource?.RaiseEvent(previewEventArg);
				ReEntrantList.Remove(previewEventArg);

				//// If no child has handled the event by now, we do our job.
				//if (!previewEventArg.Handled && ((e.Delta > 0 && scrollControl.VerticalOffset == 0) || (e.Delta <= 0 && scrollControl.VerticalOffset >= scrollControl.ExtentHeight - scrollControl.ViewportHeight))) 
				//{
				//	e.Handled = true;
				//	var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
				//	{
				//		RoutedEvent = UIElement.MouseWheelEvent,
				//		Source = sender
				//	};

				//	var parent = (UIElement)((FrameworkElement)sender).Parent;

				//	parent.RaiseEvent(eventArg);
				//}

				// If no child has handled the event by now, we do our job.
				if (!previewEventArg.Handled)
				{
					scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
					e.Handled = true;
				}
			}
		}

		#endregion

		#region Auto scroll to end

		public static bool GetAlwaysScrollToEnd(ScrollViewer scrollViewer)
		{
			if (scrollViewer is null) throw new ArgumentNullException(nameof(scrollViewer));
			return (bool)scrollViewer.GetValue(AlwaysScrollToEndProperty);
		}

		public static void SetAlwaysScrollToEnd(ScrollViewer scrollViewer, bool alwaysScrollToEnd)
		{
			if (scrollViewer is null) throw new ArgumentNullException(nameof(scrollViewer));
			scrollViewer.SetValue(AlwaysScrollToEndProperty, alwaysScrollToEnd);
		}

		/// <summary>
		/// Automatically scrolls to the end of the <see cref="ScrollViewer"/> as its size changes.
		/// </summary>
		/// <remarks> The auto scroll stops, if the user manually scrolls upwards and continues if the scroll viewer is manually scrolled back to the bottom. </remarks>
		public static readonly DependencyProperty AlwaysScrollToEndProperty = DependencyProperty.RegisterAttached
		(
			"AlwaysScrollToEnd",
			typeof(bool),
			typeof(ScrollViewerHelper),
			new PropertyMetadata(false, ScrollViewerHelper.AlwaysScrollToEndChanged)
		);

		private static void AlwaysScrollToEndChanged(object sender, DependencyPropertyChangedEventArgs args)
		{
			if (!(sender is ScrollViewer scrollViewer)) throw new ArgumentException(@"The dependency property can only be attached to a ScrollViewer", nameof(sender));

			var enabled = args.NewValue != null && (bool)args.NewValue;
			if (enabled)
			{
				scrollViewer.ScrollToEnd();
				scrollViewer.ScrollChanged += ScrollChanged;
			}
			else
			{
				scrollViewer.ScrollChanged -= ScrollChanged;
			}
		}
		
		private static void ScrollChanged(object sender, ScrollChangedEventArgs args)
		{
			if (!(sender is ScrollViewer scrollViewer)) return;

			// Check if the user scrolled.
			var userScrolled = args.ExtentHeightChange == 0;

			// If the user scrolled and the scroll view is not at the bottom, then stop auto scrolling.
			var autoScrollEnabled = userScrolled && scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight;
			if (autoScrollEnabled)
			{
				scrollViewer.ScrollToVerticalOffset(scrollViewer.ExtentHeight);
			}
		}

		#endregion
	}
}