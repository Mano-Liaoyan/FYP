using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int myIndex;
    public string chracterType;
    [SerializeField] protected float movingSpeed = 5.0f;
    protected Vector3 myPosition;

    void Start()
    {
        EventCenter.Instance.AddEventListener("GetHurt", GetHurt);
    }

    public void GetHurt()
    {
        gameObject.GetComponent<Animator>().Play("Hurt");
    }

    protected IEnumerator IEMoveCharacter(Vector3 startPoint, Vector3 endPoint, float offset = 0f)
    {
        Vector3 currentPoint = startPoint;

        while (Vector3.Distance(currentPoint, endPoint) > 1f + offset)
        {
            currentPoint = Vector3.MoveTowards(currentPoint, endPoint, movingSpeed);
            transform.localPosition = currentPoint;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Roate character by 180 degree
    protected IEnumerator RotateCharacter(string type, int index)
    {
        if (myIndex == index && chracterType.Equals(type))
            transform.Rotate(Vector3.up, 180);
        yield return null;
    }

    // Roate character by 180 degree
    protected void RotateCharacter()
    {
        transform.Rotate(Vector3.up, 180);
    }

    public virtual void Attack(Vector3 endPoint, GameObject targetObj) { }


}
