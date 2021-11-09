using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoReceiver : MonoBehaviour
{
    private GameObject Character_Info;
    private RectTransform parentRectTransform;
    private Vector2 parentSize;
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

    void ReceiveCharactersMessage(List<string> Characters)
    {
        print("Inside slots info");
        Character_Info = GameObject.Find("Character_Info");
        parentRectTransform = Character_Info.GetComponent<RectTransform>();
        parentSize = parentRectTransform.rect.size;
        characterLength = Characters.Count;
        foreach (string character in Characters)
        {
            GameObject NewSlot = (GameObject)Instantiate(Resources.Load($"Prefab/Slot"), transform.position, Quaternion.identity);
            NewSlot.transform.SetParent(Character_Info.transform, false);
            Vector3 temp = NewSlot.transform.localPosition;
            temp.z = 0;
            NewSlot.transform.localPosition = temp;
            NewSlot.GetComponent<CharacterSlotInfos>().SetCharacter = character;

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
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
