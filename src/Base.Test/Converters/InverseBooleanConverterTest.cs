using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UI.Wpf.Base.Converters;

namespace Base.Test
{
	[TestClass]
	public class InverseBooleanConverterTest
	{
		#region Convert

		[TestMethod]
		public void Check_True_Is_False()
		{
			this.CheckConvert(true, false, new InverseBooleanConverter());
			this.CheckConvertBack(true, false, new InverseBooleanConverter());
		}
		
		[TestMethod]
		public void Check_False_Is_True()
		{
			this.CheckConvert(false, true, new InverseBooleanConverter());
			this.CheckConvertBack(false, true, new InverseBooleanConverter());
		}
		
		[TestMethod]
		public void Check_Null_Is_False()
		{
			this.CheckConvert(null, true, new InverseBooleanConverter());
		}

		[TestMethod]
		public void Check_Object_Is_False()
		{
			this.CheckConvert(new object(), true, new InverseBooleanConverter());
		}

		private void CheckConvert(bool value, bool target, InverseBooleanConverter converter)
			=> this.CheckConvert((object) value, target, converter);

		private void CheckConvert(object value, bool target, InverseBooleanConverter converter)
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
		
		private void CheckConvertBack(bool value, bool target, InverseBooleanConverter converter)
			=> this.CheckConvertBack((object)value, target, converter);

		private void CheckConvertBack(object value, bool target, InverseBooleanConverter converter)
		{
			var result = converter.ConvertBack(value, null, null, CultureInfo.CurrentCulture);
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