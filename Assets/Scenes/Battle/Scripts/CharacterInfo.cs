using Nakama;
using Nakama.TinyJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        //print(result);
        //print(result.Objects.First());
        //var storageObject = result.Objects.First();

        //print(storageObject.Value.ToCharArray());
        //var tmp = storageObject.Value.ToJson();
        //print(tmp);
        if (result.Objects.Any()) {
            var storageObject = result.Objects.First();
            print(storageObject);
            print(storageObject.Value);
            try {
                var character123 = JsonParser.FromJson<character>(storageObject.Value);
                print(character123);
                print(character123.characters);
                print(character123.characters.ToString());
                Debug.LogFormat("Title: {0}", character123.characters);
            } catch (Exception e) {
                print(e.Data);
                print(e.Message);
            }

        }



        //var res = Mapbox.Json.Linq.JObject.Parse(tmp);

        //print(storageObject.Value.ToString());
        //print(storageObject.Value.ToString();

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
public class characters {
    public string name;
    public int level;
}
public class character {
    public string characters;
}