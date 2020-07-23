# Vasont.Inspire.SDK.Projects.ProjectExtensions.ExportComponents method
## ExportComponents(InspireClient, long, long, bool, long)
This method is used to request an export process for one or more components.

### Signature
```csharp
public static Vasont.Inspire.Models.Transfers.ExportRequestModel ExportComponents(InspireClient client, long projectId, long folderId, bool includeSubFolders, long exportId = 0)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `projectId`: Contains the identity of the project.
- `folderId`: Contains the identity of the project folder.
- `includeSubFolders`: Contains a value indicating whether components in descendant sub-folders should be exported.
- `exportId`: Contains the export identity.

### Returns
The  object with information about the export process.
### Remarks

