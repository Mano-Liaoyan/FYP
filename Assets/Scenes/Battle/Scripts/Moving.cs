using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Vector3 StartPoint;
    public Vector3 EndPoint;
    public float speed = 15.0f;
    private Transform obj;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartPoint = BattleDataManager.FriendlyCharactersPostions[BattleDataManager.currentIndex];
        int randomIdx = Random.Range(0, BattleDataManager.EnemyCharactersPostions.Count);
        EndPoint = BattleDataManager.EnemyCharactersPostions[randomIdx];
        EndPoint.z = 0;
        obj = GameObject.Find($"{BattleDataManager.FriendlyCharacters[BattleDataManager.currentIndex]}(Clone)").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        print($"{obj.position}");
        obj.position = Vector3.MoveTowards(obj.position, EndPoint, speed * Time.deltaTime);
        if (Vector3.Distance(obj.position, EndPoint) < 0.001f)
        {
            this.GetComponent<Moving>().enabled = false;
        }
    }

}
