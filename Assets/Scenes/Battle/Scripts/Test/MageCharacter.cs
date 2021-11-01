using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageCharacter : Character
{
    void Start()
    {
        myPosition = transform.localPosition;
    }

    public override void Attack(Vector3 endPoint)
    {
        StartCoroutine(MageAttack(endPoint));
    }

    public IEnumerator MageAttack(Vector3 endPoint)
    {
        Vector3 z_offset = new Vector3(0, 0, -20);
        Quaternion y_rotate = Quaternion.Euler(0, 90, 0);
        GameObject skill = (GameObject)Instantiate(Resources.Load($"Prefab/VFX/Modify Magic Ice"),
            transform.position + z_offset, y_rotate);
        yield return new WaitForSeconds(2f);
        EventCenter.Instance.TriggerEventListener("ActiveSlots");
        Destroy(skill);
        
    }

}
