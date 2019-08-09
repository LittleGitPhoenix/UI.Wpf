#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace Phoenix.UI.Wpf.Base.Extensions
{
	/// <summary>
	/// Encapsulates methods for dealing with dependency objects and properties.
	/// </summary>
	public static class ExtendedDependencyObject
	{
		/// <summary>
		/// Gets all children of the <paramref name="dependencyObject"/> matching <typeparamref name="TChild"/>.
		/// </summary>
		/// <typeparam name="TChild"> The type of the children to find. </typeparam>
		/// <param name="dependencyObject"> The <see cref="DependencyObject"/> to search for matching children. </param>
		/// <param name="recursive"> Optional flag if the search should be recursively throughout all children. Default is <c>True</c>. </param>
		/// <returns> An <see cref="IEnumerable{TChild}"/>. </returns>
		public static IEnumerable<TChild> GetVisualChildren<TChild>(this DependencyObject dependencyObject, bool recursive = true) where TChild : DependencyObject
		{
			// Fail fast.
			if (dependencyObject is null) yield break;
			
			for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
			{
				// Find all children matching the type.
				var child = VisualTreeHelper.GetChild(dependencyObject, i);
				if (child is TChild variable)
				{
					yield return variable;
				}

				// Iterate the child's children if necessary.
				if (recursive)
				{
					foreach (TChild childOfChild in child.GetVisualChildren<TChild>(recursive: true))
					{
						yield return childOfChild;
					}
				}
			}
		}

		/// <summary>
		/// Gets the dependency property according to its name.
		/// </summary>
		/// <param name="type"> The type. </param>
		/// <param name="propertyName"> The name of the property. </param>
		/// <returns> The matching <see cref="DependencyProperty"/>. </returns>
		/// <remarks> https://github.com/kmcginnes/SpicyTaco.AutoGrid (v1.2.29) </remarks>
		public static DependencyProperty GetDependencyProperty(Type type, string propertyName)
		{
			FieldInfo fieldInfo = type?.GetField(propertyName + "Property", BindingFlags.Static | BindingFlags.Public);
			return fieldInfo?.GetValue(null) as DependencyProperty;
		}

		/// <summary>
		/// Retrieves a <see cref="DependencyProperty"/> using reflection.
		/// </summary>
		/// <param name="dependencyObject"></param>
		/// <param name="propertyName"> The name of the property. </param>
		/// <returns> The matching <see cref="DependencyProperty"/>. </returns>
		/// <remarks> https://github.com/kmcginnes/SpicyTaco.AutoGrid (v1.2.29) </remarks>
		public static DependencyProperty GetDependencyProperty(this DependencyObject dependencyObject, string propertyName)
		{
			if (dependencyObject is null) return null;
			return ExtendedDependencyObject.GetDependencyProperty(dependencyObject.GetType(), propertyName);
		}

		/// <summary>
		/// Sets the value of the <paramref name="dependencyProperty"/> only if it hasn't been explicitly set.
		/// </summary>
		/// <typeparam name="T"> The type of the <see cref="DependencyProperty"/>. </typeparam>
		/// <param name="dependencyObject"> The <see cref="DependencyObject"/> holding the <paramref name="dependencyProperty"/>. </param>
		/// <param name="dependencyProperty"> The <see cref="DependencyProperty"/>. </param>
		/// <param name="value"> The new value. </param>
		/// <returns> <c>True</c> on success, otherwise <c>False</c>. </returns>
		/// <remarks> https://github.com/kmcginnes/SpicyTaco.AutoGrid (v1.2.29) </remarks>
		public static bool SetIfDefault<T>(this DependencyObject dependencyObject, DependencyProperty dependencyProperty, T value)
		{
			if (dependencyObject == null) throw new ArgumentNullException(nameof(dependencyObject));
			if (dependencyProperty == null) throw new ArgumentNullException(nameof(dependencyProperty));

			// Check the type.
			if (!dependencyProperty.PropertyType.IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException($"Expected {dependencyProperty.Name} to be of type {typeof(T).Name} but was {dependencyProperty.PropertyType}");
			}

			// Check if the dependency property still has its default value.
			if (DependencyPropertyHelper.GetValueSource(dependencyObject, dependencyProperty).BaseValueSource != BaseValueSource.Default) return false;

			// Set the value.
			dependencyObject.SetValue(dependencyProperty, value);
			return true;
		}
	}
}