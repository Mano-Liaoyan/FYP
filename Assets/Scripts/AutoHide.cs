using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //RectTransform.RectangleContainsScreenPoint args£ºrect transform¡¢mouse point¡¢the camera that used¡£
            //if (RectTransformUtility.RectangleContainsScreenPoint(transform.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
            //{
            //    Debug.Log("Click on panel");
            //}
            //else
            //{
            //    Debug.Log("Click Others");
            //}
            gameObject.SetActive(false);
        }
    }
}
