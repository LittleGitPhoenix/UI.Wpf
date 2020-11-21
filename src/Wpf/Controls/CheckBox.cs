#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System.Windows;

namespace Phoenix.UI.Wpf.Controls
{
	/// <summary>
	/// The different toggle orders of a <see cref="CheckBox"/>.
	/// </summary>
	public enum CheckBoxToggleOrder
	{
		/// <summary>
		/// Toggle order is not changed and should be <c>Null</c> → <c>False</c> → <c>True</c>.
		/// </summary>
		Default,
		/// <summary>
		/// Toggle order is <c>Null</c> → <c>True</c> → <c>False</c>.
		/// </summary>
		NullTrueFalse,
	}

	/// <summary>
	/// Custom <see cref="System.Windows.Controls.CheckBox"/> implementation that allows for a custom toggle order (<see cref="ToggleOrder"/>-Property).
	/// </summary>
	/// <remarks> An attached behavior or an external <see cref="DependencyProperty"/> cannot handle this, because the original <see cref="System.Windows.Controls.CheckBox"/> does not expose a <c>Toggled</c> event. </remarks>
	public class CheckBox : System.Windows.Controls.CheckBox
	{
		/// <summary>
		/// Defines the order in which the toggling is handled.
		/// </summary>
		public static readonly DependencyProperty ToggleOrderProperty = DependencyProperty.Register
		(
			nameof(ToggleOrder),
			typeof(CheckBoxToggleOrder),
			typeof(CheckBox),
			new PropertyMetadata(CheckBoxToggleOrder.Default)
		);
		public CheckBoxToggleOrder ToggleOrder
		{
			get => (CheckBoxToggleOrder) GetValue(ToggleOrderProperty);
			set => SetValue(ToggleOrderProperty, value);
		}

		/// <inheritdoc />
		protected override void OnToggle()
		{
			switch (this.ToggleOrder)
			{
				case CheckBoxToggleOrder.NullTrueFalse:
				{
					if (this.IsChecked == true)
					{
						this.IsChecked = false;
					}
					else if (this.IsChecked == false)
					{
						this.IsChecked = this.IsThreeState ? null : (bool?) true;
					}
					else
					{
						this.IsChecked = true;
					}
					break;
				}

				case CheckBoxToggleOrder.Default:
				default:
				{

					base.OnToggle();
					return;
				}
			}
		}
	}
}