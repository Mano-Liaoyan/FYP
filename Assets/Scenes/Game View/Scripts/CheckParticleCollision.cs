using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckParticleCollision : MonoBehaviour
{
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        EventCenter.Instance.AddEventListener("ResetParticleCount", ResetCount);
        //EventCenter.Instance.AddEventListener<string>("ChangeLayerNameToFriendCube", ChangeLayerNameToFriendCube);
    }

    public void ChangeLayerNameToFriendCube(string objName)
    {
        print($"OBJ Name: {gameObject.name}");
        if (objName.Equals(gameObject.name))
            gameObject.layer = LayerMask.NameToLayer("FriendCube");
        else
            gameObject.layer = LayerMask.NameToLayer("EnemyCube");
    }

    private void OnParticleCollision(GameObject partical)
    {
        var magicIceInfo = partical.GetComponent<MagicIceInfo>();
        var target = magicIceInfo.target;
        var damage = magicIceInfo.damage;

        if (partical.tag.Equals("Bullet") && count == 0)
        {
            count++;
            target.GetComponent<Character>().GetHurt(damage);
        }
        else if (partical.tag.Equals("Bullet") && count != 0)
        {
            count++;
            target.GetComponent<Character>().GetHurtWithoutDamage();
        }

    }

    public void ResetCount() { count = 0; }
}
