using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleField : MonoBehaviour
{

    [SerializeField] private List<string> FriendlyCharacters;
    [SerializeField] private List<string> EnemyCharacters;

    // Start is called before the first frame update
    void Start()
    {
        // Get all characters.
        GameObject.Find("Character_Friendly").SendMessage("ReceiveCharactersMessage", FriendlyCharacters);
        GameObject.Find("Character_Enemy").SendMessage("ReceiveCharactersMessage", EnemyCharacters);
        GameObject.Find("Character_Info").SendMessage("ReceiveCharactersMessage", FriendlyCharacters);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RecieveFriendlyPostion()
    {

    }
}
