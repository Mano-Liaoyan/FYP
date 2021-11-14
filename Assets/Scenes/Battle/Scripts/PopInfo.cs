using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopInfo : MonoBehaviour
{
    public GameObject popupTextTitle;
    public GameObject popupTextBody;
    public GameObject popupConfirm;
    public GameObject popupCancel;
    // Start is called before the first frame update
    public void ReversePopup()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        gameObject.transform.SetAsLastSibling();
    }

    public void PopupMessage(string title, string body)
    {
        popupTextTitle.GetComponent<TMP_Text>().text = title;
        popupTextBody.GetComponent<TMP_Text>().text = body;
    }

    public void Escape()
    {
        print("CLICK ESCAPE!");
        UnityMainThreadDispatcher.Instance().Enqueue(() => EventCenter.Instance.TriggerEventListener("Escape"));
    }
}
