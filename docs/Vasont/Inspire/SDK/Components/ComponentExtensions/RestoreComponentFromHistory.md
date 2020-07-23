# Vasont.Inspire.SDK.Components.ComponentExtensions.RestoreComponentFromHistory method
## RestoreComponentFromHistory(InspireClient, long, Guid, RestoreOptionsModel)
This method is used to get the history for the specified component.

### Signature
```csharp
public static Vasont.Inspire.Models.Versioning.ChangesetModel RestoreComponentFromHistory(InspireClient client, long componentId, Guid changesetId, RestoreOptionsModel inputModel)
```
### Parameters
- `client`: used to communication with the API endpoint.
- `componentId`: Contains the component identity.
- `changesetId`: Contains the specific history changeset record identity.
- `inputModel`: Contains the  model.

### Returns
Returns a  model.
### Remarks

