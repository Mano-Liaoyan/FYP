using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        //GainOrLosePoint.examineLocation(collider.gameObject.name);

    }

    // 碰撞进行过程中会回调的方法
    // 碰撞过程中，每一帧都会调用一次
    private void OnTriggerStay(Collider collider)
    {
        GainOrLosePoint.examineLocation(collider.gameObject.name);
    }

    // 碰撞结束时回调的方法
    private void OnTriggerExit(Collider collider)
    {
        //print(collider.gameObject.name);
    }
}
