# Vasont.Inspire.SDK.Folders.FolderExtensions.GetComponentsByFolderId method
## GetComponentsByFolderId(InspireClient, long, FolderComponentsQueryModel)
This method is used to return components that are stored within a specific folder matching a specific search criteria.

### Signature
```csharp
public static Vasont.Inspire.Models.Components.FolderComponentsResultModel GetComponentsByFolderId(InspireClient client, long folderId, FolderComponentsQueryModel inputModel)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `folderId`: Contains the folder identity to retrieve components from, pass a value of zero to retrieve all components.
- `inputModel`: Contains the  input.

### Returns
Returns a  object if found.
### Remarks

