# Vasont.Inspire.SDK.Folders.FolderExtensions.GetFoldersByFolderId method
## GetFoldersByFolderId(InspireClient, long, bool, PermissionFlags)
This method is used to return a list of folders that are children of the specified parent folder. 
            If parent folder is not specified, a list of root folders will be returned.

### Signature
```csharp
public static System.Collections.Generic.List<Vasont.Inspire.Models.Components.FolderBrowseModel> GetFoldersByFolderId(InspireClient client, long folderId, bool includeAllDescendantFolders, PermissionFlags permissionFlag)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `folderId`: Contains the folder identity to retrieve child folders from, pass a value of zero to retrieve all folders.
- `includeAllDescendantFolders`: Contains a value to indicate whether or not to include all descendant folders.
- `permissionFlag`: Contains an optional  that will be used to filter the results of the query.

### Returns
Returns a list of  objects if found.
### Remarks

