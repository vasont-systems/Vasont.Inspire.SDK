# Vasont.Inspire.SDK.Review.ReviewExtensions.UpdateReviewComponentState method
## UpdateReviewComponentState(InspireClient, long, long, bool)
This REST method is used to update a review component states.

### Signature
```csharp
public static Vasont.Inspire.Models.Reviews.ReviewerComponentModel UpdateReviewComponentState(InspireClient client, long reviewId, long reviewComponentId, bool completed = True)
```
### Parameters
- `client`: used to communication with the API endpoint.
- `reviewId`: Contains the identity of the review to update.
- `reviewComponentId`: Contains the identity of the review component.
- `completed`: Contains a value indicating whether the review component should be completed.

### Returns
Returns a list of  objects.
### Remarks

