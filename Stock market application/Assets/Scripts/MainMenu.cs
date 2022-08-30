using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable IdentifierTypo


public class MainMenu : MonoBehaviour
{
    [Header("Login")] public TMP_InputField logUsername;

    public TMP_InputField logPincode;

    [Header("Register")] public TMP_InputField refName;

    public TMP_InputField refUsername;
    public TMP_InputField regPincode;

    [Header("Other")] public TMP_Text error;

    public void Register()
    {
        try
        {
            var tName = refName.text;
            var tUsername = refUsername.text;
            var tPincode = regPincode.text;
            if (string.IsNullOrWhiteSpace(tName) || string.IsNullOrWhiteSpace(tUsername) ||
                string.IsNullOrWhiteSpace(tPincode))
            {
                ErrorText("Empty/invaid username or pin code");
                return;
            }

            if (!MongoDBDatabase.CommitRegister(tName, tUsername, tPincode))
                ErrorText("Username/Name already exists");
            else
              NextScene(1);
        }
        catch
        {
            ErrorText("Empty/invaid name, username or pin code");
        }
    }


    public void Login()
    {
        var tUsername = logUsername.text;
        var tPincode = logPincode.text;
        if (string.IsNullOrWhiteSpace(tUsername) || string.IsNullOrWhiteSpace(tPincode))
        {
            ErrorText("Empty/invaid username or pin code");
            return;
        }

        if (MongoDBDatabase.CheckLogin(tUsername, tPincode))
            NextScene(1);
        else
            ErrorText("Wrong Username/Password");
    }

    private void ErrorText(string displaytext)
    {
        error.text = displaytext;
    }

    private static void NextScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}