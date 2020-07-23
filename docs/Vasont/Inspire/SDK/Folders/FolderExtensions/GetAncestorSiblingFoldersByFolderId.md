# Vasont.Inspire.SDK.Folders.FolderExtensions.GetAncestorSiblingFoldersByFolderId method
## GetAncestorSiblingFoldersByFolderId(InspireClient, long)
This method is used to return a list of folders including the specified folder, 
            its ancestor folders, and corresponding sibling folders.

### Signature
```csharp
public static System.Collections.Generic.List<Vasont.Inspire.Models.Components.FolderBrowseModel> GetAncestorSiblingFoldersByFolderId(InspireClient client, long folderId)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `folderId`: Contains the folder identity.

### Returns
Returns a list of  objects if found.
### Remarks

