# Vasont.Inspire.SDK.Folders.FolderExtensions.ExportFolder method
## ExportFolder(InspireClient, long, long, bool)
This method is used to initiate an export request and return details about the successfully submitted export request.

### Signature
```csharp
public static Vasont.Inspire.Models.Transfers.ExportRequestModel ExportFolder(InspireClient client, long targetFolderId, long exportId = 0, bool includeSubFolders = False)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `targetFolderId`: Contains the folder identity.
- `exportId`: Contains the export identity value.
- `includeSubFolders`: Contains a flag indicating whether to include subfolders or not.

### Returns
Returns a  object if found.
### Remarks

