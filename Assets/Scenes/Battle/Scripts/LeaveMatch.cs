using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveMatch : MonoBehaviour
{
    public async void Leave()
    {
        await User.battleSocket.LeaveMatchAsync(BattleDataManager.matchId);
        EventCenter.Instance.ClearEventListener();
        SceneLoader.LoadScene("Game View");
    }
}
