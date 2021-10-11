using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyChracter : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Character_Friendly = GameObject.Find("Character_Friendly");
        foreach (GameObject character in characters)
        {
            // Set worldPostionStays to false
            // See https://blog.csdn.net/qq_42672770/article/details/109180796
            GameObject NewCharacter = Instantiate(character, transform.position, Quaternion.identity);
            NewCharacter.transform.SetParent(Character_Friendly.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CalcPostion()
    {

    }
}
