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
    private List<CharacterData> FriendlyCharacters;
    /*[SerializeField] */
    private List<CharacterData> EnemyCharacters;

    // Start is called before the first frame update
    async void OnEnable()
    {
        print($"Inside OnEnable!, User.battleSocket status:{User.battleSocket}");
        User.battleSocket.ReceivedMatchState += ReceiveEnemyChracterInfo;
        FriendlyCharacters = new List<CharacterData>();
        EnemyCharacters = new List<CharacterData>();
        await FetchUserCharactersAsync();
    }
    public async Task FetchUserCharactersAsync()
    {
        var res = await User.client.RpcAsync(User.session, "getCharacter");
        CharacterJson cj = JsonUtility.FromJson<CharacterJson>(res.Payload);
        var character_list = cj.characters;
        print($"res character_list: {character_list}");
        foreach (var character in character_list)
        {
            print($"res character: {character.monster_name},{character.level},{character.health}");
            if (character.level != -1)
            {
                FriendlyCharacters.Add(character);
            }
        }
        // Send infos to another client
        long opCode = 101; // 101 means send my player character info
        string json = JsonUtility.ToJson(cj);
        await Task.Delay(TimeSpan.FromSeconds(3));
        await User.battleSocket.SendMatchStateAsync(BattleDataManager.matchId, opCode, json);
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
        print("Received Enemy Info from server");
        User.battleSocket.ReceivedMatchState -= ReceiveEnemyChracterInfo;
        string messageJson = System.Text.Encoding.UTF8.GetString(matchState.State);
        if (matchState.OpCode == 101)
        {
            var character_list = JsonUtility.FromJson<CharacterJson>(messageJson).characters;
            foreach (var character in character_list)
            {
                print($"enemy character: {character.monster_name},{character.level},{character.health}");
                if (character.level != -1)
                {
                    EnemyCharacters.Add(character);
                }
            }
        }
        UnityMainThreadDispatcher.Instance().Enqueue(LoadCharacters());
    }

    public IEnumerator LoadCharacters()
    {
        print("Inside Load Characters");
        GameObject.Find("Character_Friendly").SendMessage("ReceiveCharactersMessage", FriendlyCharacters);
        GameObject.Find("Character_Enemy").SendMessage("ReceiveCharactersMessage", EnemyCharacters);
        GameObject.Find("Character_Info").SendMessage("ReceiveCharactersMessage", FriendlyCharacters);
        yield return new WaitForSeconds(1);
        UnityMainThreadDispatcher.Instance().Enqueue(() => EventCenter.Instance.TriggerEventListener("LoadPopInfo"));
        yield return null;
    }
}
[Serializable]
public class CharacterData
{
    public string monster_name;
    public int level;
    public float health;
}

[Serializable]
public class CharacterJson
{
    public List<CharacterData> characters;
}

