using System;
using System.Globalization;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UI.Wpf.Base.Converters;

namespace Base.Test
{
	[TestClass]
	public class InverseVisibilityConverterTest
	{
		#region Convert

		[TestMethod]
		public void Check_TrueVisibility_Will_Be_FalseVisibility()
		{
			var trueVisibility = Visibility.Visible;
			var falseVisibility = Visibility.Collapsed;
			this.CheckConvert(trueVisibility, falseVisibility, new InverseVisibilityConverter(){TrueVisibility = trueVisibility, FalseVisibility = falseVisibility });
		}

		[TestMethod]
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

		#endregion

		#region Convert Back

		[TestMethod]
		public void Check_ConvertBack_Throws()
		{
			Assert.ThrowsException<InvalidOperationException>(() => new HasElementsConverter().ConvertBack(null, null, null, CultureInfo.CurrentCulture));
		}

		#endregion
	}
}