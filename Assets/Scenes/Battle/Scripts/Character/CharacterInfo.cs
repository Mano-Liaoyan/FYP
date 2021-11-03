using Nakama;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This is class is used to receive the matching teams information and broadcast them into the sub-components
/// </summary>
public class CharacterInfo : MonoBehaviour {

    [SerializeField] private List<string> FriendlyCharacters;
    [SerializeField] private List<string> EnemyCharacters;

    // Start is called before the first frame update
    void Start() {
        //FriendlyCharacters = BattleDataManager.FriendlyCharacters;
        //EnemyCharacters = BattleDataManager.EnemyCharacters;
        GameObject.Find("Character_Friendly").SendMessage("ReceiveCharactersMessage", FriendlyCharacters);
        GameObject.Find("Character_Enemy").SendMessage("ReceiveCharactersMessage", EnemyCharacters);
        GameObject.Find("Character_Info").SendMessage("ReceiveCharactersMessage", FriendlyCharacters);

        // IApiReadStorageObjectId readObjectId = new StorageObjectId {
        //     Collection = "Unlocks",
        //     Key = "Hats",
        //     UserId = User.session.UserId
        //  };


        //var result = await User.client.ReadStorageObjectsAsync(User.session, readObjectId);
        _ = testAsync();
    }

    public async Task testAsync() {
        var readObject = new[] {
            new StorageObjectId {
                Collection = "user_info",
                Key = "characters",
                UserId = User.session.UserId
            }
        };
        IApiStorageObjects result = await User.client.ReadStorageObjectsAsync(User.session, readObject);
        if (result.Objects.Any()) {
            var storageObject = result.Objects.First();
            var res = JsonConvert.DeserializeObject<CharacterJson>(storageObject.Value);
            print(res.characters);
            int i = 0;
            foreach (var character in res.characters)
                print($"Character {i++}: name:{character.monster_name},level:{character.level}");
        }
    }



    // Update is called once per frame
    void Update() {

    }
}
public class CharacterData {
    public string monster_name { get; set; }
    public int level { get; set; }
}
public class CharacterJson {
    public IList<CharacterData> characters { get; set; }
}