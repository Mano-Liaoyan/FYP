using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDataManager : MonoBehaviour
{
    public static List<string> FriendlyCharactersName;
    public static List<string> EnemyCharactersName;

    public static List<Vector3> FriendlyCharactersPostions;
    public static List<Vector3> EnemyCharactersPostions;

    public static int currentIndex;
    public static bool isAnimating;

    public static object[] message = new object[2];
    public static List<GameObject> FriendlyCharacters;
    public static List<GameObject> EnemyCharacters;
    public static List<bool> MoveSwitch = new List<bool>();
    public static Vector3 Endpoint;

    void Awake()
    {
        FriendlyCharactersPostions = new List<Vector3>();
        EnemyCharactersPostions = new List<Vector3>();
        FriendlyCharactersName = new List<string>();
        isAnimating = false;
        EventCenter.Instance.AddEventListener("Attack", printAttack);
    }

    public void findAllCharacter()
    {
        GameObject friends = GameObject.Find("Character_Friendly");
        GameObject enemies = GameObject.Find("Character_Enemy");
        FriendlyCharacters = findChild(friends);
        EnemyCharacters = findChild(enemies);
        foreach(GameObject friend in FriendlyCharacters)
        {
            MoveSwitch.Add(false);
        }
    }
    public List<GameObject> findChild(GameObject father)
    {
        List<GameObject> childs = new List<GameObject>();
        foreach(Transform child in father.transform)
        {
            childs.Add(child.gameObject);
        }
        return childs;
    }


    void Update()
    {

    }

    public static void OnAttackClick(int TargetID)
    {        
        currentIndex = TargetID;
        //isAnimating = true;
        //GameObject.Find("Character_Friendly").SendMessage("RecieveSkills", message);

    }

    public void printAttack()
    {
        print("Ataaaaaaaaaaakkkkkkkkkk");
    }
}
