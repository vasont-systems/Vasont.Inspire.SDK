# Vasont.Inspire.SDK.Components.ComponentExtensions.GetComponentHistoryDetails method
## GetComponentHistoryDetails(InspireClient, long, Guid)
This method is used to get the history for the specified component.

### Signature
```csharp
public static Vasont.Inspire.Models.Versioning.MinimalComponentHistoryModel GetComponentHistoryDetails(InspireClient client, long componentId, Guid changesetId)
```
### Parameters
- `client`: used to communication with the API endpoint.
- `componentId`: Contains the component identity.
- `changesetId`: Contains the specific history changeset record identity.

### Returns
Returns a  model.
### Remarks

