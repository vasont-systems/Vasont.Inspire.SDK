# Vasont.Inspire.SDK.InspireClientConfiguration constructors
## InspireClientConfiguration()
Initializes a new instance of the  class.

### Signature
```csharp
public InspireClientConfiguration()
```
### Remarks

## InspireClientConfiguration(string, ClientAuthenticationMethods, string, string, string, string, string, bool, string)
Initializes a new instance of the  class.

### Signature
```csharp
public InspireClientConfiguration(string clientId, ClientAuthenticationMethods clientAuthenticationMethod, string authorityUri, string resourceUri, string clientSecret = "", string userId = "", string password = "", bool useDiscovery = True, string delegatedAccessToken = "")
```
### Parameters
- `clientId`: Contains the client identity to use for authentication.
- `clientAuthenticationMethod`: Contains an optional client authentication method.
- `authorityUri`: Contains the authority URI that will validate the client credentials and produce access token.
- `resourceUri`: Contains the API resource URI that will be accessed by the client.
- `clientSecret`: Contains an optional client secret key for client credential authentication method.
- `userId`: Contains the user id used for password authentication method.
- `password`: Contains the user password used for password authentication method.
- `useDiscovery`: Contains a value indicating whether the authority discovery endpoint will be used for token endpoint lookup.
- `delegatedAccessToken`: Contains an optional access token passed from the client software to be used in communication with the backchannel created within the SDK library.

### Remarks

## InspireClientConfiguration(string, ClientAuthenticationMethods, Uri, Uri, string, string, string, bool, string)
Initializes a new instance of the  class.

### Signature
```csharp
public InspireClientConfiguration(string clientId, ClientAuthenticationMethods clientAuthenticationMethod, Uri authorityUri, Uri resourceUri, string clientSecret = "", string userId = "", string password = "", bool useDiscovery = True, string delegatedAccessToken = "")
```
### Parameters
- `clientId`: Contains the client identity to use for authentication.
- `clientAuthenticationMethod`: Contains an optional client authentication method.
- `authorityUri`: Contains the authority URI that will validate the client credentials and produce access token.
- `resourceUri`: Contains the API resource URI that will be accessed by the client.
- `clientSecret`: Contains an optional client secret key for client credential authentication method.
- `userId`: Contains the user id used for password authentication method.
- `password`: Contains the user password used for password authentication method.
- `useDiscovery`: Contains a value indicating whether the authority discovery endpoint will be used for token endpoint lookup.
- `delegatedAccessToken`: Contains an optional access token passed from the client software to be used in communication with the backchannel created within the SDK library.

### Remarks

