using UnityEngine;

public class GoogleAuthen : MonoBehaviour
{
    private string clientId = "536241701089-ej2lkeskgljs17a9dp6d3eeorfhb2f2e.apps.googleusercontent.com";
    private string redirectUri = "http://localhost:5000";

    public void OpenGoogleLogin()
    {
        string googleAuthUrl = "https://accounts.google.com/o/oauth2/v2/auth" +
            "?client_id=" + clientId +
            "&redirect_uri=" + redirectUri +
            "&response_type=token" +
            "&scope=email%20profile";

        Application.OpenURL(googleAuthUrl);
    }
}
