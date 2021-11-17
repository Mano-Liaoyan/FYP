using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomStat : MonoBehaviour {
    private List<GameObject> Stats;

    // Start is called before the first frame update
    void Start() {
        FindStats();
    }

    // Update is called once per frame
    void Update() {

    }

    public void FindStats() {
        Stats = FindChild(gameObject);
        RandomStatInfo(Stats);
    }

    public void RandomStatInfo(List<GameObject> stats) {
        foreach (var stat in stats) {
            stat.transform.Find("Text_Value").GetComponent<TMP_Text>().text = Random.Range(2000, 5000).ToString(); ;
        }
    }

    public List<GameObject> FindChild(GameObject father) {
        List<GameObject> childs = new List<GameObject>();
        foreach (Transform child in father.transform) {
            childs.Add(child.gameObject);
        }
        return childs;
    }
}
