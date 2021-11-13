using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int myIndex;
    public string characterType;
    public int level;
    public float health;
    public float buffRate;
    private float currentHealth;

    [SerializeField]
    private GameObject HealthBar;
    [SerializeField]
    private GameObject HealthBarFill;
    [SerializeField]
    protected float movingSpeed = 5.0f;
    protected Vector3 myPosition;

    void Start()
    {
        buffRate = 1.0f;
    }

    public void GetHurt(float damage)
    {
        gameObject.GetComponent<Animator>().Play("Hurt");
        StartCoroutine(SubtracHealth(damage));
    }

    public void GetHurtWithoutDamage()
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
    protected void RotateCharacter()
    {
        transform.Rotate(Vector3.up, 180);
        HealthBar.transform.Rotate(Vector3.up, 180);
    }

    public virtual void Attack(Vector3 endPoint, GameObject targetObj, float damage) { }

    public void SetHealthBar(Sprite sp)
    {
        HealthBar.transform.rotation = Quaternion.identity;
        HealthBar.GetComponent<Slider>().value = 1.0f; // Set the default health to 100%
        HealthBarFill.GetComponent<Image>().sprite = sp;
    }

    public IEnumerator SubtracHealth(float deltaHealth)
    {
        float temp;
        if (currentHealth == 0f)
        {
            currentHealth = health;
        }

        temp = currentHealth;

        print($"Original Health:{health}");
        float timer = 0.01f;
        while (currentHealth - temp < deltaHealth)
        {
            HealthBar.GetComponent<Slider>().value = temp-- / health;
            if (temp <= 0)
            {
                break;
            }
            yield return new WaitForSeconds(timer -= 0.001f);
        }
        currentHealth -= deltaHealth;

        if (currentHealth <= 0) { Dead(); }
    }

    protected void Dead()
    {
        health = 0f;

        if (characterType.Equals("F"))// If it is a friendly character died
        {
            BattleDataManager.FriendlyCharacterPersistance[myIndex] = false;
            EventCenter.Instance.TriggerEventListener("DisableSlot", myIndex);
        }

        else if (characterType.Equals("E"))
        {
            BattleDataManager.EnemyCharacterPersistance[myIndex] = false;
        }

        StartCoroutine(DestorySelf());
    }

    protected IEnumerator DestorySelf()
    {
        gameObject.GetComponent<Animator>().Play("Smoke");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        gameObject.Destroy();
    }



}
