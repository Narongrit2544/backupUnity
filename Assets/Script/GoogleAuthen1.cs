using UnityEngine;
using Google;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class GoogleAuthen1 : MonoBehaviour
{
    public TMP_Text statusText; // TextMeshPro component to display status messages

    private string webClientId = "536241701089-qploj9k3osp4oel3gcu4eq0c9eqi16rm.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestIdToken = true
        };
    }

    public void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        Debug.Log("Attempting to sign in...");
        UpdateStatusText("Attempting to sign in...");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    public void OnSignOut()
    {
        GoogleSignIn.DefaultInstance.SignOut();
        Debug.Log("Signed out.");
        UpdateStatusText("Signed out.");
    }

    void OnAuthenticationFinished(System.Threading.Tasks.Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Login failed: " + task.Exception.Message);
            UpdateStatusText("Login failed: " + task.Exception.Message);
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Login canceled.");
            UpdateStatusText("Login canceled.");
        }
        else
        {
            Debug.Log("Welcome, " + task.Result.DisplayName);
            Debug.Log("User Email: " + task.Result.Email);
            Debug.Log("User ID Token: " + task.Result.IdToken);

            UpdateStatusText("Welcome, " + task.Result.DisplayName);

            // Send data to the server
            StartCoroutine(SendUserDataToServer(task.Result.Email, task.Result.DisplayName, task.Result.IdToken));
        }
    }

    IEnumerator SendUserDataToServer(string email, string name, string idToken)
    {
        string url = "http://localhost:5000/auth/google";

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("name", name);
        form.AddField("idToken", idToken);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to send user data: " + www.error);
                UpdateStatusText("Failed to send data: " + www.error);
            }
            else
            {
                Debug.Log("User data sent successfully: " + www.downloadHandler.text);
                UpdateStatusText("Data sent successfully.");
            }
        }
    }

    void UpdateStatusText(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
        else
        {
            Debug.LogWarning("Status text component is not assigned.");
        }
    }
}
