# Vasont.Inspire.SDK.Components.ComponentExtensions.GetExportState method
## GetExportState(InspireClient, string)
This method is called to get the state of an export worker process.

### Signature
```csharp
public static Vasont.Inspire.Models.Worker.MinimalWorkerStateModel<Vasont.Inspire.Models.Transfers.MinimalExportStateModel> GetExportState(InspireClient client, string workerKey)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `workerKey`: Contains the export worker key to find.

### Returns
Returns a  of type .
### Remarks

## GetExportState(InspireClient, WorkerStateModel)
This method is called to get the state of an export worker process.

### Signature
```csharp
public static Vasont.Inspire.Models.Worker.MinimalWorkerStateModel<Vasont.Inspire.Models.Transfers.MinimalExportStateModel> GetExportState(InspireClient client, WorkerStateModel inputModel)
```
### Parameters
- `client`: Contains the  that is used for communication.
- `inputModel`: Contains the  input.

### Returns
Returns a  of type .
### Remarks

