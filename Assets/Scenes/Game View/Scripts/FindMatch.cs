using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using TMPro;

public class FindMatch : MonoBehaviour
{
    [SerializeField] private GameObject InfoMessage;
    [SerializeField] private GameObject PopupWindow;
    [SerializeField] private GameObject SpiningImage;

    private IMatchmakerTicket ticket;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void Match()
    {
        PopupWindow.SetActive(true);
        SpiningImage.SetActive(true);
        User.battleSocket = User.client.NewSocket();
        User.battleSocket.ReceivedMatchmakerMatched += OnMatchmakerMatched;// Register callback function
        await User.battleSocket.ConnectAsync(User.session, true, 30);
        print($"Socket Connected!");        
        ticket = await User.battleSocket.AddMatchmakerAsync("*", 2, 2, null, null);
        print($"Got ticket!");
        print(ticket);
    }

    public async void CancelMatch()
    {
        await User.battleSocket.RemoveMatchmakerAsync(ticket);
        PopupWindow.SetActive(false);
    }

    public void OnMatchmakerMatched(IMatchmakerMatched matched)
    {
        print($"Matched!");
        User.battleSocket.ReceivedMatchmakerMatched -= OnMatchmakerMatched; // Unregister callback function
        print("Loadinddddddddddd");
        UnityMainThreadDispatcher.Instance().Enqueue(Load(matched));
    }


    public IEnumerator Load(IMatchmakerMatched matched)
    {
        print("Inside loader");

        BattleDataManager.matched = matched;
        SceneLoader.LoadSceneAsync("Battle"); // Switch scene to battle scene
        yield return null;
    }

}
