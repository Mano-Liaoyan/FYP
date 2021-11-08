using Nakama;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDataManager : MonoBehaviour
{
    public static List<string> FriendlyCharactersName;
    public static List<string> EnemyCharactersName;

    public static List<Vector3> FriendlyCharactersPostions;
    public static List<Vector3> EnemyCharactersPostions;

    public static int currentIndex;
    public static int targetIndex;
    public static bool isAnimating;

    public static object[] message = new object[2];
    public static List<GameObject> FriendlyCharacters;
    public static List<GameObject> EnemyCharacters;
    public static Vector3 Endpoint;

    public static string matchId;
    public static IMatchmakerMatched matched;

    void Awake()
    {
        FriendlyCharactersPostions = new List<Vector3>();
        EnemyCharactersPostions = new List<Vector3>();
        FriendlyCharactersName = new List<string>();
        isAnimating = false;
        //var match = await User.battleSocket.JoinMatchAsync(matched);
        //matchId = match.Id;
    }

    public void findAllCharacter()
    {
        GameObject friends = GameObject.Find("Character_Friendly");
        GameObject enemies = GameObject.Find("Character_Enemy");
        FriendlyCharacters = findChild(friends);
        EnemyCharacters = findChild(enemies);
        int i = 0, j = 0;
        foreach (GameObject friend in FriendlyCharacters)
        {
            friend.GetComponent<Character>().myIndex = i++;
            friend.GetComponent<Character>().chracterType = "F"; // F means friendly
        }
        foreach (GameObject enemy in EnemyCharacters)
        {
            enemy.GetComponent<Character>().myIndex = j++;
            enemy.GetComponent<Character>().chracterType = "E"; // E means enemy
        }
    }

    public List<GameObject> findChild(GameObject father)
    {
        List<GameObject> childs = new List<GameObject>();
        foreach (Transform child in father.transform)
        {
            childs.Add(child.gameObject);
        }
        return childs;
    }



}
