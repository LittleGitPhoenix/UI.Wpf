using System;
using System.Globalization;
using System.Windows;
using NUnit.Framework;
using Phoenix.UI.Wpf.Base.Converters;

namespace Base.Test
{
	[TestFixture]
	public class BoolToVisibilityConverterTest
	{
		#region Convert

		[Test]
		public void Check_True_Is_Visible_By_Default()
		{
			this.CheckConvert(true, Visibility.Visible, new BoolToVisibilityConverter());
		}

		[Test]
		public void Check_True_Is_Hidden_When_Overridden()
		{
			var targetVisibility = Visibility.Hidden;
			this.CheckConvert(true, targetVisibility, new BoolToVisibilityConverter() { TrueVisibility = targetVisibility });
		}
		
		[Test]
		public void Check_True_Is_Collapsed_When_Overridden()
		{
			var targetVisibility = Visibility.Collapsed;
			this.CheckConvert(true, targetVisibility, new BoolToVisibilityConverter() { TrueVisibility = targetVisibility});
		}

		[Test]
		public void Check_False_Is_Collapsed_By_Default()
		{
			this.CheckConvert(false, Visibility.Collapsed, new BoolToVisibilityConverter());
		}

		[Test]
		public void Check_Null_Is_Collapsed_By_Default()
		{
			this.CheckConvert(null, Visibility.Collapsed, new BoolToVisibilityConverter());
		}
		
		private void CheckConvert(bool value, Visibility targetVisibility, BoolToVisibilityConverter converter)
			=> this.CheckConvert((object) value, targetVisibility, converter);

		private void CheckConvert(object value, Visibility targetVisibility, BoolToVisibilityConverter converter)
		{
			var result = converter.Convert(value, null, null, CultureInfo.CurrentCulture);
			if (result is Visibility visibility)
			{
				Assert.AreEqual(targetVisibility, visibility);
			}
			else
			{
				Assert.Fail($"The result should be of type '{nameof(Visibility)}' but actually is '{result?.GetType().Name ?? "[UNKNOWN]"}'.");
			}
		}

		#endregion

		#region Convert Back

		[Test]
		public void Check_Visible_Is_True_By_Default()
		{
			this.CheckConvertBack(Visibility.Visible, true, new BoolToVisibilityConverter());
		}
		
		[Test]
		public void Check_Hidden_Is_False_By_Default()
		{
			this.CheckConvertBack(Visibility.Hidden, false, new BoolToVisibilityConverter());
		}
		
		[Test]
		public void Check_Collapsed_Is_False_By_Default()
		{
			this.CheckConvertBack(Visibility.Collapsed, false, new BoolToVisibilityConverter());
		}
		
		[Test]
		public void Check_Null_Is_False_By_Default()
		{
			this.CheckConvertBack(null, false, new BoolToVisibilityConverter());
		}

		private void CheckConvertBack(Visibility visibility, bool target, BoolToVisibilityConverter converter)
			=> this.CheckConvertBack((object) visibility, target, converter);

		private void CheckConvertBack(object visibility, bool target, BoolToVisibilityConverter converter)
		{
			var result = converter.ConvertBack(visibility, null, null, CultureInfo.CurrentCulture);
			if (result is bool boolean)
			{
				Assert.AreEqual(target, boolean);
			}
			else
			{
				Assert.Fail($"The result should be a boolean but actually is '{result?.GetType().Name ?? "[UNKNOWN]"}'.");
			}
		}

		#endregion
	}
}