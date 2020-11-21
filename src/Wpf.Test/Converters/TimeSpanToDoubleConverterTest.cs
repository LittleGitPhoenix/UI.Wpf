using System;
using System.Globalization;
using NUnit.Framework;
using Phoenix.UI.Wpf.Converters;

namespace Base.Test
{
	[TestFixture]
	public class TimeSpanToDoubleConverterTest
	{
		#region Convert
		
		[Test]
		public void Check_Ticks()
		{
			var target = 5000;
			var timeSpan = TimeSpan.FromTicks(target);
			this.CheckConvert(timeSpan, target, new TimeSpanToDoubleConverter(){UnitOfTime = UnitOfTime.Ticks});
		}
		
		[Test]
		public void Check_Milliseconds()
		{
			var target = 5000;
			var timeSpan = TimeSpan.FromMilliseconds(target);
			this.CheckConvert(timeSpan, target, new TimeSpanToDoubleConverter(){UnitOfTime = UnitOfTime.Milliseconds});
		}
		
		[Test]
		public void Check_Seconds()
		{
			var target = 5000;
			var timeSpan = TimeSpan.FromSeconds(target);
			this.CheckConvert(timeSpan, target, new TimeSpanToDoubleConverter(){UnitOfTime = UnitOfTime.Seconds});
		}
		
		[Test]
		public void Check_Minutes()
		{
			var target = 5000;
			var timeSpan = TimeSpan.FromMinutes(target);
			this.CheckConvert(timeSpan, target, new TimeSpanToDoubleConverter(){UnitOfTime = UnitOfTime.Minutes });
		}
		
		[Test]
		public void Check_Hours()
		{
			var target = 5000;
			var timeSpan = TimeSpan.FromHours(target);
			this.CheckConvert(timeSpan, target, new TimeSpanToDoubleConverter(){UnitOfTime = UnitOfTime.Hours });
		}

		[Test]
		public void Check_Days()
		{
			var target = 5000;
			var timeSpan = TimeSpan.FromDays(target);
			this.CheckConvert(timeSpan, target, new TimeSpanToDoubleConverter(){UnitOfTime = UnitOfTime.Days });
		}

		[Test]
		public void Check_Object_Is_Zero()
		{
			this.CheckConvert(new object(), 0, new TimeSpanToDoubleConverter());
		}
		
		private void CheckConvert(string value, double target, TimeSpanToDoubleConverter converter)
			=> this.CheckConvert((object) value, target, converter);

		private void CheckConvert(object value, double target, TimeSpanToDoubleConverter converter)
		{
			var result = converter.Convert(value, null, null, CultureInfo.CurrentCulture);
			if (result is double actual)
			{
				Assert.AreEqual(target, actual);
			}
			else
			{
				Assert.Fail($"The result should be a double but actually is '{result?.GetType().Name ?? "[UNKNOWN]"}'.");
			}
		}

		#endregion

		#region Convert Back

		[Test]
		public void Check_To_TimeSpan_From_Ticks_Succeeds()
		{
			var value = 5000;
			var target = TimeSpan.FromTicks(value);
			this.CheckConvertBack(value, target, new TimeSpanToDoubleConverter() {UnitOfTime = UnitOfTime.Ticks});
		}

		[Test]
		public void Check_To_TimeSpan_From_Milliseconds_Succeeds()
		{
			var value = 5000;
			var target = TimeSpan.FromMilliseconds(value);
			this.CheckConvertBack(value, target, new TimeSpanToDoubleConverter() {UnitOfTime = UnitOfTime.Milliseconds });
		}

		[Test]
		public void Check_To_TimeSpan_From_Seconds_Succeeds()
		{
			var value = 5000;
			var target = TimeSpan.FromSeconds(value);
			this.CheckConvertBack(value, target, new TimeSpanToDoubleConverter() {UnitOfTime = UnitOfTime.Seconds });
		}

		[Test]
		public void Check_To_TimeSpan_From_Minutes_Succeeds()
		{
			var value = 5000;
			var target = TimeSpan.FromMinutes(value);
			this.CheckConvertBack(value, target, new TimeSpanToDoubleConverter() {UnitOfTime = UnitOfTime.Minutes });
		}

		[Test]
		public void Check_To_TimeSpan_From_Hours_Succeeds()
		{
			var value = 5000;
			var target = TimeSpan.FromHours(value);
			this.CheckConvertBack(value, target, new TimeSpanToDoubleConverter() {UnitOfTime = UnitOfTime.Hours });
		}

		[Test]
		public void Check_To_TimeSpan_From_Days_Succeeds()
		{
			var value = 5000;
			var target = TimeSpan.FromDays(value);
			this.CheckConvertBack(value, target, new TimeSpanToDoubleConverter() {UnitOfTime = UnitOfTime.Days });
		}

		[Test]
		public void Check_Object_Is_TimeSpan_Zero()
		{
			this.CheckConvertBack(new object(), TimeSpan.Zero, new TimeSpanToDoubleConverter());
		}

		private void CheckConvertBack(object value, TimeSpan target, TimeSpanToDoubleConverter converter)
		{
			var result = converter.ConvertBack(value, null, null, CultureInfo.CurrentCulture);
			if (result is TimeSpan actual)
			{
				Assert.AreEqual(target, actual);
			}
			else
			{
				Assert.Fail($"The result should be a '{nameof(TimeSpan)}' but actually is '{result?.GetType().Name ?? "[UNKNOWN]"}'.");
			}
		}

		#endregion
	}
}