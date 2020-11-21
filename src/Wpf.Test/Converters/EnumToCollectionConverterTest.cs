using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using Phoenix.UI.Wpf.Converters;
using Phoenix.UI.Wpf.Extensions;

namespace Base.Test
{
	[TestFixture]
	public class EnumToCollectionConverterTest
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
			Blue = 1 <<2,
		}

		#region Convert

		[Test]
		public void Check_Null_Throws()
		{
			Assert.Throws<ArgumentNullException>(() => new EnumToCollectionConverter().Convert(null, null, null, CultureInfo.CurrentCulture));
		}

		[Test]
		public void Check_No_Enum_Throws()
		{
			Assert.Throws<ArgumentException>(() => new EnumToCollectionConverter().Convert(new object(), null, null, CultureInfo.CurrentCulture));
		}

		[Test]
		public void Check_Normal_Enum_Convert_Succeeds()
		{
			var converter = new EnumToCollectionConverter();
			var result = converter.Convert(Number.One, null, null, CultureInfo.CurrentCulture);
			if (!(result is IList<EnumDescription> actual))
			{
				Assert.Fail($"The result should be {nameof(IList<EnumDescription>)} but actually is '{result?.GetType().Name ?? "[UNKNOWN]"}'.");
			}
			else
			{
				Assert.AreEqual(Number.One, (Number) actual[0].Value);
				Assert.AreEqual("#1", actual[0].Description);
				Assert.AreEqual(1, actual[0].Number);
				Assert.IsTrue(actual[0].IsSelected);

				Assert.AreEqual(Number.Two, (Number) actual[1].Value);
				Assert.AreEqual("#2", actual[1].Description);
				Assert.AreEqual(2, actual[1].Number);
				Assert.IsFalse(actual[1].IsSelected);

				Assert.AreEqual(Number.Three, (Number) actual[2].Value);
				Assert.AreEqual("#3", actual[2].Description);
				Assert.AreEqual(3, actual[2].Number);
				Assert.IsFalse(actual[2].IsSelected);
			}
		}

		[Test]
		public void Check_Flags_Enum_Convert_Succeeds()
		{
			var converter = new EnumToCollectionConverter();
			var result = converter.Convert(Colors.Red | Colors.Blue, null, null, CultureInfo.CurrentCulture);
			if (!(result is IList<EnumDescription> actual))
			{
				Assert.Fail($"The result should be {nameof(IList<EnumDescription>)} but actually is '{result?.GetType().Name ?? "[UNKNOWN]"}'.");
			}
			else
			{
				Assert.AreEqual(Colors.Red, (Colors) actual[0].Value);
				Assert.AreEqual(Colors.Red.ToString(), actual[0].Description);
				Assert.AreEqual((int) Colors.Red, actual[0].Number);
				Assert.IsTrue(actual[0].IsSelected);

				Assert.AreEqual(Colors.Green, (Colors) actual[1].Value);
				Assert.AreEqual(Colors.Green.ToString(), actual[1].Description);
				Assert.AreEqual((int) Colors.Green, actual[1].Number);
				Assert.IsFalse(actual[1].IsSelected);

				Assert.AreEqual(Colors.Blue, (Colors) actual[2].Value);
				Assert.AreEqual(Colors.Blue.ToString(), actual[2].Description);
				Assert.AreEqual((int) Colors.Blue, actual[2].Number);
				Assert.IsTrue(actual[2].IsSelected);
			}
		}
		
		#endregion

		#region Convert Back

		[Test]
		public void Check_ConvertBack_Succeeds()
		{
			var target = Colors.Red | Colors.Blue;
			var enumDescriptions = target.GetEnumDescriptions().ToList();
			var converter = new EnumToCollectionConverter();
			var result = converter.ConvertBack(enumDescriptions, typeof(Colors), null, CultureInfo.CurrentCulture);
			if (!(result is Colors actual))
			{
				Assert.Fail($"The result should be {nameof(Colors)} but actually is '{result?.GetType().Name ?? "[UNKNOWN]"}'.");
			}
			else
			{
				Assert.AreEqual(target, actual);
			}
		}

		[Test]
		public void Check_ConvertBack_Returns_Default()
		{
			var converter = new EnumToCollectionConverter();
			var result = converter.ConvertBack(null, typeof(Colors), null, CultureInfo.CurrentCulture);
			if (!(result is Colors actual))
			{
				Assert.Fail($"The result should be {nameof(Colors)} but actually is '{result?.GetType().Name ?? "[UNKNOWN]"}'.");
			}
			else
			{
				Assert.AreEqual(0, (int) actual);
			}
		}

		[Test]
		public void Check_ConvertBack_Throws_For_Wrong_Type()
		{
			Assert.Throws<ArgumentException>(() => new EnumToCollectionConverter().ConvertBack(null, typeof(object), null, CultureInfo.CurrentCulture));
		}

		#endregion
	}
}