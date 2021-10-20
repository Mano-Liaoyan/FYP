using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is class is used to receive the matching teams information and broadcast them into the sub-components
/// </summary>
public class CharacterInfo : MonoBehaviour{
    
    [SerializeField] private List<string> FriendlyCharacters;
    [SerializeField] private List<string> EnemyCharacters;

    // Start is called before the first frame update
    void Start()
    {
        //FriendlyCharacters = BattleDataManager.FriendlyCharacters;
        //EnemyCharacters = BattleDataManager.EnemyCharacters;
        GameObject.Find("Character_Friendly").SendMessage("ReceiveCharactersMessage", FriendlyCharacters);
        GameObject.Find("Character_Enemy").SendMessage("ReceiveCharactersMessage", EnemyCharacters);
        GameObject.Find("Character_Info").SendMessage("ReceiveCharactersMessage", FriendlyCharacters);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}