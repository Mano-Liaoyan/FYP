using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nakama;
using TMPro;
using System;

public class LoginToServer : MonoBehaviour {
    private const string Scheme = "http";
    private const string Host = "81.71.41.8";
    private const int Port = 7350;
    private const string ServerKey = "defaultkey";

    private const string PrefKeyName = "nakama.session";

    private readonly Client _client = new Client(Scheme, Host, Port, ServerKey);

    [SerializeField] private GameObject WarningMessage;
    [SerializeField] private GameObject PopupWindow;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

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
    public async void Login(string username, string pwd) {
        try {
            User.session = await _client.AuthenticateEmailAsync(username, pwd);
            await _client.UpdateAccountAsync(User.session, username);
            User.session = await _client.AuthenticateEmailAsync(username, pwd);
            User.client = _client;
            Debug.Log($"New user: {User.session.Created}, {User.session}");
            // If success, switch scene to game view
            var my_rank = await User.client.ListLeaderboardRecordsAroundOwnerAsync(User.session, "score", User.session.UserId, null);

            foreach (var r in my_rank.Records) {
                User.score = Convert.ToSingle(r.Score);
                Debug.LogFormat("My score: Record for '{0}' score '{1}'", r.Username, r.Score);
            }

            if (User.score == 0) {
                User.score = 1000;
            }
            print(User.score);
            SceneLoader.LoadScene("Game View");
        } catch (ApiResponseException e) { // Pop up a modal that infos user the current error
            print(e);
            PopupWindow.SetActive(true);
            WarningMessage.GetComponent<TMP_Text>().text = e.StatusCode + "\n" + e.Message;
        }
    }
    public void OnClick() {
        GameObject inputUsernameObject = GameObject.Find("InputField_ID/TextArea/Text");
        GameObject inputPasswordObject = GameObject.Find("InputField_Pssword/TextArea/Text");
        string inputUsername = inputUsernameObject.GetComponent<TMP_Text>().text;
        string inputPassword = inputPasswordObject.GetComponent<TMP_Text>().text;

        Login(inputUsername, inputPassword);
    }
}
