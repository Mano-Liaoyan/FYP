using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChracter : MonoBehaviour
{
    private GameObject Character_Enemy;
    private RectTransform parentRectTransform;
    private Vector2 parentSize;
    private int characterLength;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ReceiveCharactersMessage(List<string> Characters)
    {
        Character_Enemy = GameObject.Find("Character_Enemy");
        parentRectTransform = Character_Enemy.GetComponent<RectTransform>();
        parentSize = parentRectTransform.rect.size;
        characterLength = Characters.Count;
        foreach (string character in Characters)
        {
            GameObject NewCharacter = (GameObject)Instantiate(Resources.Load($"Prefab/{character}"), transform.position, Quaternion.identity);
            NewCharacter.transform.SetParent(Character_Enemy.transform, false);
            int index = Characters.IndexOf(character);
            CalcPostion(NewCharacter, index);
        }        
    }

    void CalcPostion(GameObject obj, int index)
    {
        //float x = screenWidth / 4.0f;
        float x = parentSize.x / 4.0f;
        //float y = ((float)(index + 1) / (float)characters.Count) * screenHeight;
        float intervel = 0;
        float y = parentSize.y / (float)characterLength;
        //float deltaY = obj.GetComponent<Renderer>().bounds.size.y;
        switch (index)
        {
            case 0:
                intervel = -1;
                break;
            case 1:
                intervel = 0;
                break;
            case 2:
                intervel = 1;
                break;
        }
        //float y = ((float)(index + 1) / (float)characters.Count) * parentSize.y;
        //print($"x = {x},y = {y}");
        obj.transform.localPosition = new Vector3(x, intervel * (y * 0.4f) - 80, 0);
        obj.transform.localRotation = new Quaternion(0, 180, 0, 0);
        BattleDataManager.EnemyCharactersPostions.Insert(index, obj.transform.localPosition);
        GameObject.Find("GameManager").SendMessage("FindEnemyCharacter");
    }
}
