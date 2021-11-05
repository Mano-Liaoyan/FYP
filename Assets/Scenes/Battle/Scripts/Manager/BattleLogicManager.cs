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
        //EventCenter.Instance.AddEventListener<int>("StartAttack", CharacterGetHurt);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void UpdateFriendlyMovement(string character, int randomIdx)
    {
        EndPoint = BattleDataManager.EnemyCharactersPostions[randomIdx];
        EndPoint.z = 0;
        GameObject targetObj = BattleDataManager.EnemyCharacters[randomIdx];

        for (int i = 0; i < BattleDataManager.FriendlyCharacters.Count; i++)
        {
            if (BattleDataManager.FriendlyCharacters[i].name.Contains(character))
            {
                BattleDataManager.FriendlyCharacters[i].GetComponent<Character>().Attack(EndPoint, targetObj);
            }
        }

    }

}
