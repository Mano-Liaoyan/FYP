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

    // ��ײ���й����л�ص��ķ���
    // ��ײ�����У�ÿһ֡�������һ��
    private void OnTriggerStay(Collider collider)
    {
        GainOrLosePoint.examineLocation(collider.gameObject.name);
    }

    // ��ײ����ʱ�ص��ķ���
    private void OnTriggerExit(Collider collider)
    {
        //print(collider.gameObject.name);
    }
}
