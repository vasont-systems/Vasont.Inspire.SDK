# Vasont.Inspire.SDK.Components.ComponentExtensions.GetComponentConfiguration method
## GetComponentConfiguration(InspireClient, long, bool)
This method is used to retrieve a component configuration for a specified component

### Signature
```csharp
public static Vasont.Inspire.Models.Components.Schema.ComponentConfigurationModel GetComponentConfiguration(InspireClient client, long componentId, bool forceReload = False)
```
### Parameters
- `client`: used to communication with the API endpoint.
- `componentId`: Contains the component identity.
- `forceReload`: Contains a value indicating whether the configuration cache is bypassed and reloaded.

### Returns
Returns the  object.
### Remarks

