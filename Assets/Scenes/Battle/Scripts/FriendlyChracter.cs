using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyChracter : MonoBehaviour
{
    private GameObject Character_Friendly;
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
        if(BattleDataManager.isAnimating == true)
        {

        }
    }

    void ReceiveCharactersMessage(List<string> Characters)
    {
        Character_Friendly = GameObject.Find("Character_Friendly");
        parentRectTransform = Character_Friendly.GetComponent<RectTransform>();
        parentSize = parentRectTransform.rect.size;
        characterLength = Characters.Count;
        foreach (string character in Characters)
        {            
            GameObject NewCharacter = (GameObject)Instantiate(Resources.Load($"Prefab/{character}"), transform.position, Quaternion.identity);
            // Set worldPostionStays to false
            // See https://blog.csdn.net/qq_42672770/article/details/109180796
            NewCharacter.transform.SetParent(Character_Friendly.transform, false);
            int index = Characters.IndexOf(character);
            BattleDataManager.FriendlyCharacters.Insert(index, character);
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
        obj.transform.localPosition = new Vector3(-x, intervel * (y * 0.4f) - 80, 0);
        BattleDataManager.FriendlyCharactersPostions.Insert(index, obj.transform.localPosition);
    }

    void RecieveSkills(object[] message)
    {
        int characterIndex = (int)message[0], randomIdx = (int)message[1];
        Vector3 myPosition = BattleDataManager.FriendlyCharactersPostions[characterIndex];
        print($"v31 {myPosition}");
        Vector3 targetPosition = BattleDataManager.EnemyCharactersPostions[randomIdx];
        print($"v32 {targetPosition}");
        BattleDataManager.FriendlyCharactersPostions[characterIndex] = Vector3.MoveTowards(myPosition, targetPosition, 10 * Time.deltaTime);
        if (Vector3.Distance(myPosition,targetPosition ) < 0.001f)
        {
            BattleDataManager.isAnimating = false;
        }
    }
}
