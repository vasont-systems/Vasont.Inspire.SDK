# Vasont.Inspire.SDK.Security.UserExtensions.GetUsers method
## GetUsers(InspireClient, string, string, string, string, string)
This method is used to retrieve information about a specific user.

### Signature
```csharp
public static System.Collections.Generic.List<Vasont.Inspire.Models.Security.UserModel> GetUsers(InspireClient client, string userName = "", string email = "", string phone = "", string orderBy = "UserName", string direction = "ascending")
```
### Parameters
- `client`: Contains the  that is used for communication.
- `userName`: Contains the name of the user.
- `email`: Contains the email of the user.
- `phone`: Contains the phone number of the user.
- `orderBy`: Contains the order by field.
- `direction`: Contains the direction of sort, "ascending" by default.

### Returns
Returns  object if found.
### Remarks

