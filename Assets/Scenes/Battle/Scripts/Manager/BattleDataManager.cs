using Nakama;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDataManager : MonoBehaviour
{
    public static List<bool> FriendlyCharacterPersistance;
    public static List<bool> EnemyCharacterPersistance;

    public static List<Vector3> FriendlyCharactersPostions;
    public static List<Vector3> EnemyCharactersPostions;

    public static int currentIndex;
    public static int targetIndex;

    public static List<GameObject> FriendlyCharacters;
    public static List<GameObject> EnemyCharacters;
    public static Vector3 Endpoint;

    public static string matchId;
    public static IMatchmakerMatched matched;

    public GameObject Play_Type;

    private Sprite HealthBarGreen;
    private Sprite HealthBarRed;

    async void OnEnable()
    {
        FriendlyCharactersPostions = new List<Vector3>();
        EnemyCharactersPostions = new List<Vector3>();
        FriendlyCharacterPersistance = new List<bool>();
        EnemyCharacterPersistance = new List<bool>();
        var match = await User.battleSocket.JoinMatchAsync(matched);
        matchId = match.Id;
        print($"matchID: {matchId}");
        Play_Type.SetActive(true);
    }

    void Start()
    {
        HealthBarGreen = Resources.Load<Sprite>("Images/HealthBarGreen");
        HealthBarRed = Resources.Load<Sprite>("Images/HealthBarRed");
        EventCenter.Instance.AddEventListener<List<CharacterData>>("FindEnemyCharacter", FindEnemyCharacter);
        EventCenter.Instance.AddEventListener<List<CharacterData>>("FindFriendlyCharacter", FindFriendlyCharacter);
    }

    public void FindFriendlyCharacter(List<CharacterData> Characters)
    {
        GameObject friends = GameObject.Find("Character_Friendly");
        FriendlyCharacters = FindChild(friends);
        int i = 0;
        foreach (GameObject friend in FriendlyCharacters)
        {
            friend.GetComponent<Character>().level = Characters[i].level;
            friend.GetComponent<Character>().health = Characters[i].health;
            friend.GetComponent<Character>().myIndex = i++;
            friend.GetComponent<Character>().characterType = "F"; // F means friendly
            friend.GetComponent<Character>().SetHealthBar(HealthBarGreen);
        }
    }

    public void FindEnemyCharacter(List<CharacterData> Characters)
    {
        GameObject enemies = GameObject.Find("Character_Enemy");
        EnemyCharacters = FindChild(enemies);
        int j = 0;
        foreach (GameObject enemy in EnemyCharacters)
        {
            enemy.GetComponent<Character>().level = Characters[j].level;
            enemy.GetComponent<Character>().health = Characters[j].health;
            enemy.GetComponent<Character>().myIndex = j++;
            enemy.GetComponent<Character>().characterType = "E"; // E means enemy
            enemy.GetComponent<Character>().SetHealthBar(HealthBarRed);
        }
    }

    public List<GameObject> FindChild(GameObject father)
    {
        List<GameObject> childs = new List<GameObject>();
        foreach (Transform child in father.transform)
        {
            childs.Add(child.gameObject);
        }
        return childs;
    }



}
