# Vasont.Inspire.SDK.Security.RoleExtensions.GetRoles method
## GetRoles(InspireClient, string, string, string)
This method is used to retrieve roles that match the search criteria.

### Signature
```csharp
public static System.Collections.Generic.List<Vasont.Inspire.Models.Security.RoleModel> GetRoles(InspireClient client, string roleName = "", string orderBy = "", string direction = "")
```
### Parameters
- `client`: Contains the  that is used for communication.
- `roleName`: Name of the role.
- `orderBy`: The field to order by the list of roles.
- `direction`: The direction of the order by, ascending or descending.

### Returns
Returns  object if found.
### Remarks

