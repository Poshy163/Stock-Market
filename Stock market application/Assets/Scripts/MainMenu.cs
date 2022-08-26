using TMPro;
using UnityEngine;

// ReSharper disable IdentifierTypo


public class MainMenu : MonoBehaviour
{
    [Header("Login")] 
    public TMP_InputField logUsername;
    public TMP_InputField logPincode;

    [Header("Register")] 
    public TMP_InputField refName;
    public TMP_InputField refUsername;
    public TMP_InputField regPincode;

    [Header("Other")] 
    public TMP_Text error;
    public bool needLogin;

    public void Register()
    {
        if (!needLogin)
            NextScene();
        try
        {
            var tName = refName.text.ToUpper();
            var tUsername = refUsername.text.ToUpper();
            var tPincode = regPincode.text.ToUpper();
            MongoDBDatabase.CommitRegister(tName, tUsername, tPincode);
        }
        catch
        {
            ErrorText("Empty/invaid name, username or pin code");
        }
    }


    public void Login()
    {
        if (!needLogin)
            NextScene();
        var tUsername = logUsername.text.ToUpper();
        var tPincode = logPincode.text.ToUpper();
        if (string.IsNullOrWhiteSpace(tUsername) || string.IsNullOrWhiteSpace(tPincode))
        {
            ErrorText("Empty/invaid username or pin code");
            return;
        }
           
        MongoDBDatabase.CheckLogin(tUsername,tPincode);
    }
    private void ErrorText(string displaytext)
    {
        error.text = displaytext;
    }

    private static void NextScene()
    {
        
    }


}