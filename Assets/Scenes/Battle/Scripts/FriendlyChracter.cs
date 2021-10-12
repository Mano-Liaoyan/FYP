using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyChracter : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters;
    private GameObject Character_Friendly;
    private RectTransform parentRectTransform;
    private float screenWidth;
    private float screenHeight;
    private Vector2 parentSize;

    // Start is called before the first frame update
    void Start()
    {
        Character_Friendly = GameObject.Find("Character_Friendly");
        parentRectTransform = Character_Friendly.GetComponent<RectTransform>();
        parentSize = parentRectTransform.rect.size;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        foreach (GameObject character in characters)
        {
            // Set worldPostionStays to false
            // See https://blog.csdn.net/qq_42672770/article/details/109180796
            GameObject NewCharacter = Instantiate(character, transform.position, Quaternion.identity);
            NewCharacter.transform.SetParent(Character_Friendly.transform, false);
            int index = characters.IndexOf(character);
            CalcPostion(NewCharacter, index);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CalcPostion(GameObject obj, int index)
    {
        //print($"index: {index} size: {parentSize}");
        //float x = screenWidth / 4.0f;
        float x = parentSize.x / 4.0f;
        //float y = ((float)(index + 1) / (float)characters.Count) * screenHeight;
        float y = ((float)(index + 1) / (float)characters.Count) * parentSize.y;
        print($"x = {x},y = {y}");
        //RectTransform objRect = obj.GetComponent<Transform>() as RectTransform;
        //objRect.sizeDelta = new Vector3(x, y);
        obj.transform.localPosition = new Vector3(0, y - parentSize.y, 0);
    }
}
