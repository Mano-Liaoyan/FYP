using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDataManager : MonoBehaviour
{
    public static List<string> FriendlyCharacters;
    public static List<string> EnemyCharacters;

    public static List<Vector3> FriendlyCharactersPostions;
    public static List<Vector3> EnemyCharactersPostions;

    public static int currentIndex;
    public static bool isAnimating;

    public static object[] message = new object[2];

    void Awake()
    {
        FriendlyCharactersPostions = new List<Vector3>();
        EnemyCharactersPostions = new List<Vector3>();
        FriendlyCharacters = new List<string>();
        isAnimating = false;
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
}
