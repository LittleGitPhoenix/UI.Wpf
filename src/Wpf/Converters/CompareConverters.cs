#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Globalization;
using System.Windows;

namespace Phoenix.UI.Wpf.Converters
{
	#region Base

	/// <summary>
	/// Base class for all <see cref="CompareConverter{T}"/>s.
	/// </summary>
	public abstract class CompareConverter<T> : SourceValueConverter<T>
		where T : CompareConverter<T>, new()
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (int.TryParse(value?.ToString(), out int actual) && int.TryParse(parameter?.ToString(), out int target))
			{
				return this.Compare(actual, target);
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Compares the two values.
		/// </summary>
		/// <param name="actual"> The actual value. </param>
		/// <param name="target"> The target value. </param>
		/// <returns> <c>TRUE</c> or <c>FALSE</c>. </returns>
		public abstract bool Compare(int actual, int target);
	}

	/// <summary>
	/// Base class for all <see cref="CompareToVisibilityConverter{T}"/>s.
	/// </summary>
	public abstract class CompareToVisibilityConverter<T> : SourceValueToVisibilityConverter<T>
		where T : CompareToVisibilityConverter<T>, new()
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (int.TryParse(value?.ToString(), out int actual) && int.TryParse(parameter?.ToString(), out int target))
			{
				return this.Compare(actual, target) ? base.TrueVisibility  : base.FalseVisibility;
			}
			else
			{
				return base.FalseVisibility;
			}
		}

		/// <summary>
		/// Compares the two values.
		/// </summary>
		/// <param name="actual"> The actual value. </param>
		/// <param name="target"> The target value. </param>
		/// <returns> <c>TRUE</c> or <c>FALSE</c>. </returns>
		public abstract bool Compare(int actual, int target);
	}

	#endregion

	#region IsEqualTo

	/// <summary> 
	/// Checks if the bound value equals the passed parameter.
	/// </summary>
	public class IsEqualToConverter : SourceValueConverter<IsEqualToConverter>
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Object.Equals(value, parameter);
		}
	}

	/// <summary> 
	/// Checks if the bound value equals the passed parameter and converts it into configurable <see cref="Visibility"/>.
	/// </summary>
	public class IsEqualToToVisibilityConverter : SourceValueToVisibilityConverter<IsEqualToToVisibilityConverter>
	{
		/// <inheritdoc />
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Object.Equals(value, parameter) ? base.TrueVisibility : base.FalseVisibility;
		}
	}

	#endregion

	#region IsEqualOrGreaterThan

	/// <summary>
	/// Checks if the bound value is equal or greater than the passed parameter.
	/// </summary>
	public class IsEqualOrGreaterThanConverter : CompareConverter<IsEqualOrGreaterThanConverter>
	{
		/// <inheritdoc />
		public override bool Compare(int actual, int target)
		{
			return actual >= target;
		}
	}

	/// <summary>
	/// Checks if the bound value is equal or greater than the passed parameter and converts it into configurable <see cref="Visibility"/>.
	/// </summary>
	public class IsEqualOrGreaterThanToVisibilityConverter : CompareToVisibilityConverter<IsEqualOrGreaterThanToVisibilityConverter>
	{
		/// <inheritdoc />
		public override bool Compare(int actual, int target)
		{
			return actual >= target;
		}
	}

	#endregion

	#region IsGreaterThan

	/// <summary>
	/// Checks if the bound value is greater than the passed parameter.
	/// </summary>
	public class IsGreaterThanConverter : CompareConverter<IsGreaterThanConverter>
	{
		/// <inheritdoc />
		public override bool Compare(int actual, int target)
		{
			return actual > target;
		}
	}

	/// <summary>
	/// Checks if the bound value is greater than the passed parameter and converts it into configurable <see cref="Visibility"/>.
	/// </summary>
	public class IsGreaterThanToVisibilityConverter : CompareToVisibilityConverter<IsGreaterThanToVisibilityConverter>
	{
		/// <inheritdoc />
		public override bool Compare(int actual, int target)
		{
			return actual > target;
		}
	}

	#endregion

	#region IsEqualOrLowerThan

	/// <summary>
	/// Checks if the bound value is equal or lower than the passed parameter.
	/// </summary>
	public class IsEqualOrLowerThanConverter : CompareConverter<IsEqualOrLowerThanConverter>
	{
		/// <inheritdoc />
		public override bool Compare(int actual, int target)
		{
			return actual <= target;
		}
	}

	/// <summary>
	/// Checks if the bound value is equal or lower than the passed parameter and converts it into configurable <see cref="Visibility"/>.
	/// </summary>
	public class IsEqualOrLowerThanToVisibilityConverter : CompareToVisibilityConverter<IsEqualOrLowerThanToVisibilityConverter>
	{
		/// <inheritdoc />
		public override bool Compare(int actual, int target)
		{
			return actual <= target;
		}
	}

	#endregion

	#region IsLowerThan
	
	/// <summary>
	/// Checks if the bound value is lower than the passed parameter.
	/// </summary>
	public class IsLowerThanConverter : CompareConverter<IsLowerThanConverter>
	{
		/// <inheritdoc />
		public override bool Compare(int actual, int target)
		{
			return actual < target;
		}
	}

	/// <summary>
	/// Checks if the bound value is lower than the passed parameter and converts it into configurable <see cref="Visibility"/>.
	/// </summary>
	public class IsLowerThanToVisibilityConverter : CompareToVisibilityConverter<IsLowerThanToVisibilityConverter>
	{
		/// <inheritdoc />
		public override bool Compare(int actual, int target)
		{
			return actual < target;
		}
	}

	#endregion
}