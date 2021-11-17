using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour {
    private int lastScore;
    private int currentScore;
    // Start is called before the first frame update
    void Start() {
        EventCenter.Instance.AddEventListener("UpdateTopRightScore", UpdateTopRightScore);
        lastScore = Convert.ToInt32( User.score);
    }

    // Update is called once per frame
    void Update() {

    }

    public void UpdateTopRightScore() {
        currentScore = Convert.ToInt32(User.score);
        gameObject.GetComponent<TMP_Text>().text = currentScore.ToString();
        StartCoroutine(ShowDeltaNumber());
    }

    private IEnumerator ShowDeltaNumber() {
        var deltaScore = currentScore - lastScore;
        GameObject scoreNumber = (GameObject)Instantiate(Resources.Load($"Prefab/DamageNumber"),
            transform.position, Quaternion.identity);
        scoreNumber.transform.SetParent(transform.parent, false);
        var temp_position = transform.localPosition;
        temp_position.y -= 130f;
        scoreNumber.transform.localPosition = temp_position;
        var tmp = scoreNumber.GetComponent<TMP_Text>();
        if (deltaScore < 0) {
            tmp.color = Color.red;
            tmp.text = "";
        } else {
            tmp.color = Color.green;
            tmp.text = "+";
        }
        tmp.text += deltaScore.ToString();
        tmp.fontSize = 150f;
        while (tmp.fontSize > 0f) {
            tmp.fontSize--;
            yield return new WaitForSeconds(0.01f);
        }
        lastScore = currentScore;
        Destroy(scoreNumber);
    }
}
