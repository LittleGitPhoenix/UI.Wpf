#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using System.Linq;
using System.Windows;
using ControlzEx.Theming;
using Phoenix.UI.Wpf.Helper;

namespace Phoenix.UI.Wpf.Metro.Helper
{
	/// <summary>
	/// Helper class for changing application themes.
	/// </summary>
	public static class ApplicationThemeChanger
	{
		#region Delegates / Events
		
		/// <summary> Is raised once the <see cref="MainWindow"/> is available. </summary>
		private static event EventHandler<Window> MainWindowAvailable;
		
		#endregion

		#region Constants
		#endregion

		#region Fields

		private static readonly object Lock;

		#endregion

		#region Properties
		
		/// <summary> The current applications <see cref="Application.MainWindow"/>. </summary>
		private static Window MainWindow
		{
			get
			{
				lock (ApplicationThemeChanger.Lock)
				{
					return _mainWindow;
				}
			}
			set
			{
				lock (ApplicationThemeChanger.Lock)
				{
					// Don't set null values.
					if (value is null) return;

					// Allow setting only once.
					if (_mainWindow != null) return;

					// Remove listener for application activation.
					Application.Current.Activated -= ApplicationThemeChanger.HandleApplicationActivated;

					// Save the value.
					_mainWindow = value;

					// Raise the window available event.
					ApplicationThemeChanger.MainWindowAvailable?.Invoke(null, value);
				}
			}
		}
		private static Window _mainWindow;

		#endregion

		#region (De)Constructors
		
		/// <summary>
		/// Static constructor
		/// </summary>
		static ApplicationThemeChanger()
		{
			ApplicationThemeChanger.Lock = new object();

			if (Application.Current is null)
			{
				System.Diagnostics.Trace.WriteLine($"ERROR: Cannot change the application theme because {nameof(Application)}.{nameof(Application.Current)} is null. This can happen when running unit tests.");
				return;
			}

			// Attach to the applications activated event, so that an reference to its main window can be obtained once it is available.
			Application.Current.Activated += ApplicationThemeChanger.HandleApplicationActivated;

			// Try to get a reference to the applications main window regardless of the activation event (it could have been fired already).
			ApplicationThemeChanger.MainWindow = ApplicationThemeChanger.GetMainWindow();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Handler for the current applications <see cref="Application.Activated"/> event.
		/// </summary>
		private static void HandleApplicationActivated(object sender, EventArgs args)
		{
			// Try to get a reference to the applications main window.
			ApplicationThemeChanger.MainWindow = ApplicationThemeChanger.GetMainWindow();
		}

		/// <summary>
		/// Get the current applications <see cref="Application.MainWindow"/>.
		/// </summary>
		/// <returns> A reference to the current applications <see cref="Application.MainWindow"/>. </returns>
		private static Window GetMainWindow()
		{
			Window GetWindow()
			{
				return Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive) ?? Application.Current.MainWindow;
			}

			return Application.Current.Dispatcher.CheckAccess() ? GetWindow() : Application.Current.Dispatcher.Invoke(GetWindow);
		}
		
		/// <summary>
		/// Changes the <see cref="Theme"/> of the current application to match the operating system style.
		/// </summary>
		public static void ChangeApplicationThemeToMatchOperatingSystem()
		{
			void ChangeHandler(object sender, Window mainWindow)
			{
				ApplicationThemeChanger.MainWindowAvailable += ChangeHandler;
				Change(mainWindow);
			}
			
			void Change(Window window)
			{
				var osTheme = Phoenix.UI.Wpf.Helper.OperatingSystemThemeDetection.GetTheme();
				var currentTheme = window is null ? null : ThemeManager.Current.DetectTheme(window);
				var newBaseColor = osTheme == OperatingSystemTheme.Dark ? "Dark" : "Light";
				var currentAccentColor = currentTheme.ColorScheme.ToLower();
				var newTheme = ThemeManager.Current.GetTheme($"{newBaseColor}.{currentAccentColor}") ?? currentTheme;

				ThemeManager.Current.ChangeTheme(window, newTheme);
			}

			if (ApplicationThemeChanger.MainWindow is null)
			{
				ApplicationThemeChanger.MainWindowAvailable += ChangeHandler;
			}
			else
			{
				Change(ApplicationThemeChanger.MainWindow);
			}
		}

		#endregion
	}
}