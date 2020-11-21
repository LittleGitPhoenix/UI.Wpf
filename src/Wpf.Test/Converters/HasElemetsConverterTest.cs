using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using Phoenix.UI.Wpf.Converters;

namespace Base.Test
{
	[TestFixture]
	public class HasElementsConverterTest
	{
		#region Convert

		[Test]
		public void Check_Null_Is_False()
		{
			this.CheckConvert(null, false, new HasElementsConverter());
		}
		
		[Test]
		public void Check_Empty_Collection_Is_False()
		{
			this.CheckConvert(new List<int>(), false, new HasElementsConverter());
		}
		
		[Test]
		public void Check_Filled_Collection_Is_True()
		{
			this.CheckConvert(new List<int>() {int.MaxValue}, true, new HasElementsConverter());
		}
		
		private void CheckConvert<T>(ICollection<T> collection, bool target, HasElementsConverter converter)
			=> this.CheckConvert((object) collection, target, converter);

		private void CheckConvert(object collection, bool target, HasElementsConverter converter)
		{
			var result = converter.Convert(collection, null, null, CultureInfo.CurrentCulture);
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