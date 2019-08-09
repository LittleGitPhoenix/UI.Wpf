#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Phoenix.UI.Wpf.Base.Extensions
{
	/// <summary>
	/// Encapsulates methods for dealing with <see cref="Visual"/> elements.
	/// </summary>
	public static class ExtendedVisual
	{
		/// <summary>
		/// Gets the <see cref="AdornerLayer"/> for the <paramref name="visual"/>.
		/// </summary>
		/// <param name="visual"> The <see cref="Visual"/> whose <see cref="AdornerLayer"/> to get. </param>
		/// <returns> The <see cref="AdornerLayer"/> or <c>Null</c>. </returns>
		/// <remarks> If the <paramref name="visual"/> is a <see cref="Window"/> then the <see cref="AdornerLayer"/> of its content will be returned. </remarks>
		public static AdornerLayer GetAdornerLayer(this Visual visual)
			=> visual.GetAdornerLayer(out _);

		/// <summary>
		/// Gets the <see cref="AdornerLayer"/> for the <paramref name="visual"/>.
		/// </summary>
		/// <param name="visual"> The <see cref="Visual"/> whose <see cref="AdornerLayer"/> to get. </param>
		/// <param name="adornedVisual"> If <paramref name="visual"/> was changed in order to get a proper <see cref="AdornerLayer"/>, then this will contain the new <see cref="Visual"/>, otherwise this will be the <paramref name="visual"/> itself. </param>
		/// <returns> The <see cref="AdornerLayer"/> or <c>Null</c>. </returns>
		/// <remarks> If the <paramref name="visual"/> is a <see cref="Window"/> then the <see cref="AdornerLayer"/> of its content will be returned. </remarks>
		public static AdornerLayer GetAdornerLayer(this Visual visual, out Visual adornedVisual)
		{
			adornedVisual = visual;

			if (visual is AdornerDecorator decorator) return decorator.AdornerLayer;
			if (visual is ScrollContentPresenter presenter) return presenter.AdornerLayer;

			// Check if the visual is a window.
			if (visual is Window window && window.Content is Visual content)
			{
				adornedVisual = content;
				return AdornerLayer.GetAdornerLayer(content);
			}

			// Just try the normal method for retrieving an adorner layer.
			return AdornerLayer.GetAdornerLayer(visual);
		}
	}
}