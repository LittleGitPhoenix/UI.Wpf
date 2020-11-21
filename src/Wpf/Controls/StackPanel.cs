#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Phoenix.UI.Wpf.Controls
{
	/// <summary> Defines the different fill modes of <see cref="StackPanel"/>. </summary>
	public enum StackPanelFill
	{
		Auto,
		Fill,
		Ignored
	}

    /// <summary>
    /// Replacement for the standard <see cref="System.Windows.Controls.StackPanel"/> that allows for:
    /// <para> • A child item to fill the remaining space (<see cref="FillProperty"/>). </para>
    /// <para> • Defining a margin between all children (<see cref="MarginBetweenChildren"/>). </para>
    /// </summary>
    /// <remarks> https://github.com/kmcginnes/SpicyTaco.AutoGrid (v1.2.29) </remarks>
    public class StackPanel : Panel
    {
        #region Delegates / Events
        #endregion

        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties

		#region Orientation

		public Orientation Orientation
		{
			get => (Orientation) GetValue(OrientationProperty);
			set => SetValue(OrientationProperty, value);
		}

		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register
		(
			"Orientation",
			typeof(Orientation),
			typeof(StackPanel),
			new FrameworkPropertyMetadata
			(
				Orientation.Vertical,
				FrameworkPropertyMetadataOptions.AffectsArrange |
				FrameworkPropertyMetadataOptions.AffectsMeasure
			)
		);

        #endregion

		#region MarginBetweenChildren

        public double MarginBetweenChildren
		{
			get => (double) GetValue(MarginBetweenChildrenProperty);
			set => SetValue(MarginBetweenChildrenProperty, value);
		}

		public static readonly DependencyProperty MarginBetweenChildrenProperty = DependencyProperty.Register
		(
			"MarginBetweenChildren",
			typeof(double),
			typeof(StackPanel),
			new FrameworkPropertyMetadata
			(
				0.0,
				FrameworkPropertyMetadataOptions.AffectsArrange |
				FrameworkPropertyMetadataOptions.AffectsMeasure
			)
		);

        #endregion

		#region Fill

        public static void SetFill(DependencyObject element, StackPanelFill value)
		{
			element.SetValue(FillProperty, value);
		}

		public static StackPanelFill GetFill(DependencyObject element)
		{
			return (StackPanelFill)element.GetValue(FillProperty);
		}
		
        public static readonly DependencyProperty FillProperty = DependencyProperty.RegisterAttached
        (
            "Fill",
            typeof(StackPanelFill),
            typeof(StackPanel),
            new FrameworkPropertyMetadata
            (
                StackPanelFill.Auto,
                FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsParentArrange |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure
            )
        );

		#endregion

        #endregion

        #region (De)Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public StackPanel()
		{
			// Save parameters.

			// Initialize fields.
		}

		#endregion

		#region Methods

		/// <inheritdoc />
		protected override Size MeasureOverride(Size constraint)
        {
            UIElementCollection children = InternalChildren;
            var parentWidth = 0D;
            var parentHeight = 0D;
            var accumulatedWidth = 0D;
            var accumulatedHeight = 0D;
			var isHorizontal = Orientation == Orientation.Horizontal;
            var totalMarginToAdd = StackPanel.CalculateTotalMarginToAdd(children, MarginBetweenChildren);

            for (var i = 0; i < children.Count; i++)
            {
                UIElement child = children[i];

                if (child == null) continue;

                // Handle only the Auto's first to calculate remaining space for Fill's
                if (GetFill(child) != StackPanelFill.Auto) continue;

                // Child constraint is the remaining size; this is total size minus size consumed by previous children.
                var childConstraint = new Size(Math.Max(0.0, constraint.Width - accumulatedWidth), Math.Max(0.0, constraint.Height - accumulatedHeight));

                // Measure child.
                child.Measure(childConstraint);
                var childDesiredSize = child.DesiredSize;

                if (isHorizontal)
                {
                    accumulatedWidth += childDesiredSize.Width;
                    parentHeight = Math.Max(parentHeight, accumulatedHeight + childDesiredSize.Height);
                }
                else
                {
                    parentWidth = Math.Max(parentWidth, accumulatedWidth + childDesiredSize.Width);
                    accumulatedHeight += childDesiredSize.Height;
                }
            }

            // Add all margin to accumulated size before calculating remaining space for
            // Fill elements.
            if (isHorizontal)
            {
                accumulatedWidth += totalMarginToAdd;
            }
            else
            {
                accumulatedHeight += totalMarginToAdd;
            }

            var totalCountOfFillTypes = children.OfType<UIElement>().Count(x => GetFill(x) == StackPanelFill.Fill && x.Visibility != Visibility.Collapsed);
			var availableSpaceRemaining = isHorizontal ? Math.Max(0, constraint.Width - accumulatedWidth) : Math.Max(0, constraint.Height - accumulatedHeight);
			var eachFillTypeSize = totalCountOfFillTypes > 0 ? availableSpaceRemaining / totalCountOfFillTypes : 0;

            for (var i = 0; i < children.Count; i++)
            {
                UIElement child = children[i];

                if (child == null) continue;

                // Handle all the Fill's giving them a portion of the remaining space
                if (GetFill(child) != StackPanelFill.Fill) continue;

                // Child constraint is the remaining size; this is total size minus size consumed by previous children.
                var childConstraint = isHorizontal ? new Size(eachFillTypeSize, Math.Max(0.0, constraint.Height - accumulatedHeight)) : new Size(Math.Max(0.0, constraint.Width - accumulatedWidth), eachFillTypeSize);

                // Measure child.
                child.Measure(childConstraint);
                var childDesiredSize = child.DesiredSize;

                if (isHorizontal)
                {
                    accumulatedWidth += childDesiredSize.Width;
                    parentHeight = Math.Max(parentHeight, accumulatedHeight + childDesiredSize.Height);
                }
                else
                {
                    parentWidth = Math.Max(parentWidth, accumulatedWidth + childDesiredSize.Width);
                    accumulatedHeight += childDesiredSize.Height;
                }
            }

            // Make sure the final accumulated size is reflected in parentSize. 
            parentWidth = Math.Max(parentWidth, accumulatedWidth);
            parentHeight = Math.Max(parentHeight, accumulatedHeight);
            var parent = new Size(parentWidth, parentHeight);

            return parent;
        }

		/// <inheritdoc />
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            UIElementCollection children = InternalChildren;
            var totalChildrenCount = children.Count;
			var accumulatedLeft = 0D;
            var accumulatedTop = 0D;
            var isHorizontal = Orientation == Orientation.Horizontal;
            var marginBetweenChildren = MarginBetweenChildren;
            var totalMarginToAdd = StackPanel.CalculateTotalMarginToAdd(children, marginBetweenChildren);
            var allAutoSizedSum = 0.0D;
            var countOfFillTypes = 0;
			
            foreach (var child in children.OfType<UIElement>())
            {
                var fillType = GetFill(child);
                if (fillType != StackPanelFill.Auto)
                {
                    if (child.Visibility != Visibility.Collapsed && fillType != StackPanelFill.Ignored) countOfFillTypes += 1;
                }
                else
                {
                    var desiredSize = isHorizontal ? child.DesiredSize.Width : child.DesiredSize.Height;
                    allAutoSizedSum += desiredSize;
                }
            }

            var remainingForFillTypes = isHorizontal ? Math.Max(0, arrangeSize.Width - allAutoSizedSum - totalMarginToAdd) : Math.Max(0, arrangeSize.Height - allAutoSizedSum - totalMarginToAdd);
            var fillTypeSize = remainingForFillTypes / countOfFillTypes;

            for (var i = 0; i < totalChildrenCount; ++i)
            {
                UIElement child = children[i];
                if (child == null) continue;
                Size childDesiredSize = child.DesiredSize;
                var fillType = GetFill(child);
                var isCollapsed = child.Visibility == Visibility.Collapsed || fillType == StackPanelFill.Ignored;
                var isLastChild = i == totalChildrenCount - 1;
                var marginToAdd = isLastChild || isCollapsed ? 0 : marginBetweenChildren;

				Rect rcChild = new Rect
				(
					accumulatedLeft,
					accumulatedTop,
					Math.Max(0.0, arrangeSize.Width - accumulatedLeft),
					Math.Max(0.0, arrangeSize.Height - accumulatedTop)
				);

                if (isHorizontal)
                {
                    rcChild.Width = fillType == StackPanelFill.Auto || isCollapsed ? childDesiredSize.Width : fillTypeSize;
                    rcChild.Height = arrangeSize.Height;
                    accumulatedLeft += rcChild.Width + marginToAdd;
                }
                else
                {
                    rcChild.Width = arrangeSize.Width;
                    rcChild.Height = fillType == StackPanelFill.Auto || isCollapsed ? childDesiredSize.Height : fillTypeSize;
                    accumulatedTop += rcChild.Height + marginToAdd;
                }

                child.Arrange(rcChild);
            }

            return arrangeSize;
        }
        
		static double CalculateTotalMarginToAdd(UIElementCollection children, double marginBetweenChildren)
		{
			var visibleChildrenCount = children
				.OfType<UIElement>()
				.Count(x => x.Visibility != Visibility.Collapsed && GetFill(x) != StackPanelFill.Ignored)
				;
			var marginMultiplier = Math.Max(visibleChildrenCount - 1, 0);
			var totalMarginToAdd = marginBetweenChildren * marginMultiplier;
			return totalMarginToAdd;
		}

        #endregion
    }
}