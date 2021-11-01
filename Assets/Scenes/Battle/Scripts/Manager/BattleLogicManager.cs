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
        EventCenter.Instance.AddEventListener<int>("StartAttack", CharacterGetHurt);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CharacterGetHurt(int i)
    {
        GameObject currObj = BattleDataManager.EnemyCharacters[i];
        Animator animator = currObj.GetComponent<Animator>();
        animator.Play("Hurt");
    }

    public static void UpdateMovement(string character, int randomIdx)
    {
        EndPoint = BattleDataManager.EnemyCharactersPostions[randomIdx];
        EndPoint.z = 0;

        for (int i = 0; i < BattleDataManager.FriendlyCharacters.Count; i++)
        {
            if (BattleDataManager.FriendlyCharacters[i].name.Contains(character))
            {
                //EventCenter.Instance.TriggerEventListener<string, int>("RotateCharacter", "F", i);
                BattleDataManager.FriendlyCharacters[i].GetComponent<Character>().Attack(EndPoint);
            }
        }

    }

}
