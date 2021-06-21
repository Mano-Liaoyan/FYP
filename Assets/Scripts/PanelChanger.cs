using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    public GameObject panel;
    public GameObject mainPage;
    public void ChangePanel()
    {
        panel.SetActive(true);
        mainPage.SetActive(false);
    }
    
    public void Exit()
    {
        Application.Quit ();
    }
}
