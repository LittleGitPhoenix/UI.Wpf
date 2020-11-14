using System;
using System.Globalization;
using NUnit.Framework;
using Phoenix.UI.Wpf.Base.Converters;

namespace Base.Test
{
	[TestFixture]
	public class IsNullConverterTest
	{
		#region Convert

		[Test]
		public void Check_Null_Is_True()
		{
			this.CheckConvert(null, true, new IsNullConverter());
		}
		
		[Test]
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

		[Test]
		public void Check_ConvertBack_Throws()
		{
			Assert.Throws<InvalidOperationException>(() => new HasElementsConverter().ConvertBack(null, null, null, CultureInfo.CurrentCulture));
		}

		#endregion
	}
}