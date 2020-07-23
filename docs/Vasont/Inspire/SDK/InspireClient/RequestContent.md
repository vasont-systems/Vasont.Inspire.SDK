# Vasont.Inspire.SDK.InspireClient.RequestContent method
## RequestContent<TOut>(HttpWebRequest)
### Signature
```csharp
public TOut RequestContent<TOut>(HttpWebRequest request)
```
## RequestContent(HttpWebRequest)
This method is used to execute a web request and return the results of the request as a string.

### Signature
```csharp
public string RequestContent(HttpWebRequest request)
```
### Parameters
- `request`: Contains the HttpWebRequest to execute.

### Returns
Returns the content of the request response.
### Remarks

## RequestContent<T, TOut>(HttpWebRequest, T)
### Signature
```csharp
public TOut RequestContent<T, TOut>(HttpWebRequest request, T requestBodyModel)
```
## RequestContent<T>(HttpWebRequest, T)
### Signature
```csharp
public string RequestContent<T>(HttpWebRequest request, T requestBodyModel)
```
