#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System;
using Microsoft.Win32;

namespace Phoenix.UI.Wpf.Helper
{
	/// <summary>
	/// Defines different themes an operating system can have.
	/// </summary>
	public enum OperatingSystemTheme
	{
		/// <summary> Light theme. </summary>
		Light,
		/// <summary> Dark theme. </summary>
		Dark
	}
	
	/// <summary>
	/// Helper class for detecting the theme of the operating system.
	/// </summary>
	public static class OperatingSystemThemeDetection
	{
		#region Delegates / Events
		#endregion

		#region Constants

		private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

		private const string RegistryValueName = "AppsUseLightTheme";
		
		#endregion

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region (De)Constructors

		static OperatingSystemThemeDetection() { }

		#endregion

		#region Methods
		
		/// <summary>
		/// Gets the <see cref="OperatingSystemTheme"/> of the operating system. This currently only works on Windows machines.
		/// </summary>
		/// <returns> The current <see cref="OperatingSystemTheme"/> for Windows OS, otherwise <see cref="OperatingSystemTheme.Light"/>. </returns>
		public static OperatingSystemTheme GetTheme()
		{
#if !NETFRAMEWORK
			if (!System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) return OperatingSystemTheme.Light;
#endif

			try
			{
				using RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
				var registryValueObject = key?.GetValue(RegistryValueName);

				if (registryValueObject is null || !int.TryParse(registryValueObject.ToString(), out var registryValue)) return OperatingSystemTheme.Light;
				return registryValue > 0 ? OperatingSystemTheme.Light : OperatingSystemTheme.Dark;
			}
			catch
			{
				return OperatingSystemTheme.Light;
			}
		}
		
#endregion
	}
}