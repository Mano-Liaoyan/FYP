using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaveMatch : MonoBehaviour
{
    public static LeaveMatch Instance { get; private set; }

    public GameObject popupInfo;
    public GameObject popupTextTitle;
    public GameObject popupTextBody;
    public GameObject popupConfirm;
    public GameObject popupCancel;

    void Start()
    {
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
        PopupMessage("Win", "Congradulations!");
        Invoke(nameof(Leave), 2);
    }

    public IEnumerator LooseMatch()
    {
        popupCancel.SetActive(false);
        PopupMessage("Loose", "Keep on going!");
        yield return new WaitForSeconds(2);
        Leave();
    }

    public async void Escape()
    {
        await User.battleSocket.SendMatchStateAsync(BattleDataManager.matchId, 103, "");
        popupCancel.SetActive(false);
        PopupMessage("Loose", "Keep on going!");
        Leave();
    }

}
