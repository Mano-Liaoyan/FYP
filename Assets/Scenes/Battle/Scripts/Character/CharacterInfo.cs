using Nakama;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This is class is used to receive the matching teams information and broadcast them into the sub-components
/// </summary>
public class CharacterInfo : MonoBehaviour
{
    private List<string> FriendlyCharacters;
    [SerializeField] private List<string> EnemyCharacters;

    // Start is called before the first frame update
    async void Start()
    {
        FriendlyCharacters = new List<string>();
        //EnemyCharacters = BattleDataManager.EnemyCharacters;
        await FetchUserCharactersAsync();
        GameObject.Find("Character_Friendly").SendMessage("ReceiveCharactersMessage", FriendlyCharacters);
        GameObject.Find("Character_Enemy").SendMessage("ReceiveCharactersMessage", EnemyCharacters);
        GameObject.Find("Character_Info").SendMessage("ReceiveCharactersMessage", FriendlyCharacters);


    }
    public async Task FetchUserCharactersAsync()
    {
        try
        {
            var res = await User.client.RpcAsync(User.session, "getCharacter");
            print($"rps res: {res.Payload}");
            var character_list = JsonConvert.DeserializeObject<List<CharacterData>>(res.Payload);
            foreach (var character in character_list)
            {
                if (character.level != -1)
                {
                    FriendlyCharacters.Add(character.monster_name);
                }
            }
        }
        catch (ApiResponseException ex)
        {
            Debug.LogFormat("Error: {0}", ex.Message);
        }

        /*
        var readObject = new[] {
            new StorageObjectId {
                Collection = "user_info",
                Key = "characters",
                UserId = User.session.UserId
            }
        };
        IApiStorageObjects result = await User.client.ReadStorageObjectsAsync(User.session, readObject);
        if (result.Objects.Any())
        {
            var storageObject = result.Objects.First();
            print($"Json: {storageObject.Value}");
            var res = JsonConvert.DeserializeObject<CharacterJson>(storageObject.Value);
            print(res.characters);
            int i = 0;
            foreach (var character in res.characters)
            {
                print($"Character {i++}: name:{character.monster_name},level:{character.level}");
                if (character.level != -1)
                {
                    FriendlyCharacters.Add(character.monster_name);
                }
            }

        }*/
    }



    // Update is called once per frame
    void Update()
    {

    }
}
public class CharacterData
{
    public string monster_name { get; set; }
    public int level { get; set; }
}
public class CharacterJson
{
    public IList<CharacterData> characters { get; set; }
}