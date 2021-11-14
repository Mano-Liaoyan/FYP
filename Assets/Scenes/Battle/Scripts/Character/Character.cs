using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
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
        buffRate = 0f;
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

    public async void BuffSelf()
    {
        ResetBuffRate();
        RandomBuffRate();
        GameObject buffSkill = (GameObject)Instantiate(Resources.Load($"Prefab/VFX/Fx_Spread_Star"),
            transform.position, Quaternion.identity);
        buffSkill.transform.SetParent(transform.parent, false);
        // Reverse the y coordinate
        var temp = myPosition;
        temp.y += 160;
        buffSkill.transform.localPosition = temp;
        buffSkill.transform.localScale = new Vector3(100, 100, 1);
        // This is an enemy character
        if (characterType.Equals("E"))
        {
            ParticleSystem buffPartical = buffSkill.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule buffMain = buffPartical.main;
            buffMain.startColor = Color.red;
            ParticleSystem starPartical = buffSkill.transform.Find("Fx_Star").GetComponent<ParticleSystem>();
            ParticleSystem.MainModule starMain = starPartical.main;
            starMain.startColor = Color.red;
        }
        await Task.Delay(TimeSpan.FromSeconds(1.5));
        Destroy(buffSkill);
        EventCenter.Instance.TriggerEventListener("ActiveSlots");
    }

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
        GameObject DamageNumber = (GameObject)Instantiate(Resources.Load($"Prefab/DamageNumber"),
            transform.position, Quaternion.identity);
        DamageNumber.transform.SetParent(transform.parent, false);
        var temp_position = myPosition;
        temp_position.y += 160;
        DamageNumber.transform.localPosition = temp_position;
        var tmp = DamageNumber.GetComponent<TMP_Text>();
        tmp.text = $"-{(int)deltaHealth}";
        tmp.fontSize = 200f;
        StartCoroutine(DamageNumberDisolve(DamageNumber, tmp));
        while (currentHealth - temp < deltaHealth)
        {
            HealthBar.GetComponent<Slider>().value = temp -- / health;
            if (temp <= 0)
            {
                break;
            }
            yield return new WaitForSeconds(timer -= 0.001f);
        }
        currentHealth -= deltaHealth;

        if (currentHealth <= 0) { Dead(); }
    }

    protected IEnumerator DamageNumberDisolve(GameObject DamageNumber, TMP_Text text)
    {
        while (text.fontSize > 0)
        {
            int x_flag = characterType.Equals("F") ? -1 : 1;
            DamageNumber.transform.localPosition += new Vector3(x_flag * 10, 10, 0);
            text.fontSize -= 3f;
            text.alpha -= 0.05f;
            yield return new WaitForSeconds(0.04f);
        }
        Destroy(DamageNumber);
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

    protected void ResetBuffRate()
    {
        buffRate = 0f;
    }

    protected void RandomBuffRate()
    {
        buffRate = UnityEngine.Random.Range(0, 1.0f);
    }



}
