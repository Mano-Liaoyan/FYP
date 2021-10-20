using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public Vector3 EndPoint;
    public static float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        int randomIdx = Random.Range(0, BattleDataManager.EnemyCharactersPostions.Count);
        //EndPoint = BattleDataManager.EnemyCharactersPostions[randomIdx];
        //EndPoint.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void moveMe(Vector3 EndPoint)
    {
        GameObject thisObject = GameObject.Find("Crab");

        thisObject.transform.position = Vector3.MoveTowards(thisObject.transform.position, EndPoint, speed * Time.deltaTime);
        if (Vector3.Distance(thisObject.transform.position, EndPoint) < 0.001f)
        {
            thisObject.GetComponent<Moving>().enabled = false;
        }
    }
}
