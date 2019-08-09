using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UI.Wpf.Base.Converters;

namespace Base.Test
{
	[TestClass]
	public class IsNullConverterTest
	{
		#region Convert

		[TestMethod]
		public void Check_Null_Is_True()
		{
			this.CheckConvert(null, true, new IsNullConverter());
		}
		
		[TestMethod]
		public void Check_Object_Is_False()
		{
			this.CheckConvert(new object(), false, new IsNullConverter());
		}
		
		private void CheckConvert(object value, bool target, IsNullConverter converter)
		{
			var result = converter.Convert(value, null, null, CultureInfo.CurrentCulture);
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

		#region Convert Back

		[TestMethod]
		public void Check_ConvertBack_Throws()
		{
			Assert.ThrowsException<InvalidOperationException>(() => new HasElementsConverter().ConvertBack(null, null, null, CultureInfo.CurrentCulture));
		}

		#endregion
	}
}