#region LICENSE NOTICE
//! This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of this source code package.
#endregion


using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
	ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located (used if a resource is not found in the page, or application resource dictionaries)
	ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located (used if a resource is not found in the page, app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsPrefix("http://programming.little-phoenix.de/wpf/", "phoenix")]
//[assembly: XmlnsDefinition("http://programming.little-phoenix.de/wpf/", "Phoenix.UI.Wpf.Base.AttachedProperties")]
//[assembly: XmlnsDefinition("http://programming.little-phoenix.de/wpf/", "Phoenix.UI.Wpf.Base.Controls")]
[assembly: XmlnsDefinition("http://programming.little-phoenix.de/wpf/", "Phoenix.UI.Wpf.Base.Converters")]
//[assembly: XmlnsDefinition("http://programming.little-phoenix.de/wpf/", "Phoenix.UI.Wpf.Base.Helper")]

// Make internals visible to the test project.
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Base.Test")]