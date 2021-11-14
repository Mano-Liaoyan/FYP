using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaveMatch : MonoBehaviour
{
    public GameObject popupInfo;
    public GameObject popupTextTitle;
    public GameObject popupTextBody;
    public GameObject popupConfirm;
    public GameObject popupCancel;

    void Start()
    {
        EventCenter.Instance.AddEventListener("WinMatch", WinMatch);
        EventCenter.Instance.AddEventListener("LooseMatch", LooseMatch);
        EventCenter.Instance.AddEventListener("LoadPopInfo", LoadPopInfo);
    }

    void OnDestroy()
    {
        Escape();
    }
    public async void Leave()
    {
        await User.battleSocket.LeaveMatchAsync(BattleDataManager.matchId);
        EventCenter.Instance.ClearEventListener();
        SceneLoader.LoadScene("Game View");
    }

    public void ReversePopup()
    {
        popupInfo.SetActive(!popupInfo.activeSelf);
        popupInfo.transform.SetAsLastSibling();
    }

    public void PopupMessage(string title, string body)
    {
        popupTextTitle.GetComponent<TMP_Text>().text = title;
        popupTextBody.GetComponent<TMP_Text>().text = body;
    }

    public async void WinMatch()
    {
        await User.client.RpcAsync(User.session, "matchWin");
        popupCancel.SetActive(false);
        popupConfirm.SetActive(false);
        PopupMessage("Win", "Congradulations!");
        popupInfo.SetActive(true); 
        Invoke("Leave", 2);
    }

    public void LooseMatch()
    {
        popupCancel.SetActive(false);
        popupConfirm.SetActive(false);
        PopupMessage("Loose", "Keep on going!");
        popupInfo.SetActive(true);
        Invoke("Leave", 2);
    }

    public async void Escape()
    {
        await User.battleSocket.SendMatchStateAsync(BattleDataManager.matchId, 103, "");
        popupCancel.SetActive(false);
        PopupMessage("Loose", "Keep on going!");
        popupInfo.SetActive(true);
        Leave();
    }

    public void LoadPopInfo()
    {

    }

}
