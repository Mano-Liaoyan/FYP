using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaveMatch : MonoBehaviour
{

    private PopInfo popInfo;

    void Start()
    {
        EventCenter.Instance.AddEventListener("WinMatch", WinMatch);
        EventCenter.Instance.AddEventListener("LooseMatch", LooseMatch);
        EventCenter.Instance.AddEventListener("Escape", Escape);
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



    public async void WinMatch()
    {
        await User.client.RpcAsync(User.session, "matchWin");
        popInfo.popupCancel.SetActive(false);
        popInfo.popupConfirm.SetActive(false);
        popInfo.PopupMessage("Win", "Congradulations!");
        popInfo.gameObject.SetActive(true);
        Invoke("Leave", 2);
    }

    public void LooseMatch()
    {
        popInfo.popupCancel.SetActive(false);
        popInfo.popupConfirm.SetActive(false);
        popInfo.PopupMessage("Loose", "Keep on going!");
        popInfo.gameObject.SetActive(true);

        Invoke("Leave", 2);
    }

    public async void Escape()
    {
        await User.battleSocket.SendMatchStateAsync(BattleDataManager.matchId, 103, "");
        popInfo.popupCancel.SetActive(false);
        popInfo.PopupMessage("Loose", "Keep on going!");
        popInfo.gameObject.SetActive(true);
        Invoke("Leave", 2);
    }

    public void LoadPopInfo()
    {
        var obj = (GameObject)Instantiate(Resources.Load($"Prefab/Popup_Info"), transform.parent.position, Quaternion.identity, transform.parent);
        popInfo = obj.GetComponent<PopInfo>();
    }

    public void ReversePopInfo()
    {
        popInfo.ReversePopup();
    }

}
