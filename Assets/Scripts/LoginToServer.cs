using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nakama;
using TMPro;

public class LoginToServer : MonoBehaviour
{
    private const string Scheme = "http";
    private const string Host = "20.94.197.30";
    private const int Port = 7350;
    private const string ServerKey = "defaultkey";

    private const string PrefKeyName = "nakama.session";

    private readonly IClient _client = new Client(Scheme, Host, Port, ServerKey);

    private ISession _session;
    public SceneLoader SceneLoader;
    [SerializeField] private GameObject WarningMessage;
    [SerializeField] private GameObject PopupWindow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Login user.
    /// </summary>
    /// <returns>
    /// None
    /// </returns>
    /// <param name="username">
    /// The username or email
    /// </param>
    /// <param name="pwd">
    /// The user's password
    /// </param>
    public async void Login(string username, string pwd)
    {
        try
        {
            var session = await _client.AuthenticateEmailAsync(username, pwd);
            Debug.Log($"New user: {session.Created}, {session}");
            // If success, switch scene to game view
            SceneLoader.LoadScene("Game View");
        }
        catch (ApiResponseException e)
        { // Pop up a modal that infos user the current error
            print(e);
            PopupWindow.SetActive(true);
            WarningMessage.GetComponent<TMP_Text>().text = e.StatusCode + "\n" + e.Message;
        }


    }
    public void OnClick()
    {
        GameObject inputUsernameObject = GameObject.Find("InputField_ID/TextArea/Text");
        GameObject inputPasswordObject = GameObject.Find("InputField_Pssword/TextArea/Text");
        string inputUsername = inputUsernameObject.GetComponent<TMP_Text>().text;
        string inputPassword = inputPasswordObject.GetComponent<TMP_Text>().text;

        Login(inputUsername, inputPassword);
    }
}
