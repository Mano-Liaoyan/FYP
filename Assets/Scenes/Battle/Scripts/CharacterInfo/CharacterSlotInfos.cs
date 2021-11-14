using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CharacterSlotInfos : MonoBehaviour
{
    [SerializeField] private string Character;
    [SerializeField] private int CharacterIdx;
    [SerializeField] private GameObject SkillPanel;
    private int Level;
    [SerializeField] private GameObject BackGlow;
    [SerializeField] private GameObject BottomGlow;
    [SerializeField] private GameObject Frame;

    public string SetCharacter { set { Character = value; UpdateMasks(); } }
    public int SetCharacterIdx { set { CharacterIdx = value; } }

    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.AddEventListener<int>("DisableSlot", DisableSlot);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateMasks()
    {
        transform.Find("Mask/Character").GetComponent<Image>().sprite =
            Instantiate(Resources.Load($"Masks/{Character}", typeof(Sprite))) as Sprite;
    }

    public void OnclickAttack()
    {
        EventCenter.Instance.TriggerEventListener("DisableSlots");
        BattleDataManager.currentIndex = CharacterIdx;
        int target_index = -1;
        while (true)
        {
            target_index = Random.Range(0, BattleDataManager.EnemyCharactersPostions.Count);
            if (!BattleDataManager.EnemyCharacterPersistance[target_index])// if the enemy does not exist
                target_index = -1;
            if (target_index != -1) break;
        }
        BattleLogicManager.UpdateFriendlyMovement(Character, target_index);
    }

    public void OnclickBuff()
    {
        EventCenter.Instance.TriggerEventListener("DisableSlots");
        BattleLogicManager.UpdateBuff(CharacterIdx);
    }

    public void InitSlotInfos(int level)
    {
        this.Level = level;
        transform.Find("Level/Text_Lv").GetComponent<TMP_Text>().text = this.Level.ToString();
    }

    public void DisableSlot(int idx)
    {
        print($"Inside Disable Slot {idx}");
        if (CharacterIdx == idx)
        {
            gameObject.GetComponent<Image>().color = Color.gray;
            BackGlow.GetComponent<Image>().color = Color.gray;
            BottomGlow.GetComponent<Image>().color = Color.gray;
            Frame.GetComponent<Image>().color = Color.gray;
            gameObject.GetComponent<Button>().interactable = false;
            //gameObject.GetComponent<Button>().enabled = false;
        }
    }

    public void ReverseSkillPanelVisible()
    {
        SkillPanel.SetActive(!SkillPanel.activeSelf);
    }


}
