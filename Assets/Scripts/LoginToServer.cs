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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void Login(string username, string pwd) {
        // Debug.Log($"username: {username.text},password: {password.text}");
        // string email = "1234@mail.com";
        // string pwd = "password";
        try {
            var session = await _client.AuthenticateEmailAsync(username, pwd);
            Debug.Log($"New user: {session.Created}, {session}");
        } catch(ApiResponseException e){
            print(e);
        }

        
    }
    public void OnClick(){
        GameObject inputUsernameObject = GameObject.Find("InputField_ID/TextArea/Text");
        GameObject inputPasswordObject = GameObject.Find("InputField_Pssword/TextArea/Text");
        string inputUsername = inputUsernameObject.GetComponent<TMP_Text>().text;
        string inputPassword = inputPasswordObject.GetComponent<TMP_Text>().text;

        Login(inputUsername, inputPassword);
        print(inputUsername);
    }
}
