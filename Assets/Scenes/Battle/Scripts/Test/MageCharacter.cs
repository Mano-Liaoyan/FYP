using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageCharacter : Character
{
    void Start()
    {
        myPosition = transform.localPosition;
        //EventCenter.Instance.AddEventListener<string, int>("RotateCharacter", RotateCharacter);
    }

    public override void Attack(Vector3 endPoint)
    {
        StartCoroutine(IEMoveCharacter(myPosition, endPoint));
    }

}
