using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMenuClick : MonoBehaviour
{
    public GameObject secondaryButtonGroup; 
    public GameObject mainMenuIcon;

    // ReSharper disable once InconsistentNaming
    private Transform mainMenuIconTrans;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuIconTrans = mainMenuIcon.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        mainMenuIconTrans.Rotate(Vector3.forward,30*Time.deltaTime);
    }

    public void ClickMenu()
    {
        secondaryButtonGroup.SetActive(!secondaryButtonGroup.activeSelf);
    }
}
