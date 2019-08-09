# Phoenix.UI.Wpf.Base

| .NET Framework | .NET Standard | .NET Core |
| :-: | :-: | :-: |
| :heavy_check_mark: 4.5 | :heavy_minus_sign: | :heavy_check_mark: 3.1 |

Collection of base projects for general **WPF** development.
___

# Converters

To use the converters reference the namespace in the root of the **XAML** document.

```xml
xmlns:phoenix="http://programming.little-phoenix.de/wpf/"
```
The following symbols are used to specify how the converters are able to handle value conversion.

| Symbol | Meaning |
| :-: | :- |
| :arrow_up: | Conversion is only from source to destination. |
| :arrow_down: | Conversion is only from destination to source. |
| :arrow_up_down: | Conversion is two ways. |

The following symbols have other meaning.

| Symbol | Meaning |
| :-: | :- |
| :white_circle: | This converter provides a **ToVisibility** version. |
| :red_circle: | This converter provides an inverse **Not** version. |

## BoolToVisibilityConverter :arrow_up_down: :white_circle:

- Checks if the bound **Boolean** is **True** or **False** and converts it into configurable **Visibility**.

## Compare Converters

### IsEqualToConverter :arrow_up: :white_circle: 

- Checks if the bound value equals the passed parameter.

### IsEqualOrGreaterThanConverter :arrow_up: :white_circle: 

- Checks if the bound value is equal or greater than the passed parameter.

### IsGreaterThanConverter :arrow_up: :white_circle: 

- Checks if the bound value is greater than the passed parameter.

### IsEqualOrLowerThanConverter :arrow_up: :white_circle: 

- Checks if the bound value is equal or lower than the passed parameter.

### IsLowerThanConverter :arrow_up: :white_circle: 

- Checks if the bound value is lower than the passed parameter.

## EnumEqualsConverter :arrow_up: :white_circle: :red_circle:

- Checks if the bound **Enum** value matches the **Enum** value of the **ConverterParameter**.

## EnumToCollectionConverter :arrow_up_down:

- Converts the underlying **Enum** of the passed value into a collection of **EnumDescription**.

## HasElementsConverter :arrow_up: :white_circle:

- Checks if the bound **ICollection** contains elements. If the bound property is **NULL** or it could not be cast then **FALSE** will be returned.

## Image Converters

### StreamToImageSourceConverter :arrow_up:

- Converts a **System.IO.Stream** into a **System.Windows.Media.Imaging.BitmapImage** that can be used as source of an image control.

### BytesToImageSourceConverter :arrow_up:

- Converts a **Byte** collection into a **System.Windows.Media.Imaging.BitmapImage** that can be used as source of an image control.
...

## Inverse Converters

### InverseBooleanConverter :arrow_up_down:

- Inverses the bound **Boolean**.

### InverseVisibilityConverter :arrow_up:

- Inverses the bound **Visibility**. Can be used to toggle controls dependent n one another.

## Null Converters

### IsNullConverter :arrow_up: :white_circle: :red_circle:

-  Checks if the bound property is **NULL**.

### IsNullOrWhitespaceConverter :arrow_up: :white_circle:

- Checks if the bound **String** is **NULL** or whitespace and converts it into configurable **Visibility**.

## TimeSpanToDoubleConverter :arrow_up_down:

- Converts a **TimeSpan** into a **Double** and back based on a configurable **UnitOfTime**.

___

# Authors

* **Felix Leistner**: _v1.x_