using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using Phoenix.UI.Wpf.Converters;

namespace Base.Test
{
	[TestFixture]
	public class InverseVisibilityConverterTest
	{
		#region Convert

		[Test]
		public void Check_TrueVisibility_Will_Be_FalseVisibility()
		{
			var trueVisibility = Visibility.Visible;
			var falseVisibility = Visibility.Collapsed;
			this.CheckConvert(trueVisibility, falseVisibility, new InverseVisibilityConverter(){TrueVisibility = trueVisibility, FalseVisibility = falseVisibility });
		}

		[Test]
		public void Check_FalseVisibility_Will_Be_TrueVisibility()
		{
			var trueVisibility = Visibility.Visible;
			var falseVisibility = Visibility.Collapsed;
			this.CheckConvert(falseVisibility, trueVisibility, new InverseVisibilityConverter(){TrueVisibility = trueVisibility, FalseVisibility = falseVisibility });
		}

		private void CheckConvert(Visibility value, Visibility target, InverseVisibilityConverter converter)
		{
			var result = converter.Convert(value, null, null, CultureInfo.CurrentCulture);
			if (result is Visibility visibility)
			{
				Assert.AreEqual(target, visibility);
			}
			else
			{
				Assert.Fail($"The result should be a boolean but actually is '{result?.GetType().Name ?? "[UNKNOWN]"}'.");
			}
		}

		[Test]
		[Apartment(ApartmentState.STA)]
		[TestCase(Visibility.Visible, Visibility.Hidden)]
		[TestCase(Visibility.Visible, Visibility.Collapsed)]
		[TestCase(Visibility.Hidden, Visibility.Visible)]
		[TestCase(Visibility.Hidden, Visibility.Collapsed)]
		[TestCase(Visibility.Collapsed, Visibility.Visible)]
		[TestCase(Visibility.Collapsed, Visibility.Hidden)]
		public void CheckConvertFromUIElement(Visibility elementVisibility, Visibility targetVisibility)
		{
			// Arrange
			var element = new TextBlock {Visibility = elementVisibility};
			var converter = new InverseVisibilityConverter() {TrueVisibility = elementVisibility, FalseVisibility = targetVisibility};

			// Act
			var actualVisibility = (Visibility) converter.Convert(element, null, null, CultureInfo.CurrentCulture);
			
			// Assert
			Assert.That(targetVisibility, Is.EqualTo(actualVisibility));
		}

		#endregion

		#region Convert Back

		[Test]
		public void Check_ConvertBack_Throws()
		{
			Assert.Throws<InvalidOperationException>(() => new HasElementsConverter().ConvertBack(null, null, null, CultureInfo.CurrentCulture));
		}

		#endregion
	}
}