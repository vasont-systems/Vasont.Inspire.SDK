# Vasont.Inspire.SDK.InspireClient.CreateRequest method
## CreateRequest(string, HttpMethod, bool, ICredentials, string)
This method is used to easily create a new WebRequest object for the Web API.

### Signature
```csharp
public System.Net.HttpWebRequest CreateRequest(string relativeUri, HttpMethod method = null, bool noCache = True, ICredentials credentials = null, string contentType = "application/json")
```
### Parameters
- `relativeUri`: Contains the relative Uri path of the web request to make against the Web API.
- `method`: Contains the HttpMethod request method object.
- `noCache`: Contains a value indicating whether the URL shall contain a parameter preventing the server from returning cached content.
- `credentials`: Contains optional credentials
- `contentType`: Contains optional content type.

### Returns
Returns a new WebRequest object to execute.
### Remarks

## CreateRequest(string, string, bool, ICredentials, string)
This method is used to easily create a new WebRequest object for the Web API.

### Signature
```csharp
public System.Net.HttpWebRequest CreateRequest(string relativeUri, string method, bool noCache = True, ICredentials credentials = null, string contentType = "application/json")
```
### Parameters
- `relativeUri`: Contains the relative Uri path of the web request to make against the Web API.
- `method`: Contains the request method as a string value.
- `noCache`: Contains a value indicating whether the URL shall contain a parameter preventing the server from returning cached content.
- `credentials`: Contains optional credentials
- `contentType`: Contains optional content type.

### Returns
Returns a new HttpWebRequest object to execute.
### Remarks

