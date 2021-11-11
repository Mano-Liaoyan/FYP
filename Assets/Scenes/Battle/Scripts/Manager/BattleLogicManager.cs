using Nakama;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicManager : MonoBehaviour
{
    public static Vector3 EndPoint;
    public float speed = 15.0f;

    private Vector3 OriginalCharacterPosition = new Vector3(-1, -1, -1);

    // Start is called before the first frame update  
    void Start()
    {
        User.battleSocket.ReceivedMatchState += ReceiveEnemyChracterInfo;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        User.battleSocket.ReceivedMatchState -= ReceiveEnemyChracterInfo;
    }


    public static async void UpdateFriendlyMovement(string character, int randomIdx)
    {
        EndPoint = BattleDataManager.EnemyCharactersPostions[randomIdx];
        EndPoint.z = 0;
        GameObject targetObj = BattleDataManager.EnemyCharacters[randomIdx];
        BattleMessage bm = null;
        for (int i = 0; i < BattleDataManager.FriendlyCharacters.Count; i++)
        {
            if (BattleDataManager.FriendlyCharacters[i].name.Contains(character))
            {
                BattleDataManager.FriendlyCharacters[i].GetComponent<Character>().Attack(EndPoint, targetObj);
                bm = new BattleMessage(i, randomIdx, BattleDataManager.FriendlyCharacters[i].GetComponent<Character>().level, 0);
            }
        }
        // Send infos to another client
        long opCode = 102; // 102 means send battle message: A hits B
        string json = JsonUtility.ToJson(bm);
        print($"102 json: {json}");
        await User.battleSocket.SendMatchStateAsync(BattleDataManager.matchId, opCode, json);
    }

    public static IEnumerator UpdateEnemyMovement(int oid, int tid)
    {
        EndPoint = BattleDataManager.FriendlyCharactersPostions[tid];
        EndPoint.z = 0;
        GameObject targetObj = BattleDataManager.FriendlyCharacters[tid];
        BattleDataManager.EnemyCharacters[oid].GetComponent<Character>().Attack(EndPoint, targetObj);
        yield return null;
    }

    public void ReceiveEnemyChracterInfo(IMatchState matchState)
    {
        string messageJson = System.Text.Encoding.UTF8.GetString(matchState.State);
        if (matchState.OpCode == 102) // 102 means send battle message: A hits B
        {
            var msg = JsonUtility.FromJson<BattleMessage>(messageJson);
            print($"msg:{msg.original_id},{msg.target_id}");
            UnityMainThreadDispatcher.Instance().Enqueue(UpdateEnemyMovement(msg.original_id, msg.target_id));
        }
    }

}

public class BattleMessage
{
    public int original_id;
    public int target_id;
    public float real_damage;
    private float default_damage = 100f;

    public BattleMessage(int oid, int tid, int original_level, float buffRate)
    {
        original_id = oid;
        target_id = tid;
        real_damage = default_damage * (1 + buffRate + original_level * 0.1f);
    }
}
