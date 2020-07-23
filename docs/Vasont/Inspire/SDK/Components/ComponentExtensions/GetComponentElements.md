# Vasont.Inspire.SDK.Components.ComponentExtensions.GetComponentElements method
## GetComponentElements(InspireClient, long, List<string>)
This method is used to retrieve the specified components elements.

### Signature
```csharp
public static System.Collections.Generic.List<Vasont.Inspire.Models.Components.XmlComponentElementModel> GetComponentElements(InspireClient client, long componentId, List<string> allowedElements)
```
### Parameters
- `client`: used to communication with the API endpoint.
- `componentId`: The list of component identifier.
- `allowedElements`: Contains an optional list of allowed element names.

### Returns
Returns a  object containing a list of requested components.
### Remarks

