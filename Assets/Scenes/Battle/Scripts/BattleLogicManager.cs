using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicManager : MonoBehaviour
{
    public static Vector3 EndPoint;
    public float speed = 15.0f;
    public int randomIdx = -1;

    private Vector3 OriginalCharacterPosition = new Vector3(-1, -1, -1);
    private bool isBack = false;
    private bool endAttack = false;

    // Start is called before the first frame update  
    void Start()
    {
        EventCenter.Instance.AddEventListener("ExitAttack", EndCharactorAttack);
        EventCenter.Instance.AddEventListener("ExitAttack", RotateCharactor);
        EventCenter.Instance.AddEventListener<int>("StartAttack", CharacterGetHurt);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < BattleDataManager.MoveSwitch.Count; i++)
        {
            if (BattleDataManager.MoveSwitch[i])
            {
                if (!isBack) 
                {
                    MoveCharacter(i);
                    print("start move character");
                }
                else if(isBack && endAttack)
                {
                    MoveCharacterBack(i);
                    print("start move character back");
                }

            }

        }
    }
    public void MoveCharacter(int i)
    {
        GameObject currObj = BattleDataManager.FriendlyCharacters[i];
        Vector3 tmp = currObj.transform.localPosition;
        tmp.z = 0;
        if (randomIdx == -1)
        {
            OriginalCharacterPosition = tmp;
            randomIdx = Random.Range(0, BattleDataManager.EnemyCharactersPostions.Count);
            BattleDataManager.targetIndex = randomIdx;
        }

        EndPoint = BattleDataManager.EnemyCharactersPostions[randomIdx];
        EndPoint.z = 0;
        print("start moving");

        currObj.transform.localPosition = tmp;
        currObj.transform.localPosition = Vector3.MoveTowards(currObj.transform.localPosition, EndPoint, speed * Time.deltaTime);
        if (Vector3.Distance(currObj.transform.localPosition, EndPoint) < 200f)
        {
            BattleDataManager.MoveSwitch[i] = false;
            randomIdx = -1;
            print("End moving");
            ChracterAttack(i);
        }

    }

    public void MoveCharacterBack(int i)
    {
        GameObject currObj = BattleDataManager.FriendlyCharacters[i];
        Vector3 tmp = currObj.transform.localPosition;
        tmp.z = 0;
        currObj.transform.localPosition = tmp;
        currObj.transform.localPosition = Vector3.MoveTowards(currObj.transform.localPosition, OriginalCharacterPosition, speed * Time.deltaTime);
        if (Vector3.Distance(currObj.transform.localPosition, OriginalCharacterPosition) < 1f)
        {
            BattleDataManager.MoveSwitch[i] = false;
            currObj.transform.Rotate(Vector3.up, 180);
            isBack = false;
            endAttack = false;
            EventCenter.Instance.TriggerEventListener("ActiveSlots");
        }

    }

    public void CharacterGetHurt(int i)
    {
        print($"Hurt! {i}");
        GameObject currObj = BattleDataManager.EnemyCharacters[i];
        Animator animator = currObj.GetComponent<Animator>();
        animator.Play("Hurt");
    }

    public void EndCharactorMove(int i)
    {
        isBack = true;
        BattleDataManager.MoveSwitch[i] = true;
    }

    public static void UpdateMovement(string character, int randomIdx)
    {
        EndPoint = BattleDataManager.EnemyCharactersPostions[randomIdx];
        EndPoint.z = 0;
        for (int i = 0; i < BattleDataManager.FriendlyCharacters.Count; i++)
        {
            if (BattleDataManager.FriendlyCharacters[i].name.Contains(character))
            {
                BattleDataManager.MoveSwitch[i] = true;

            }
        }

    }

    public void ChracterAttack(int i)
    {
        GameObject currObj = BattleDataManager.FriendlyCharacters[i];
        Animator animator = currObj.GetComponent<Animator>();
        animator.SetTrigger("LeftMouseClick");
        EndCharactorMove(i);
    }

    public void RotateCharactor()
    {
        GameObject currObj = BattleDataManager.FriendlyCharacters[BattleDataManager.currentIndex];
        currObj.transform.Rotate(Vector3.up, 180);
    }

    public void EndCharactorAttack()
    {
        endAttack = true;
    }

}
