using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerMagic : MonoBehaviour
{
    [SerializeField] private GameObject attackSkill;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.AddEventListener("NecromancerAttack", Attack);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Vector3 z_offset = new Vector3(0, 0, -20);
        Quaternion y_rotate = Quaternion.Euler(0, 90, 0);
        GameObject skill = (GameObject)Instantiate(Resources.Load($"Prefab/VFX/Modify Magic Ice"),
            transform.position + z_offset, y_rotate);
    }
}
