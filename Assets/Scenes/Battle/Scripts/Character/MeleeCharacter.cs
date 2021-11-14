using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCharacter : Character
{
    private bool isAttacking;


    void Start()
    {
        isAttacking = false;
        myPosition = transform.localPosition;
        EventCenter.Instance.AddEventListener<bool>("ExitAttack", SetAttackStates);
    }

    public override void Attack(Vector3 endPoint, GameObject targetObj, float damage)
    {
        StartCoroutine(MeleeAttack(endPoint, targetObj, damage));
    }

    public IEnumerator MeleeAttack(Vector3 endPoint, GameObject targetObj, float damage)
    {
        float offset = 200f;
        SetAttackStates(true);
        yield return StartCoroutine(IEMoveCharacter(myPosition, endPoint, offset));
        gameObject.GetComponent<Animator>().SetTrigger("LeftMouseClick");
        targetObj.GetComponent<Character>().GetHurt(damage);
        yield return new WaitUntil(() => isAttacking == false);
        RotateCharacter();
        yield return StartCoroutine(IEMoveCharacter(transform.localPosition, myPosition));
        RotateCharacter();
        EventCenter.Instance.TriggerEventListener("ActiveSlots");
        ResetBuffRate();
    }

    private void SetAttackStates(bool state)
    {
        isAttacking = state;
    }
}
