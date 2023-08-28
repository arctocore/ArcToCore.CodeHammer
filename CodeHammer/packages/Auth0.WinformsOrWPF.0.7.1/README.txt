Usage
=====

var auth0 = new Auth0Client("Your Auth0 Domain e.g.: contoso.auth0.com", 
                            "Your Client ID e.g. IFAaMT8nun....h7oT04EI2Mo");
                            
var windowParent = new WindowWrapper(new WindowInteropHelper(this).Handle);

auth0.LoginAsync(windowParent).ContinueWith(t =>
{
   var profile = t.Result.Profile;
},
TaskScheduler.FromCurrentSynchronizationContext());

Use t.Result to do wonderful things, e.g.: 
- get user email => t.Result.Profile["email"].ToString()
- get facebook/google/twitter/etc access token => t.Result.Profile["identities"][0]["access_token"]
- get Windows Azure AD groups => t.Result.Profile["groups"]
- get a JsonWebToken to flow the identity to a Web API or Service => t.Result.IdToken;
- etc.

