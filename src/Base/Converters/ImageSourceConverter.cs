#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UI.Wpf.Base.Converters
{
	/// <summary>
	/// Converts a <see cref="System.IO.Stream"/> into a <see cref="System.Windows.Media.Imaging.BitmapImage"/> that can be used as source of an image control.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(System.IO.Stream), targetType: typeof(System.Windows.Media.Imaging.BitmapImage))]
	public class StreamToImageSourceConverter : SourceValueConverter<StreamToImageSourceConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(System.Windows.Media.ImageSource)) throw new InvalidOperationException($"The target must be {nameof(System.Windows.Media.ImageSource)} or derived types");

			if (value is System.IO.Stream stream)
			{
				var image = new System.Windows.Media.Imaging.BitmapImage();
				image.BeginInit();
				image.StreamSource = stream;
				image.EndInit();
				return image;
			}

			return null;
			//return new System.Windows.Media.Imaging.BitmapImage();
		}
	}

	/// <summary>
	/// Converts a <see cref="Byte"/> collection into a <see cref="System.Windows.Media.Imaging.BitmapImage"/> that can be used as source of an image control.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(ICollection<Byte>), targetType: typeof(System.Windows.Media.Imaging.BitmapImage))]
	public class BytesToImageSourceConverter : SourceValueConverter<BytesToImageSourceConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(System.Windows.Media.ImageSource)) throw new InvalidOperationException($"The target must be {nameof(System.Windows.Media.ImageSource)} or derived types");

			if (value is ICollection<Byte> bytes && bytes.Any())
			{
				var stream = new System.IO.MemoryStream(bytes.ToArray());
				return StreamToImageSourceConverter.Instance.Convert(stream, targetType, parameter, culture);
			}

			return null;
			//return new System.Windows.Media.Imaging.BitmapImage();
		}
	}
}