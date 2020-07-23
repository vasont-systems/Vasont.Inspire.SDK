# Vasont.Inspire.SDK.Components.ComponentExtensions.UpdateComponentPermissions method
## UpdateComponentPermissions(InspireClient, PermissionUpdateModel)
This method is used to set permissions on an existing component.

### Signature
```csharp
public static Vasont.Inspire.Models.Components.PermissionUpdateModel UpdateComponentPermissions(InspireClient client, PermissionUpdateModel inputModel)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `inputModel`: Contains the  input.

### Returns
Returns a  object if found.
### Remarks

## UpdateComponentPermissions(InspireClient, long, PermissionUpdateModel)
This method is used to update permissions for a single component.

### Signature
```csharp
public static Vasont.Inspire.Models.Components.PermissionUpdateModel UpdateComponentPermissions(InspireClient client, long componentId, PermissionUpdateModel inputModel)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `componentId`: Contains the component identity.
- `inputModel`: Contains the  input.

### Returns
Returns the updated  object.
### Remarks

