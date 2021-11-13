using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageCharacter : Character
{
    void Start()
    {
        myPosition = transform.localPosition;
    }
    void Update()
    {
    }

    public override void Attack(Vector3 endPoint, GameObject targetObj, float damage)
    {
        Vector3 endPointDirection = endPoint - myPosition;
        StartCoroutine(MageAttack(endPointDirection, targetObj, damage));
    }



    public IEnumerator MageAttack(Vector3 direction, GameObject targetObj, float damage)
    {
        Vector3 z_offset = new Vector3(0, 0, -20);
        Quaternion rotation = Quaternion.Euler(-Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, 90, 0);
        gameObject.GetComponent<Animator>().SetTrigger("LeftMouseClick");

        GameObject skill = (GameObject)Instantiate(Resources.Load($"Prefab/VFX/Modify Magic Ice"),
            transform.position + z_offset, rotation);

        skill.GetComponent<MagicIceInfo>().index = myIndex;
        skill.GetComponent<MagicIceInfo>().target = targetObj;
        skill.GetComponent<MagicIceInfo>().damage = damage;
        EventCenter.Instance.TriggerEventListener("ResetParticleCount");
        yield return new WaitForSeconds(2);
        //targetObj.GetComponent<Character>().GetHurt(damage);
        Destroy(skill);
        EventCenter.Instance.TriggerEventListener("ActiveSlots");

    }

}
