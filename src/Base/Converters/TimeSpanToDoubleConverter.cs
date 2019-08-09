#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Globalization;

namespace Phoenix.UI.Wpf.Base.Converters
{
	/// <summary>
	/// Unit of time to be used with the <see cref="TimeSpanToDoubleConverter"/>.
	/// </summary>
	public enum UnitOfTime
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member → Should be self-explanatory.
		Ticks,
		Milliseconds,
		Seconds,
		Minutes,
		Hours,
		Days,
#pragma warning restore CS1591
	}

	/// <summary>
	/// Converts a <see cref="TimeSpan"/> into a <see cref="Double"/> and back.
	/// </summary>
	[System.Windows.Data.ValueConversion(sourceType: typeof(TimeSpan), targetType: typeof(Double))]
	public class TimeSpanToDoubleConverter : SourceValueConverter<TimeSpanToDoubleConverter>
	{
		/// <summary> The <see cref="UnitOfTime"/> used for conversion. </summary>
		public UnitOfTime UnitOfTime { get; set; } = UnitOfTime.Milliseconds;

		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is TimeSpan timeSpan)) return 0d;

			switch (this.UnitOfTime)
			{
				case UnitOfTime.Ticks:
					return System.Convert.ToDouble(timeSpan.Ticks);
				case UnitOfTime.Seconds:
					return timeSpan.TotalSeconds;
				case UnitOfTime.Minutes:
					return timeSpan.TotalMinutes;
				case UnitOfTime.Hours:
					return timeSpan.TotalHours;
				case UnitOfTime.Days:
					return timeSpan.TotalDays;
				case UnitOfTime.Milliseconds:
				default:
					return timeSpan.TotalMilliseconds;
			}
		}

		/// <inheritdoc />
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var @double = System.Convert.ToDouble(value);
				
				switch (this.UnitOfTime)
				{
					case UnitOfTime.Ticks:
						return TimeSpan.FromTicks(System.Convert.ToInt64(@double));
					case UnitOfTime.Seconds:
						return TimeSpan.FromSeconds(@double);
					case UnitOfTime.Minutes:
						return TimeSpan.FromMinutes(@double);
					case UnitOfTime.Hours:
						return TimeSpan.FromHours(@double);
					case UnitOfTime.Days:
						return TimeSpan.FromDays(@double);
					case UnitOfTime.Milliseconds:
					default:
						return TimeSpan.FromMilliseconds(@double);
				}
			}
			catch (InvalidCastException)
			{
				return TimeSpan.Zero;
			}
		}
	}
}