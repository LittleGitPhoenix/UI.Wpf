using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UI.Wpf.Base.Converters;

namespace Base.Test
{
	[TestClass]
	public class IsNullOrWhitespaceConverterTest
	{
		#region Convert

		[TestMethod]
		public void Check_Null_Is_True()
		{
			this.CheckConvert(null, true, new IsNullOrWhitespaceConverter());
		}

		[TestMethod]
		public void Check_Empty_Is_True()
		{
			this.CheckConvert("", true, new IsNullOrWhitespaceConverter());
		}
		
		[TestMethod]
		public void Check_Whitespace_Is_True()
		{
			this.CheckConvert("   ", true, new IsNullOrWhitespaceConverter());
		}
		
		[TestMethod]
		public void Check_Content_Is_False()
		{
			this.CheckConvert(Guid.NewGuid().ToString(), false, new IsNullOrWhitespaceConverter());
		}
		
		[TestMethod]
		public void Check_Object_Is_False()
		{
			this.CheckConvert(new object(), false, new IsNullOrWhitespaceConverter());
		}
		
		private void CheckConvert(string value, bool target, IsNullOrWhitespaceConverter converter)
			=> this.CheckConvert((object) value, target, converter);

		private void CheckConvert(object value, bool target, IsNullOrWhitespaceConverter converter)
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