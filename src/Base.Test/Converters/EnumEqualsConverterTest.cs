using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UI.Wpf.Base.Converters;

namespace Base.Test
{
	[TestClass]
	public class EnumEqualsConverterTest
	{
		enum Number
		{
			[System.ComponentModel.Description("#1")]
			One = 1,
			[System.ComponentModel.Description("#2")]
			Two = 2,
			[System.ComponentModel.Description("#3")]
			Three = 3,
		}

		[Flags]
		enum Colors
		{
			Red = 1 << 0,
			Green = 1 << 1,
			Blue = 1 << 2,
		}

		#region Convert

		[TestMethod]
		public void Check_Normal_Enum_Matches()
		{
			this.CheckConvert(Number.One, Number.One, true, new EnumEqualsConverter());
		}

		[TestMethod]
		public void Check_Normal_Enum_Mismatches()
		{
			this.CheckConvert(Number.Three, Number.One, false, new EnumEqualsConverter());
		}

		[TestMethod]
		public void Check_Flags_Enum_Matches()
		{
			this.CheckConvert(Colors.Red | Colors.Blue, Colors.Red, true, new EnumEqualsConverter());
		}

		[TestMethod]
		public void Check_Flags_Enum_Mismatches()
		{
			this.CheckConvert(Colors.Red | Colors.Blue, Colors.Green, false, new EnumEqualsConverter());
		}

		[TestMethod]
		public void Check_No_Enum_Value_Throws()
		{
			Assert.ThrowsException<NotSupportedException>(() => new EnumEqualsConverter().Convert(new object(), typeof(Number), Number.One, CultureInfo.CurrentCulture));
		}

		[TestMethod]
		public void Check_No_Enum_Parameter_Throws()
		{
			Assert.ThrowsException<NotSupportedException>(() => new EnumEqualsConverter().Convert(Number.One, typeof(Number), null, CultureInfo.CurrentCulture));
		}

		private void CheckConvert<TEnum>(TEnum realValue, TEnum expectedValue, bool target, EnumEqualsConverter converter) where TEnum : Enum
			=> this.CheckConvert((object) realValue, expectedValue, target, converter);

		private void CheckConvert(object realValue, object expectedValue, bool target, EnumEqualsConverter converter)
		{
			var result = converter.Convert(realValue, realValue.GetType(), expectedValue, CultureInfo.CurrentCulture);
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
			Assert.ThrowsException<InvalidOperationException>(() => new EnumEqualsConverter().ConvertBack(null, null, null, CultureInfo.CurrentCulture));
		}

		#endregion
	}
}