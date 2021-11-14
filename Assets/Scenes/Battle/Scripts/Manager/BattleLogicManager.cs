using Nakama;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicManager : MonoBehaviour
{
    public static Vector3 EndPoint;
    public float speed = 15.0f;

    // Start is called before the first frame update  
    void Start()
    {
        User.battleSocket.ReceivedMatchState += ReceiveEnemyChracterInfo;
        User.battleSocket.ReceivedMatchState += ReceiveEscapeSignal;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        User.battleSocket.ReceivedMatchState -= ReceiveEnemyChracterInfo;
        User.battleSocket.ReceivedMatchState -= ReceiveEscapeSignal;

    }


    public static async void UpdateFriendlyMovement(string character, int randomIdx)
    {
        EndPoint = BattleDataManager.EnemyCharactersPostions[randomIdx];
        EndPoint.z = 0;
        GameObject targetObj = BattleDataManager.EnemyCharacters[randomIdx];
        BattleMessage bm = null;
        for (int i = 0; i < BattleDataManager.FriendlyCharacters.Count; i++)
        {
            var currentCharacter = BattleDataManager.FriendlyCharacters[i];
            if (currentCharacter == null) continue;
            if (currentCharacter.name.Contains(character))
            {
                var comp = currentCharacter.GetComponent<Character>();
                bm = new BattleMessage(i, randomIdx, comp.level, comp.buffRate);
                comp.Attack(EndPoint, targetObj, bm.real_damage);
            }
        }
        // Send infos to another client
        long opCode = 102; // 102 means send battle message: A hits B
        string json = JsonUtility.ToJson(bm);
        print($"102 json: {json}");
        await User.battleSocket.SendMatchStateAsync(BattleDataManager.matchId, opCode, json);
    }

    public static IEnumerator UpdateEnemyMovement(int oid, int tid, BattleMessage bm)
    {
        EndPoint = BattleDataManager.FriendlyCharactersPostions[tid];
        EndPoint.z = 0;
        GameObject targetObj = BattleDataManager.FriendlyCharacters[tid];
        UnityMainThreadDispatcher.Instance().Enqueue(() => EventCenter.Instance.TriggerEventListener("DisableSlots"));
        BattleDataManager.EnemyCharacters[oid].GetComponent<Character>().Attack(EndPoint, targetObj, bm.real_damage);
        yield return null;
    }

    public static async void UpdateBuff(int index)
    {
        var character = BattleDataManager.FriendlyCharacters[index].GetComponent<Character>();
        character.BuffSelf();
        var bm = new BattleMessage(index, 0, character.level, character.buffRate);
        // Send infos to another client
        long opCode = 104; // 104 means buff a character
        string json = JsonUtility.ToJson(bm);
        print($"104 json: {json}");
        await User.battleSocket.SendMatchStateAsync(BattleDataManager.matchId, opCode, json);

    }

    public void ReceiveEnemyChracterInfo(IMatchState matchState)
    {
        string messageJson = System.Text.Encoding.UTF8.GetString(matchState.State);
        if (matchState.OpCode == 102) // 102 means send battle message: A hits B
        {
            var msg = JsonUtility.FromJson<BattleMessage>(messageJson);
            print($"msg:{msg.original_id},{msg.target_id},{msg.real_damage}");
            UnityMainThreadDispatcher.Instance().Enqueue(UpdateEnemyMovement(msg.original_id, msg.target_id, msg));
        }
        if (matchState.OpCode == 104) // 104 means buff a character
        {
            var msg = JsonUtility.FromJson<BattleMessage>(messageJson);
            print($"msg:{msg.original_id}");
            UnityMainThreadDispatcher.Instance().Enqueue(() => BattleDataManager.EnemyCharacters[msg.original_id].GetComponent<Character>().BuffSelf());
        }
    }

    public void ReceiveEscapeSignal(IMatchState matchState)
    {
        string messageJson = System.Text.Encoding.UTF8.GetString(matchState.State);
        if (matchState.OpCode == 103) // 103 means your opponent is running away
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => EventCenter.Instance.TriggerEventListener("WinMatch"));
        }
    }

}

public class BattleMessage
{
    public int original_id;
    public int target_id;
    public float real_damage;
    private float default_damage = 300f;

    public BattleMessage(int oid, int tid, int original_level, float buffRate)
    {
        original_id = oid;
        target_id = tid;
        real_damage = default_damage * (1 + buffRate + original_level * 0.1f);
    }
}
