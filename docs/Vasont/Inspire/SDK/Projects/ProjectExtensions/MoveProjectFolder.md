# Vasont.Inspire.SDK.Projects.ProjectExtensions.MoveProjectFolder method
## MoveProjectFolder(InspireClient, ProjectFolderItemModel, long, string, string)
This method is used to move a project folder to another target folder.

### Signature
```csharp
public static Vasont.Inspire.Models.Projects.ProjectFolderItemModel MoveProjectFolder(InspireClient client, ProjectFolderItemModel model, long projectId, string itemId, string targetFolderId = "")
```
### Parameters
- `client`: Contains the  that is used for communication.
- `model`: Contains the existing project folder item model to move.
- `projectId`: Contains the identity of the parent project.
- `itemId`: Contains the identity of the folder item object.
- `targetFolderId`: Contains the new target folder of the folder item.

### Returns
Returns the  object.
### Remarks

