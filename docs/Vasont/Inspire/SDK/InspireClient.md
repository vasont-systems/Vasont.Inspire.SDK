# InspireClient Class
**Namespace:** Vasont.Inspire.SDK

**Inheritance:** Object → InspireClient

This class is the primary client from which interaction to the Inspire Web API shall be made.

## Signature
```csharp
public class InspireClient : System.IDisposable
```
## Constructors
|**Name**|**Summary**|
|---|---|
|[InspireClient(InspireClientConfiguration)](InspireClient/ctors.md)|Initializes a new instance of the  class.|
## Methods
|**Name**|**Summary**|
|---|---|
|[Authenticate](InspireClient/Authenticate.md)|Authenticates the client with the specified scopes in a synchronous manor.|
|[AuthenticateAsync](InspireClient/AuthenticateAsync.md)|This method is used to retrieve and authorize the client for back-channel communication to the Identity API.|
|[CreateRequest](InspireClient/CreateRequest.md)|This method is used to easily create a new WebRequest object for the Web API.|
|[CreateRequest](InspireClient/CreateRequest.md#createrequeststring-string-bool-icredentials-string)|This method is used to easily create a new WebRequest object for the Web API.|
|[Dispose](InspireClient/Dispose.md)|This method is called upon disposal of the client class.|
|[Equals](InspireClient/Equals.md)||
|[GetHashCode](InspireClient/GetHashCode.md)||
|[GetType](InspireClient/GetType.md)||
|[RequestContent](InspireClient/RequestContent.md)||
|[RequestContent](InspireClient/RequestContent.md#requestcontenthttpwebrequest)|This method is used to execute a web request and return the results of the request as a string.|
|[RequestContent](InspireClient/RequestContent.md#requestcontentt-touthttpwebrequest-t)||
|[RequestContent](InspireClient/RequestContent.md#requestcontentthttpwebrequest-t)||
|[ToString](InspireClient/ToString.md)||
## Properties
|**Name**|**Summary**|
|---|---|
|[Config](InspireClient/Config.md)|Gets the inspire client configuration.
|[HasAuthenticated](InspireClient/HasAuthenticated.md)|Gets a value indicating whether the client has authenticated with the server at some point
|[HasError](InspireClient/HasError.md)|Gets a value indicating whether the client has an error.
|[LastErrorResponse](InspireClient/LastErrorResponse.md)|Gets the last error response model from an internal RequestContent call.
|[LastException](InspireClient/LastException.md)|Gets the last exception handled within the client.
## Conversions
