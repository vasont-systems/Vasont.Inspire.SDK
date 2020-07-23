# Vasont.Inspire.SDK.InspireClient.AuthenticateAsync method
## AuthenticateAsync(string, CancellationToken)
This method is used to retrieve and authorize the client for back-channel communication to the Identity API.

### Signature
```csharp
public System.Threading.Tasks.Task<bool> AuthenticateAsync(string scopes = "", CancellationToken cancellationToken = null)
```
### Parameters
- `scopes`: Contains the scopes that are requested for the client credentials authentication.
- `cancellationToken`: Contains an optional cancellation token.

### Returns
Returns a value indicating whether the authentication was successful.
### Remarks

