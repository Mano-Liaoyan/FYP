using Nakama;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This is class is used to receive the matching teams information and broadcast them into the sub-components
/// </summary>
public class CharacterInfo : MonoBehaviour
{
    private List<string> FriendlyCharacters;
    /*[SerializeField] */
    private List<string> EnemyCharacters;

    // Start is called before the first frame update
    async void Start()
    {
        User.battleSocket.ReceivedMatchState += ReceiveEnemyChracterInfo;
        FriendlyCharacters = new List<string>();
        EnemyCharacters = new List<string>();
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
            var character_list = JsonUtility.FromJson<CharacterJson>(res.Payload).characters;
            foreach (var character in character_list)
            {
                if (character.level != -1)
                {
                    FriendlyCharacters.Add(character.monster_name);
                }
            }
            try
            {
                long opCode = 101; // 101 means send my player character info
                string json = JsonUtility.ToJson(res.Payload);
                print($"json: {json}");
                await User.battleSocket.SendMatchStateAsync(BattleDataManager.matchId, opCode, json);
            }
            catch (ApiResponseException ex) { Debug.LogFormat("Error: {0}", ex.Message); }
        }
        catch (ApiResponseException ex) { Debug.LogFormat("Error: {0}", ex.Message); }

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

    public void ReceiveEnemyChracterInfo(IMatchState matchState)
    {
        string messageJson = System.Text.Encoding.UTF8.GetString(matchState.State);
        print($"messagejson: {messageJson}");
        if (matchState.OpCode == 101)
        {
            var character_list = JsonUtility.FromJson<CharacterJson>(messageJson).characters;
            foreach (var character in character_list)
            {
                if (character.level != -1)
                {
                    EnemyCharacters.Add(character.monster_name);
                }
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
[Serializable]
public class CharacterData
{
    public string monster_name;
    public int level;
}

[Serializable]
public class CharacterJson
{
    public List<CharacterData> characters;
}

