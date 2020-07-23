# Vasont.Inspire.SDK.Components.ComponentExtensions.GetImportState method
## GetImportState(InspireClient, string)
This method is called to get the state of an import worker process.

### Signature
```csharp
public static Vasont.Inspire.Models.Worker.MinimalWorkerStateModel<Vasont.Inspire.Models.Transfers.MinimalImportStateModel> GetImportState(InspireClient client, string workerKey)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `workerKey`: Contains the import worker key to find.

### Returns
Returns a  of type .
### Remarks

## GetImportState(InspireClient, WorkerStateModel)
This method is called to get the state of an import worker process.

### Signature
```csharp
public static Vasont.Inspire.Models.Worker.MinimalWorkerStateModel<Vasont.Inspire.Models.Transfers.MinimalImportStateModel> GetImportState(InspireClient client, WorkerStateModel inputModel)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `inputModel`: Contains the  input.

### Returns
Returns a  of type .
### Remarks

