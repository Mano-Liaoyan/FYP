using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        print($"ct: {characterType}");
        //if (characterType.Equals("F"))
        //    EventCenter.Instance.TriggerEventListener("ChangeLayerNameToFriendCube", "FriendCube");
        //else if (characterType.Equals("E"))
        //    EventCenter.Instance.TriggerEventListener("ChangeLayerNameToFriendCube", "EnemyCube");
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

        var collider = skill.GetComponent<ParticleSystem>().collision;

        if (characterType.Equals("F"))
            collider.collidesWith = 1 << 7;
        else if (characterType.Equals("E"))
            collider.collidesWith = 1 << 8;

        skill.GetComponent<ParticleSystem>().Play();
        skill.GetComponent<MagicIceInfo>().index = myIndex;
        skill.GetComponent<MagicIceInfo>().target = targetObj;
        skill.GetComponent<MagicIceInfo>().damage = damage;
        EventCenter.Instance.TriggerEventListener("ResetParticleCount");
        yield return new WaitForSeconds(2);
        //targetObj.GetComponent<Character>().GetHurt(damage);
        Destroy(skill);
        EventCenter.Instance.TriggerEventListener("ActiveSlots");
        ResetBuffRate();
    }

}
