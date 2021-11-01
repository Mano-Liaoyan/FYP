using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCharacter : Character
{
    private bool isAttacking = false;


    void Start()
    {
        myPosition = transform.localPosition;
        //EventCenter.Instance.AddEventListener<string, int>("RotateCharacter", RotateCharacter);
        EventCenter.Instance.AddEventListener("ExitAttack", ReverseAttackStates);
    }

    public override void Attack(Vector3 endPoint)
    {
        StartCoroutine(MeleeAttack(endPoint));
    }

    public IEnumerator MeleeAttack(Vector3 endPoint)
    {
        ReverseAttackStates();
        yield return StartCoroutine(IEMoveCharacter(myPosition, endPoint));
        gameObject.GetComponent<Animator>().SetTrigger("LeftMouseClick");
        yield return new WaitUntil(() => isAttacking == false);
        RotateCharacter();
        yield return StartCoroutine(IEMoveCharacter(endPoint, myPosition));
        RotateCharacter();
        EventCenter.Instance.TriggerEventListener("ActiveSlots");
    }

    private void ReverseAttackStates()
    {
        isAttacking = !isAttacking;
    }
}
