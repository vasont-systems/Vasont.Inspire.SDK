# Vasont.Inspire.SDK.Components.ComponentExtensions.ImportComponents method
## ImportComponents(InspireClient, ImportRequestModel, List<string>)
This REST method is used to import components from one or more files.

### Signature
```csharp
public static Vasont.Inspire.Models.Worker.MinimalWorkerStateModel<Vasont.Inspire.Models.Transfers.MinimalImportStateModel> ImportComponents(InspireClient client, ImportRequestModel importModel, List<string> filePaths)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `importModel`: The import model.
- `filePaths`: Contains a list of local file paths that will be submitted for synchronization import.

### Returns
Returns a  of type .
### Remarks

