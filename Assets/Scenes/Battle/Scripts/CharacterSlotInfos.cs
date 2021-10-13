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
    [SerializeField, Min(1)] private int Level;
    [SerializeField, Range(0, 1)] private float Health;
    [SerializeField, Range(0, 1)] private float Mana;

    public string SetCharacter { set { Character = value; UpdateMasks(); } }
    public int SetCharacterIdx { set { CharacterIdx = value; } }

    // Start is called before the first frame update
    void Start()
    {
        this.Health = Random.Range(0f, 1);
        this.Mana = Random.Range(0f, 1);
        this.Level = Random.Range(1, 100);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("Slider/Slider_Health").GetComponent<Slider>().value = this.Health;
        transform.Find("Slider/Slider_Mana").GetComponent<Slider>().value = this.Mana;
        transform.Find("Level/Text_Lv").GetComponent<TMP_Text>().text = this.Level.ToString();
    }

    void UpdateMasks()
    {
        transform.Find("Mask/Character").GetComponent<Image>().sprite =
            Instantiate(Resources.Load($"Masks/{Character}", typeof(Sprite))) as Sprite;
    }

    public void OnclickAttack()
    {
        BattleDataManager.OnAttackClick(CharacterIdx);
        this.GetComponent<Moving>().enabled = true;
    }
}
