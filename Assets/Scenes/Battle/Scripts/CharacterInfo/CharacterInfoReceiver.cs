using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoReceiver : MonoBehaviour
{
    private GameObject Character_Info;
    private RectTransform parentRectTransform;
    private int characterLength;

    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.AddEventListener("DisableSlots", DisableSlots);
        EventCenter.Instance.AddEventListener("ActiveSlots", ActiveSlots);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ReceiveCharactersMessage(List<CharacterData> Characters)
    {
        print("Inside slots info");
        Character_Info = GameObject.Find("Character_Info");
        parentRectTransform = Character_Info.GetComponent<RectTransform>();
        characterLength = Characters.Count;
        foreach (CharacterData character in Characters)
        {
            GameObject NewSlot = (GameObject)Instantiate(Resources.Load($"Prefab/Slot"), transform.position, Quaternion.identity);
            NewSlot.transform.SetParent(Character_Info.transform, false);
            Vector3 temp = NewSlot.transform.localPosition;
            temp.z = 0;
            NewSlot.transform.localPosition = temp;
            NewSlot.GetComponent<CharacterSlotInfos>().SetCharacter = character.monster_name;
            NewSlot.GetComponent<CharacterSlotInfos>().InitSlotInfos(character.level);
            int index = Characters.IndexOf(character);
            NewSlot.GetComponent<CharacterSlotInfos>().SetCharacterIdx = index;
        }
    }

    void DisableSlots()
    {
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void ActiveSlots()
    {
        print("INSIDE ACTIVE SLOTS");
        if (checkFriendlyPersistence() && checkEnemyPersistence())
        {
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else if (checkFriendlyPersistence() && !checkEnemyPersistence())
        {
            // Win
            // Send RPC to server
            EventCenter.Instance.TriggerEventListener("WinMatch");
        }
        else if (!checkFriendlyPersistence() && checkEnemyPersistence())
        {
            // Loose
            EventCenter.Instance.TriggerEventListener("LooseMatch");
        }

    }

    /// <summary>
    /// Find if all the friend is still persist
    /// </summary>
    /// <returns>true, if there is at least one friend exist</returns>
    bool checkFriendlyPersistence()
    {
        foreach (var friendly_persistence in BattleDataManager.FriendlyCharacterPersistance)
        {
            if (friendly_persistence)
            {
                return true;
            }
        }

        return false;
    }
    /// <summary>
    /// Find if all the enemy is still persist
    /// </summary>
    /// <returns>true, if there is at least one enemy exist</returns>
    bool checkEnemyPersistence()
    {
        foreach (var enemy_persistence in BattleDataManager.EnemyCharacterPersistance)
        {
            if (enemy_persistence)
            {
                return true;
            }
        }

        return false;
    }

}
